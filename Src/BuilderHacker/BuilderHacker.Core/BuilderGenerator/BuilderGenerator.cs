using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuilderHacker.Core.BuilderGenerator
{
    [Generator(LanguageNames.CSharp)]
    public class BuilderGenerator : ISourceGenerator
    {
        private static readonly DiagnosticDescriptor StaticClassNotSupported = new DiagnosticDescriptor(
            id: "BHG001",
            title: "Static classes are not supported",
            messageFormat: "'{0}' is a static class and cannot have a builder generated",
            category: "BuilderHacker",
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true);

        private static readonly DiagnosticDescriptor RecursivePropertyNotSupported = new DiagnosticDescriptor(
            id: "BHG002",
            title: "Recursive property getter is not supported",
            messageFormat: "BuilderHacker skipped '{0}.{1}' because it has a recursive getter. Fix the getter to use a backing field or base member.",
            category: "BuilderHacker",
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true);

        /// <summary>
        /// Initializes the source generator.
        /// </summary>
        /// <param name="context">The generator initialization context.</param>
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        /// <summary>
        /// Executes source generation for classes marked with <c>GenerateBuilderHackerAttribute</c>.
        /// </summary>
        /// <param name="context">The generator execution context.</param>
        public void Execute(GeneratorExecutionContext context)
        {
            var compilation = context.Compilation;

            foreach (var tree in compilation.SyntaxTrees)
            {
                var root = tree.GetRoot();
                var classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();

                foreach (var cls in classes)
                {
                    var model = compilation.GetSemanticModel(tree);
                    var symbol = model.GetDeclaredSymbol(cls) as INamedTypeSymbol;
                    if (symbol == null)
                    {
                        continue;
                    }

                    var hasAttribute = symbol.GetAttributes()
                        .Any(a => a.AttributeClass?.Name == "GenerateBuilderHackerAttribute");

                    if (!hasAttribute)
                    {
                        continue;
                    }

                    if (symbol.IsStatic)
                    {
                        context.ReportDiagnostic(Diagnostic.Create(
                            StaticClassNotSupported,
                            cls.Identifier.GetLocation(),
                            symbol.Name));
                        continue;
                    }

                    var className = symbol.Name;
                    var builderName = className + "Builder";
                    var members = GetAllInstanceMembers(symbol)
                        .GroupBy(m => m.Name)
                        .Select(g => g.First())
                        .ToList();

                    var sb = new StringBuilder();
                    sb.AppendLine("using System;");
                    sb.AppendLine("using System.Reflection;");
                    sb.AppendLine();
                    sb.AppendLine($"namespace {symbol.ContainingNamespace}");
                    sb.AppendLine("{");
                    sb.AppendLine($"    /// <summary>");
                    sb.AppendLine($"    /// BuilderHacker builder for {className}.");
                    sb.AppendLine($"    /// </summary>");
                    sb.AppendLine($"    public class {builderName}");
                    sb.AppendLine("    {");
                    sb.AppendLine($"        private readonly {className} obj = new {className}();");
                    sb.AppendLine();
                    sb.AppendLine("        /// <summary>");
                    sb.AppendLine("        /// Creates a new builder instance.");
                    sb.AppendLine("        /// </summary>");
                    sb.AppendLine($"        /// <returns>A new {builderName} instance.</returns>");
                    sb.AppendLine($"        public static {builderName} Create() => new {builderName}();");
                    sb.AppendLine();
                    sb.AppendLine("        /// <summary>");
                    sb.AppendLine("        /// Sets a property or field by name using reflection.");
                    sb.AppendLine("        /// </summary>");
                    sb.AppendLine("        /// <typeparam name=\"TValue\">The value type.</typeparam>");
                    sb.AppendLine("        /// <param name=\"name\">The member name.</param>");
                    sb.AppendLine("        /// <param name=\"value\">The value to assign.</param>");
                    sb.AppendLine("        /// <returns>The current builder instance.</returns>");
                    sb.AppendLine($"        private {builderName} SetMember<TValue>(string name, TValue value)");
                    sb.AppendLine("        {");
                    sb.AppendLine("            var type = obj.GetType();");
                    sb.AppendLine("            while (type != null)");
                    sb.AppendLine("            {");
                    sb.AppendLine("                var prop = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);");
                    sb.AppendLine("                if (prop != null)");
                    sb.AppendLine("                {");
                    sb.AppendLine("                    var setter = prop.GetSetMethod(true);");
                    sb.AppendLine("                    if (setter != null)");
                    sb.AppendLine("                    {");
                    sb.AppendLine("                        setter.Invoke(obj, new object[] { value });");
                    sb.AppendLine("                        return this;");
                    sb.AppendLine("                    }");
                    sb.AppendLine("                }");
                    sb.AppendLine();
                    sb.AppendLine("                var field = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);");
                    sb.AppendLine("                if (field != null)");
                    sb.AppendLine("                {");
                    sb.AppendLine("                    field.SetValue(obj, value);");
                    sb.AppendLine("                    return this;");
                    sb.AppendLine("                }");
                    sb.AppendLine();
                    sb.AppendLine("                type = type.BaseType;");
                    sb.AppendLine("            }");
                    sb.AppendLine();
                    sb.AppendLine("            throw new Exception(string.Format(\"Property or field '{0}' not found on {1}\", name, obj.GetType().Name));");
                    sb.AppendLine("        }");
                    sb.AppendLine();

                    foreach (var member in members)
                    {
                        if (member is IPropertySymbol prop)
                        {
                            if (prop.IsIndexer || prop.SetMethod == null || prop.IsStatic)
                            {
                                continue;
                            }

                            if (IsRecursiveGetter(prop))
                            {
                                context.ReportDiagnostic(Diagnostic.Create(
                                    RecursivePropertyNotSupported,
                                    GetMemberLocation(prop),
                                    className,
                                    prop.Name));
                                continue;
                            }

                            var typeName = prop.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                            sb.AppendLine("        /// <summary>");
                            sb.AppendLine($"        /// Sets the {prop.Name} member of the {className} instance.");
                            sb.AppendLine("        /// </summary>");
                            sb.AppendLine($"        /// <param name=\"value\">The value to assign to {prop.Name}.</param>");
                            sb.AppendLine("        /// <returns>The current builder instance.</returns>");
                            sb.AppendLine($"        public {builderName} Set{prop.Name}({typeName} value)");
                            sb.AppendLine("        {");
                            sb.AppendLine($"            return SetMember(\"{prop.Name}\", value);");
                            sb.AppendLine("        }");
                            sb.AppendLine();
                            continue;
                        }

                        if (member is IFieldSymbol field)
                        {
                            if (field.IsStatic || field.IsReadOnly || field.IsConst || field.Name.StartsWith("<"))
                            {
                                continue;
                            }

                            var typeName = field.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
                            sb.AppendLine("        /// <summary>");
                            sb.AppendLine($"        /// Sets the {field.Name} field of the {className} instance.");
                            sb.AppendLine("        /// </summary>");
                            sb.AppendLine($"        /// <param name=\"value\">The value to assign to {field.Name}.</param>");
                            sb.AppendLine("        /// <returns>The current builder instance.</returns>");
                            sb.AppendLine($"        public {builderName} Set{field.Name}({typeName} value)");
                            sb.AppendLine("        {");
                            sb.AppendLine($"            return SetMember(\"{field.Name}\", value);");
                            sb.AppendLine("        }");
                            sb.AppendLine();
                        }
                    }

                    sb.AppendLine("        /// <summary>");
                    sb.AppendLine($"        /// Builds and returns the constructed {className} instance.");
                    sb.AppendLine("        /// </summary>");
                    sb.AppendLine($"        /// <returns>The constructed {className} instance.</returns>");
                    sb.AppendLine($"        public {className} Build() => obj;");
                    sb.AppendLine("    }");
                    sb.AppendLine("}");

                    context.AddSource($"{builderName}.g.cs", sb.ToString());
                }
            }
        }

        private static Location GetMemberLocation(IPropertySymbol prop)
        {
            var syntaxRef = prop.DeclaringSyntaxReferences.FirstOrDefault();
            return syntaxRef != null ? syntaxRef.GetSyntax().GetLocation() : Location.None;
        }

        private static bool IsRecursiveGetter(IPropertySymbol prop)
        {
            var syntaxRef = prop.DeclaringSyntaxReferences.FirstOrDefault();
            var propertySyntax = syntaxRef == null ? null : syntaxRef.GetSyntax() as PropertyDeclarationSyntax;
            if (propertySyntax == null)
            {
                return false;
            }

            var getter = propertySyntax.AccessorList == null ? null : propertySyntax.AccessorList.Accessors.FirstOrDefault(a => a.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.GetAccessorDeclaration));
            if (getter == null)
            {
                return false;
            }

            if (getter.ExpressionBody != null)
            {
                var identifier = getter.ExpressionBody.Expression as Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax;
                return identifier != null && identifier.Identifier.ValueText == prop.Name;
            }

            if (getter.Body != null)
            {
                foreach (var statement in getter.Body.Statements)
                {
                    var returnStatement = statement as Microsoft.CodeAnalysis.CSharp.Syntax.ReturnStatementSyntax;
                    if (returnStatement != null)
                    {
                        var identifier = returnStatement.Expression as Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax;
                        if (identifier != null && identifier.Identifier.ValueText == prop.Name)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private static IEnumerable<ISymbol> GetAllInstanceMembers(INamedTypeSymbol symbol)
        {
            var current = symbol;
            while (current != null)
            {
                foreach (var member in current.GetMembers())
                {
                    if (member is IPropertySymbol prop)
                    {
                        if (prop.IsStatic || prop.IsIndexer || prop.SetMethod == null)
                        {
                            continue;
                        }

                        yield return member;
                        continue;
                    }

                    if (member is IFieldSymbol field)
                    {
                        if (field.IsStatic || field.IsReadOnly || field.IsConst || field.Name.StartsWith("<"))
                        {
                            continue;
                        }

                        yield return member;
                    }
                }

                current = current.BaseType;
            }
        }
    }
}
