using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Linq;
using System.Text;

namespace AspNetCoreNuxt.ApiSourceGenerator
{
    public partial class SourceGenerator
    {
        public void AddModelSource(GeneratorExecutionContext context, INamespaceSymbol featureNamespace, INamedTypeSymbol entityType)
        {
            foreach (var property in entityType.GetMembers().OfType<IPropertySymbol>())
            {
                var namedTypeSymbol = (INamedTypeSymbol)property.Type;
                if (namedTypeSymbol.ContainingNamespace.ToDisplayString() == entityType.ContainingNamespace.ToDisplayString())
                {
                    AddModelSource(context, featureNamespace, namedTypeSymbol);
                }
                if (namedTypeSymbol.TypeArguments.Length == 1 &&
                    namedTypeSymbol.TypeArguments[0].ContainingNamespace.ToDisplayString() == entityType.ContainingNamespace.ToDisplayString())
                {
                    AddModelSource(context, featureNamespace, (INamedTypeSymbol)namedTypeSymbol.TypeArguments[0]);
                }
            }

            var buildSourceNamespace = $"{featureNamespace.ToDisplayString()}.Models";
            var buildSourceClassName = $"Api{entityType.Name}";

            var namespaces = Enumerable
                .Empty<string>()
                .Append($"System.Collections.Generic")
                .Append($"{entityType.ContainingNamespace.ToDisplayString()}")
                .Append($"{nameof(AspNetCoreNuxt)}.Applications.WebHost.Core.Models")
                .Select(x => $"using {x};")
                .OrderBy(x => x)
                .Distinct();

            var builder = new StringBuilder();
            builder.Append(string.Join(Environment.NewLine, namespaces));
            builder.Append($@"
namespace {buildSourceNamespace}
{{
    public partial class {buildSourceClassName} : {entityType.Name}, IIdentification
    {{");
            foreach (var property in entityType.GetMembers().OfType<IPropertySymbol>())
            {
                var propertyNamedTypeSymbol = (INamedTypeSymbol)property.Type;
                if (property.Type.IsValueType && property.Type.NullableAnnotation != NullableAnnotation.Annotated)
                {
                    builder.Append($@"
        public new {propertyNamedTypeSymbol.ToDisplayString()}? {property.Name} {{ get; set; }}");
                }
                else if (propertyNamedTypeSymbol.ContainingNamespace.ToDisplayString() == entityType.ContainingNamespace.ToDisplayString())
                {
                    builder.Append($@"
        public new Api{propertyNamedTypeSymbol.Name} {property.Name} {{ get; set; }}");
                }
                else if (propertyNamedTypeSymbol.TypeArguments.Length == 1 &&
                         propertyNamedTypeSymbol.TypeArguments[0].ContainingNamespace.ToDisplayString() == entityType.ContainingNamespace.ToDisplayString())
                {
        //            builder.Append($@"
        //public new IEnumerable<Api{propertyNamedTypeSymbol.TypeArguments[0].Name}> {property.Name} {{ get; set; }}");
                    builder.Append($@"
        public new Api{propertyNamedTypeSymbol.TypeArguments[0].Name}[] {property.Name} {{ get; set; }}");
                }
            }
            builder.Append($@"
        public int? Identifier {{ get; set; }}
    }}
}}");

            context.AddSource($"{Guid.NewGuid()}.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
        }
    }
}
