using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
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

            context.RegisterSourceOutput(provider, static (spc, source) => ExecuteGeneration(spc, source.source, source.model));
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
        private static (ClassDeclarationSyntax source, SemanticModel model) GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
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
                            return (classDeclarationSyntax, context.SemanticModel);
                        }
                    }
                }
            }

            return (null, context.SemanticModel);
        }

        /// <summary>
        /// Generates the builder class source code based on the target class.
        /// Only generates methods for non-static properties that define a setter.
        /// </summary>
        private static void ExecuteGeneration(SourceProductionContext context, ClassDeclarationSyntax classDeclarationSyntax, SemanticModel model)
        {
            if (classDeclarationSyntax == null)
                return;

            var symbol = model.GetDeclaredSymbol(classDeclarationSyntax) as INamedTypeSymbol;
            if (symbol == null)
                return;

            var className = symbol.Name;
            var @namespace = symbol.ContainingNamespace.ToDisplayString();

            // Use HashSet-based deduplication for cross-framework compatibility (instead of DistinctBy which requires .NET 7+)
            var seenPropertyNames = new HashSet<string>();
            var properties = new List<IPropertySymbol>();

            foreach (var prop in GetAllProperties(symbol))
            {
                if (prop.SetMethod != null &&
                    !prop.IsStatic &&
                    seenPropertyNames.Add(prop.Name))  // Add returns true only if property was added (first occurrence)
                {
                    properties.Add(prop);
                }
            }

            if (properties.Count == 0)
                return;

            var sb = new StringBuilder();

            // Use string.Format() instead of interpolation for potential future .NET Framework support
            sb.AppendLine(string.Format("namespace {0}", @namespace));
            sb.AppendLine("{");
            sb.AppendLine(string.Format("    public partial class {0}", className));
            sb.AppendLine("    {");
            sb.AppendLine(string.Format("        public static {0}Builder Builder() => new {0}Builder();", className));
            sb.AppendLine();
            sb.AppendLine(string.Format("        public class {0}Builder", className));
            sb.AppendLine("        {");
            sb.AppendLine(string.Format("            private readonly {0} obj = new {0}();", className));
            sb.AppendLine();

            foreach (var prop in properties)
            {
                var propertyTypeName = prop.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                sb.AppendLine("            /// <summary>");
                sb.AppendLine(string.Format("            /// Sets the {0} property of the {1} instance.", prop.Name, className));
                sb.AppendLine("            /// </summary>");
                sb.AppendLine(string.Format("            /// <param name=\"value\">The value to set for {0}.</param>", prop.Name));
                sb.AppendLine("            /// <returns>The current builder instance for method chaining.</returns>");
                sb.AppendLine(string.Format("            public {0}Builder {1}({2} value)", className, prop.Name, propertyTypeName));
                sb.AppendLine("            {");
                sb.AppendLine(string.Format("                obj.{0} = value;", prop.Name));
                sb.AppendLine("                return this;");
                sb.AppendLine("            }");
                sb.AppendLine();
            }

            sb.AppendLine("            /// <summary>");
            sb.AppendLine(string.Format("            /// Builds and returns the constructed {0} instance.", className));
            sb.AppendLine("            /// </summary>");
            sb.AppendLine("            /// <returns>The constructed instance.</returns>");
            sb.AppendLine(string.Format("            public {0} Build() => obj;", className));
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("}");

            context.AddSource(string.Format("{0}.Builder.g.cs", className), sb.ToString());
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
    }
}
