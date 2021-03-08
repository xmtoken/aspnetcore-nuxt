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
        public void AddSearchSpecificationSource(GeneratorExecutionContext context, INamespaceSymbol featureNamespace, INamedTypeSymbol entityType, string[] excludePropertyNames)
        {
            var entityNameWithoutSufix = Regex.Replace(entityType.Name, "Entity$", string.Empty);

            var buildSourceNamespace = $"{featureNamespace.ToDisplayString()}.Specifications";
            var buildSourceClassName = $"{entityNameWithoutSufix}SearchSpecification";

            var namespaces = Enumerable
                .Empty<string>()
                .Append($"{entityType.ContainingNamespace.ToDisplayString()}")
                .Append($"{featureNamespace.ToDisplayString()}.Models")
                .Append($"{nameof(AspNetCoreNuxt)}.Extensions.AspNetCore.Mvc.ApplicationModels")
                .Select(x => $"using {x};")
                .OrderBy(x => x)
                .Distinct();

            var builder = new StringBuilder();
            builder.Append(string.Join(Environment.NewLine, namespaces));
            builder.Append($@"
namespace {buildSourceNamespace}
{{
    public partial class {buildSourceClassName} : SearchSpecification<{entityType.Name}>
    {{
        public {buildSourceClassName}({entityNameWithoutSufix}SearchConditions conditions)
        {{");
            foreach (var property in entityType.GetMembers().OfType<IPropertySymbol>())
            {
                if (excludePropertyNames.Contains(property.Name))
                {
                    continue;
                }
                if (property.Type.IsValueType || property.Type.ToDisplayString() == "string")
                {
                    builder.Append($@"
            TryAdd(conditions.{property.Name});");
                }
            }
            builder.Append($@"
            BuildExpression();
        }}
        partial void BuildExpression();
    }}
}}");

            context.AddSource($"{Guid.NewGuid()}.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
        }
    }
}
