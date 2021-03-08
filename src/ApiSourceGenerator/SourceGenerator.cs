using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Linq;
using System.Text;

namespace AspNetCoreNuxt.ApiSourceGenerator
{
    [Generator]
    public partial class SourceGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new ClassDeclarationSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var attributeSourceText = SourceText.From(AutoGenerateApiAttribute.SourceText, Encoding.UTF8);
            context.AddSource($"{AutoGenerateApiAttribute.Name}.g.cs", attributeSourceText);

            if (context.SyntaxReceiver is not ClassDeclarationSyntaxReceiver receiver)
            {
                return;
            }

            var options = (context.Compilation as CSharpCompilation).SyntaxTrees[0].Options as CSharpParseOptions;
            var syntaxTree = CSharpSyntaxTree.ParseText(attributeSourceText, options);
            var compilation = context.Compilation.AddSyntaxTrees(syntaxTree);
            var attributeSymbol = compilation.GetTypeByMetadataName(AutoGenerateApiAttribute.FullName);

            foreach (var candidate in receiver.CandidateClasses)
            {
                var model = compilation.GetSemanticModel(candidate.SyntaxTree);
                var symbol = ModelExtensions.GetDeclaredSymbol(model, candidate);
                var attribute = symbol
                    .GetAttributes()
                    .SingleOrDefault(x => SymbolEqualityComparer.Default.Equals(x.AttributeClass, attributeSymbol));

                if (attribute is not null)
                {
                    var featureNamespace = symbol.ContainingNamespace.ContainingNamespace;

                    var attributeConstructorArgument = attribute.ConstructorArguments.SingleOrDefault();
                    if (attributeConstructorArgument.IsNull)
                    {
                        continue;
                    }

                    var entityType = compilation.GetTypeByMetadataName(attributeConstructorArgument.Value.ToString());

                    var primaryKeyProperty = attribute
                        .NamedArguments
                        .SingleOrDefault(x => x.Key == "PrimaryKey")
                        .Value;

                    var primaryKeyPropertyNames
                        = primaryKeyProperty.IsNull
                        ? Array.Empty<string>()
                        : primaryKeyProperty.Values.Select(x => x.Value.ToString()).ToArray();

                    var excludeProperty = attribute
                        .NamedArguments
                        .SingleOrDefault(x => x.Key == "Exclude")
                        .Value;

                    var excludePropertyNames
                        = excludeProperty.IsNull
                        ? Array.Empty<string>()
                        : excludeProperty.Values.Select(x => x.Value.ToString()).ToArray();

                    AddAuthorizationSource(context, featureNamespace, entityType, primaryKeyPropertyNames);
                    AddModelSource(context, featureNamespace, entityType);
                    AddSearchConditionsSource(context, featureNamespace, entityType, excludePropertyNames);
                    AddSearchSpecificationSource(context, featureNamespace, entityType, excludePropertyNames);
                    AddValidatorSource(context, featureNamespace, entityType);
                    AddUseCaseSource(context, featureNamespace, entityType);
                    AddControllerSource(context, featureNamespace, entityType, excludePropertyNames, primaryKeyPropertyNames);
                }
            }
        }
    }
}
