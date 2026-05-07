using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuilderHacker.Core.BuilderGenerator
{
    [Generator(LanguageNames.CSharp)]
    public class BuilderGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var provider = context.SyntaxProvider.CreateSyntaxProvider(
                predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
                transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx)
            ).Where(static m => m.source is not null);

            context.RegisterSourceOutput(provider, static (spc, source) => ExecuteGeneration(spc, source.source!, source.model));
        }

        private static bool IsSyntaxTargetForGeneration(SyntaxNode node)
            => node is ClassDeclarationSyntax;

        private static (ClassDeclarationSyntax? source, SemanticModel model) GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
        {
            var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;
            foreach (var attributeListSyntax in classDeclarationSyntax.AttributeLists)
            {
                foreach (var attributeSyntax in attributeListSyntax.Attributes)
                {
                    if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                        continue;

                    INamedTypeSymbol? attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                    if (attributeContainingTypeSymbol?.Name == "GenerateBuilderHackerAttribute")
                    {
                        return (classDeclarationSyntax, context.SemanticModel);
                    }
                }
            }

            return (null, context.SemanticModel);
        }

        private static void ExecuteGeneration(SourceProductionContext context, ClassDeclarationSyntax classDeclarationSyntax, SemanticModel model)
        {
            var symbol = model.GetDeclaredSymbol(classDeclarationSyntax) as INamedTypeSymbol;

            if (symbol == null) return;

            var className = symbol.Name;

            var props = GetAllProperties(symbol)
                .Where(p => p.DeclaredAccessibility == Accessibility.Public || p.DeclaredAccessibility == Accessibility.Private)
                .DistinctBy(p => p.Name);

            var sb = new StringBuilder();

            sb.Append($@"
namespace {symbol.ContainingNamespace}
{{
    public partial class {className}
    {{
        public static {className}Builder Builder() => new {className}Builder();

        public class {className}Builder
        {{
            private readonly {className} obj = new {className}();
");

            foreach (var prop in props)
            {
                sb.Append($@"
            /// <summary>
            /// Sets the {prop.Name} property of the {className} instance.
            /// </summary>
            /// <param name=""value"">The value to set for {prop.Name}.</param>
            /// <returns>The current builder instance for method chaining.</returns>
            public {className}Builder {prop.Name}({prop.Type} value)
            {{
                obj.{prop.Name} = value;
                return this;
            }}
");
            }

            sb.Append($@"
            /// <summary>
            /// Builds and returns the constructed {className} instance.
            /// </summary>
            /// <returns>The constructed {className} instance.</returns>
            public {className} Build() => obj;
        }}
    }}
}}");

            context.AddSource($"{className}.Builder.g.cs", sb.ToString());
        }

        private static IEnumerable<IPropertySymbol> GetAllProperties(INamedTypeSymbol symbol)
        {
            var current = symbol;
            while (current != null)
            {
                foreach (var member in current.GetMembers())
                {
                    if (member is IPropertySymbol prop)
                    {
                        yield return prop;
                    }
                }
                current = current.BaseType;
            }
        }
    }
}
