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
        public void AddAuthorizationSource(GeneratorExecutionContext context, INamespaceSymbol featureNamespace, INamedTypeSymbol entityType, string[] primaryKeyPropertyNames)
        {
            var entityNameWithoutSufix = Regex.Replace(entityType.Name, "Entity$", string.Empty);

            var buildSourceNamespace = $"{featureNamespace.ToDisplayString()}.Authorizations";
            var buildSourceClassName = $"Default{entityNameWithoutSufix}AuthorizationHandler";

            var namespaces = Enumerable
                .Empty<string>()
                .Append($"Microsoft.AspNetCore.Authorization")
                .Append($"Microsoft.AspNetCore.Authorization.Infrastructure")
                .Append($"System.Threading.Tasks")
                .Append($"{entityType.ContainingNamespace.ToDisplayString()}")
                .Append($"{nameof(AspNetCoreNuxt)}.Applications.WebHost.Core.Authorizations")
                .Append($"{nameof(AspNetCoreNuxt)}.Extensions.AspNetCore.Authorization")
                .Append($"{nameof(AspNetCoreNuxt)}.Extensions.DependencyInjection")
                .Select(x => $"using {x};")
                .OrderBy(x => x)
                .Distinct();

            var builder = new StringBuilder();
            builder.Append(string.Join(Environment.NewLine, namespaces));
            builder.Append($@"
namespace {buildSourceNamespace}
{{
    public sealed class {buildSourceClassName} : ExpandObjectAuthorizationHandler<OperationAuthorizationRequirement, {entityType.Name}, {entityType.Name}>, IDependencyInjectionService<IAuthorizationHandler>
    {{
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, ExpandObjectAuthorizationResource<{entityType.Name}, {entityType.Name}> resource)
        {{
            if (requirement.Name == Operations.Update.Name)
            {{");
            foreach (var primaryKeyPropertyName in primaryKeyPropertyNames)
            {
                builder.Append($@"
                if (resource.PropertyName == nameof({entityType.Name}.{primaryKeyPropertyName}))
                {{
                    context.Fail();
                    return Task.CompletedTask;
                }}");
            }
            builder.Append($@"
            }}
            return Task.CompletedTask;
        }}
    }}
}}");

            context.AddSource($"{Guid.NewGuid()}.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
        }
    }
}
