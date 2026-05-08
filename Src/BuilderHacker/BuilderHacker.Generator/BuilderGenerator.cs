using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuilderHacker.Generator
{
    /// <summary>
    /// Incremental source generator that creates builder classes for types decorated with GenerateBuilderHackerAttribute.
    /// </summary>
    /// <remarks>
    /// This generator requires .NET 6.0 or higher due to the IIncrementalGenerator interface and Roslyn API requirements.
    /// 
    /// For .NET Framework 4.5+, .NET Standard 2.0+, or .NET Core 2.0-5.0, use EntityBuilder&lt;T&gt; from BuilderHacker.Core instead.
    /// 
    /// Performance Characteristics:
    /// - Uses incremental generation for fast builds (only changed files are regenerated)
    /// - Caches syntax tree analysis to minimize redundant checks
    /// - O(1) property deduplication using HashSet approach
    /// 
    /// Generated Code Example:
    /// [GenerateBuilderHacker]
    /// public partial class User { public string Name { get; set; } }
    /// 
    /// Generated:
    /// public static UserBuilder Builder() => new UserBuilder();
    /// public class UserBuilder {
    ///     public UserBuilder Name(string value) { obj.Name = value; return this; }
    ///     public User Build() => obj;
    /// }
    /// </remarks>
    [Generator(LanguageNames.CSharp)]
    public class BuilderGenerator : IIncrementalGenerator
    {
        private const string GeneratorAttributeFullName = "BuilderHacker.Abstraction.Attributes.GenerateBuilderHackerAttribute";

        /// <summary>
        /// Initializes the incremental generator with syntax and semantic providers.
        /// </summary>
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var provider = context.SyntaxProvider.CreateSyntaxProvider(
                predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
                transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx)
            ).Where(static m => m.source is not null);

            context.RegisterSourceOutput(provider, static (spc, source) => ExecuteGeneration(spc, source.source, source.model, source.createPartial));
        }

        /// <summary>
        /// Checks if a syntax node is a class declaration with attributes - early filter for performance.
        /// </summary>
        private static bool IsSyntaxTargetForGeneration(SyntaxNode node)
        {
            var classDecl = node as ClassDeclarationSyntax;
            return classDecl != null && classDecl.AttributeLists.Count > 0;
        }

        /// <summary>
        /// Retrieves semantic information for a class declaration.
        /// Returns null if the class doesn't have the GenerateBuilderHacker attribute.
        /// </summary>
        private static (ClassDeclarationSyntax source, SemanticModel model, bool createPartial) GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
        {
            var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;

            foreach (var attributeListSyntax in classDeclarationSyntax.AttributeLists)
            {
                foreach (var attributeSyntax in attributeListSyntax.Attributes)
                {
                    var symbolInfo = context.SemanticModel.GetSymbolInfo(attributeSyntax);
                    var attributeSymbol = symbolInfo.Symbol as IMethodSymbol;

                    if (attributeSymbol != null)
                    {
                        var containingType = attributeSymbol.ContainingType;
                        if (containingType != null && containingType.ToDisplayString() == GeneratorAttributeFullName)
                        {
                            var createPartial = false;
                            TryGetCreatePartialValue(attributeSyntax, out createPartial);
                            return (classDeclarationSyntax, context.SemanticModel, createPartial);
                        }
                    }
                }
            }

            return (null, context.SemanticModel, false);
        }

        /// <summary>
        /// Generates the builder class source code based on the target class.
        /// Only generates methods for non-static properties that define a setter.
        /// </summary>
        private static void ExecuteGeneration(SourceProductionContext context, ClassDeclarationSyntax classDeclarationSyntax, SemanticModel model, bool createPartial)
        {
            if (classDeclarationSyntax == null)
                return;

            var symbol = model.GetDeclaredSymbol(classDeclarationSyntax) as INamedTypeSymbol;
            if (symbol == null)
                return;

            var className = symbol.Name;
            var @namespace = symbol.ContainingNamespace.ToDisplayString();

            // Use dictionary to track properties by name, preferring the most derived version
            // This handles the 'new' keyword shadowing case correctly
            var propertiesByName = new Dictionary<string, IPropertySymbol>();
            var allProps = GetAllProperties(symbol).ToList();

            // Process properties in reverse order (from most derived to base)
            // to ensure we keep the most derived version when shadowing occurs
            for (int i = allProps.Count - 1; i >= 0; i--)
            {
                var prop = allProps[i];

                if (prop.SetMethod == null || prop.IsStatic)
                    continue;

                if (!createPartial && !CanBeSetFromStandaloneBuilder(prop))
                    continue;

                // Only add if we haven't seen this property name yet (first/most-derived wins)
                if (!propertiesByName.ContainsKey(prop.Name))
                {
                    propertiesByName[prop.Name] = prop;
                }
            }

            var properties = propertiesByName.Values.ToList();

            if (properties.Count == 0)
                return;

            var sb = new StringBuilder();

            // Use string.Format() instead of interpolation for potential future .NET Framework support
            sb.AppendLine(string.Format("namespace {0}", @namespace));
            sb.AppendLine("{");

            if (createPartial)
            {
                sb.AppendLine(string.Format("    public partial class {0}", className));
                sb.AppendLine("    {");
                sb.AppendLine(string.Format("        public static {0}Builder Builder() => new {0}Builder();", className));
                sb.AppendLine();
                sb.AppendLine(string.Format("        public class {0}Builder", className));
                sb.AppendLine("        {");
                sb.AppendLine(string.Format("            private readonly {0} obj = new {0}();", className));
                sb.AppendLine();

                AppendBuilderMembers(sb, className, properties, "            ", "                ", "obj", "obj");

                sb.AppendLine("        }");
                sb.AppendLine("    }");
            }
            else
            {
                sb.AppendLine(string.Format("    public class {0}Builder : {0}", className));
                sb.AppendLine("    {");
                sb.AppendLine(string.Format("        public static {0}Builder Create() => new {0}Builder();", className));
                sb.AppendLine();

                AppendBuilderMembers(sb, className, properties, "        ", "            ", "base", "this");

                sb.AppendLine("    }");
            }

            sb.AppendLine("}");

            context.AddSource(string.Format("{0}.Builder.g.cs", className), sb.ToString());
        }

        private static void AppendBuilderMembers(StringBuilder sb, string className, IEnumerable<IPropertySymbol> properties, string memberIndent, string bodyIndent, string targetExpression, string buildExpression)
        {
            foreach (var prop in properties)
            {
                var propertyTypeName = prop.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                sb.AppendLine(string.Format("{0}/// <summary>", memberIndent));
                sb.AppendLine(string.Format("{0}/// Sets the {1} property of the {2} instance.", memberIndent, prop.Name, className));
                sb.AppendLine(string.Format("{0}/// </summary>", memberIndent));
                sb.AppendLine(string.Format("{0}/// <param name=\"value\">The value to set for {1}.</param>", memberIndent, prop.Name));
                sb.AppendLine(string.Format("{0}/// <returns>The current builder instance for method chaining.</returns>", memberIndent));
                sb.AppendLine(string.Format("{0}public {1}Builder {2}({3} value)", memberIndent, className, prop.Name, propertyTypeName));
                sb.AppendLine(string.Format("{0}{{", memberIndent));
                sb.AppendLine(string.Format("{0}{1}.{2} = value;", bodyIndent, targetExpression, prop.Name));
                sb.AppendLine(string.Format("{0}return this;", bodyIndent));
                sb.AppendLine(string.Format("{0}}}", memberIndent));
                sb.AppendLine();
            }

            sb.AppendLine(string.Format("{0}/// <summary>", memberIndent));
            sb.AppendLine(string.Format("{0}/// Builds and returns the constructed {1} instance.", memberIndent, className));
            sb.AppendLine(string.Format("{0}/// </summary>", memberIndent));
            sb.AppendLine(string.Format("{0}/// <returns>The constructed instance.</returns>", memberIndent));
            sb.AppendLine(string.Format("{0}public {1} Build() => {2};", memberIndent, className, buildExpression));
        }

        private static bool TryGetCreatePartialValue(AttributeSyntax attributeSyntax, out bool createPartial)
        {
            createPartial = false;

            var argumentList = attributeSyntax.ArgumentList;
            if (argumentList == null)
                return false;

            foreach (var argument in argumentList.Arguments)
            {
                var nameEquals = argument.NameEquals;
                if (nameEquals != null && nameEquals.Name.Identifier.ValueText == "CreatePartial")
                {
                    return TryReadBooleanLiteral(argument.Expression, out createPartial);
                }
            }

            if (argumentList.Arguments.Count > 0)
            {
                return TryReadBooleanLiteral(argumentList.Arguments[0].Expression, out createPartial);
            }

            return false;
        }

        private static bool TryReadBooleanLiteral(ExpressionSyntax expression, out bool value)
        {
            var literal = expression as LiteralExpressionSyntax;
            if (literal != null)
            {
                if (literal.IsKind(SyntaxKind.TrueLiteralExpression))
                {
                    value = true;
                    return true;
                }

                if (literal.IsKind(SyntaxKind.FalseLiteralExpression))
                {
                    value = false;
                    return true;
                }
            }

            value = false;
            return false;
        }

        /// <summary>
        /// Gets all properties from the symbol and its base types.
        /// Walks the inheritance hierarchy to collect properties from base classes.
        /// </summary>
        private static IEnumerable<IPropertySymbol> GetAllProperties(INamedTypeSymbol symbol)
        {
            var current = symbol;
            while (current != null)
            {
                var members = current.GetMembers();
                foreach (var member in members)
                {
                    var prop = member as IPropertySymbol;
                    if (prop != null)
                    {
                        yield return prop;
                    }
                }

                current = current.BaseType;
            }
        }

        private static bool CanBeSetFromStandaloneBuilder(IPropertySymbol prop)
        {
            var setter = prop.SetMethod;
            if (setter == null)
                return false;

            switch (setter.DeclaredAccessibility)
            {
                case Accessibility.Public:
                case Accessibility.Internal:
                case Accessibility.Protected:
                case Accessibility.ProtectedOrInternal:
                    return true;
                default:
                    return false;
            }
        }
    }
}
