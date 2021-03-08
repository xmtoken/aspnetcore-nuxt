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
        public void AddUseCaseSource(GeneratorExecutionContext context, INamespaceSymbol featureNamespace, INamedTypeSymbol entityType)
        {
            var entityNameWithoutSufix = Regex.Replace(entityType.Name, "Entity$", string.Empty);

            var buildSourceNamespace = $"{featureNamespace.ToDisplayString()}.UseCases";
            var buildSourceClassName = $"{entityNameWithoutSufix}UseCaseBase";

            var namespaces = Enumerable
                .Empty<string>()
                .Append($"AutoMapper")
                .Append($"{entityType.ContainingNamespace.ToDisplayString()}")
                .Append($"{nameof(AspNetCoreNuxt)}.Applications.WebHost.Core.UseCases")
                .Append($"{nameof(AspNetCoreNuxt)}.Domains.Data")
                .Append($"{nameof(AspNetCoreNuxt)}.Extensions.AspNetCore.Mvc")
                .Append($"{nameof(AspNetCoreNuxt)}.Extensions.DependencyInjection")
                .Select(x => $"using {x};")
                .OrderBy(x => x)
                .Distinct();

            var builder = new StringBuilder();
            builder.Append(string.Join(Environment.NewLine, namespaces));
            builder.Append($@"
namespace {buildSourceNamespace}
{{
    public partial class {buildSourceClassName} : UseCaseBase<{entityType.Name}>, IDependencyInjectionService
    {{
        public {buildSourceClassName}(AppDbContext context, IExpandoObjectFactory expandoObjectFactory, IMapper mapper)
            : base(context, expandoObjectFactory, mapper)
        {{
        }}
    }}
}}");

            context.AddSource($"{Guid.NewGuid()}.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
        }
    }
}
