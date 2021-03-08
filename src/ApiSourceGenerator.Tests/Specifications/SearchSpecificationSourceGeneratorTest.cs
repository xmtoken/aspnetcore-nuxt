using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using Xunit;

namespace AspNetCoreNuxt.Applications.WebHost.ApiSourceGenerator.Tests.Specifications
{
    public class SearchSpecificationSourceGeneratorTest
    {
        [Fact]
        public void Test()
        {
            string userSource = @"
using System;
namespace Root.Name.Specifications
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class AutoGenerateApiAttribute : Attribute
    {
        public Type EntityType { get; }
        public string[] Exclude { get; set; }
        public AutoGenerateApiAttribute(Type entityType)
        {
            EntityType = entityType;
        }
    }
    public class CustomerEntity
    {
        public int? Id { get; private set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    }
    [AutoGenerateApi(typeof(CustomerEntity))]
    public class CustomerSearchSpecification
    {
    }
    public class Program
    {
        public static void Main(string[] args)
        {
        }
    }
}
";
            Compilation comp = CreateCompilation(userSource);
            var newComp = RunGenerators(comp, out var generatorDiags, new SourceGenerator());

            Assert.Empty(generatorDiags);
            Assert.Empty(newComp.GetDiagnostics());
        }

        private static Compilation CreateCompilation(string source)
            => CSharpCompilation.Create("compilation",
                new[] { CSharpSyntaxTree.ParseText(source, new CSharpParseOptions(LanguageVersion.Preview)) },
                new[] { MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location) },
                new CSharpCompilationOptions(OutputKind.ConsoleApplication));

        private static GeneratorDriver CreateDriver(Compilation c, params ISourceGenerator[] generators)
            => CSharpGeneratorDriver.Create(
                ImmutableArray.Create(generators),
                ImmutableArray<AdditionalText>.Empty,
                c.SyntaxTrees.First().Options as CSharpParseOptions,
                null);

        private static Compilation RunGenerators(Compilation c, out ImmutableArray<Diagnostic> diagnostics, params ISourceGenerator[] generators)
        {
            CreateDriver(c, generators).RunGeneratorsAndUpdateCompilation(c, out var d, out diagnostics);
            return d;
        }
    }
}
