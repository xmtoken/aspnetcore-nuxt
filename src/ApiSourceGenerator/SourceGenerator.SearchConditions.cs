using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AspNetCoreNuxt.ApiSourceGenerator
{
    public partial class SourceGenerator
    {
        public void AddSearchConditionsSource(GeneratorExecutionContext context, INamespaceSymbol featureNamespace, INamedTypeSymbol entityType, string[] excludePropertyNames)
        {
            var entityNameWithoutSufix = Regex.Replace(entityType.Name, "Entity$", string.Empty);

            var buildSourceNamespace = $"{featureNamespace.ToDisplayString()}.Models";
            var buildSourceClassName = $"{entityNameWithoutSufix}SearchConditions";

            var namespaces = entityType
                .GetMembers()
                .Select(x => x.ContainingNamespace.ToDisplayString())
                .Append($"NJsonSchema.Annotations")
                .Append($"System.Collections.Generic")
                .Append($"{nameof(AspNetCoreNuxt)}.Extensions.AspNetCore.Mvc.ApplicationModels")
                .Select(x => $"using {x};")
                .OrderBy(x => x)
                .Distinct();

            var builder = new StringBuilder();
            builder.Append(string.Join(Environment.NewLine, namespaces));
            builder.Append($@"
namespace {buildSourceNamespace}
{{
    public partial class {buildSourceClassName}
    {{");
            foreach (var property in entityType.GetMembers().OfType<IPropertySymbol>())
            {
                if (excludePropertyNames.Contains(property.Name))
                {
                    continue;
                }
                if (property.Type.IsValueType || property.Type.ToDisplayString() == "string")
                {
                    var propertyTypeName = property.Type.ToDisplayString().TrimEnd('?');
                    builder.Append($@"
        [JsonSchemaType(typeof(IEnumerable<string>))]
        public SearchConditions<{propertyTypeName}> {property.Name} {{ get; set; }}");
                }
            }
            builder.Append($@"
    }}
}}");

            context.AddSource($"{Guid.NewGuid()}.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
        }
    }
}
