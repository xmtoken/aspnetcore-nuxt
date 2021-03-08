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
        public void AddValidatorSource(GeneratorExecutionContext context, INamespaceSymbol featureNamespace, INamedTypeSymbol entityType)
        {
            var entityNameWithoutSufix = Regex.Replace(entityType.Name, "Entity$", string.Empty);

            var buildSourceNamespace = $"{featureNamespace.ToDisplayString()}.Validators";
            var buildSourceClassName = $"{entityNameWithoutSufix}ValidatorRuleSet";

            var namespaces = Enumerable
                .Empty<string>()
                .Select(x => $"using {x};")
                .OrderBy(x => x)
                .Distinct();

            var builder = new StringBuilder();
            builder.Append(string.Join(Environment.NewLine, namespaces));
            builder.Append($@"
namespace {buildSourceNamespace}
{{
    public static partial class {buildSourceClassName}
    {{
        public const string Add = nameof(Add);
        public const string AddOrUpdate = nameof(AddOrUpdate);
        public const string AddOrUpdateOnBulk = nameof(AddOrUpdateOnBulk);
        public const string Delete = nameof(Delete);
        public const string DeleteOnBulk = nameof(DeleteOnBulk);
        public const string Update = nameof(Update);
    }}
}}");

            context.AddSource($"{Guid.NewGuid()}.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
        }
    }
}
