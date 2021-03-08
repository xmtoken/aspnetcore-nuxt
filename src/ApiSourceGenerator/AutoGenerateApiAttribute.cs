namespace AspNetCoreNuxt.ApiSourceGenerator
{
    public class AutoGenerateApiAttribute
    {
        public static string FullName => $"{Namespace}.{Name}";

        public static string Name => nameof(AutoGenerateApiAttribute);

        public static string Namespace => typeof(AutoGenerateApiAttribute).Namespace;

        public static string SourceText => @$"
using System;
namespace {Namespace}
{{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class {Name} : Attribute
    {{
        public Type EntityType {{ get; }}
        public string[] PrimaryKey {{ get; set; }}
        public string[] Exclude {{ get; set; }}
        public {Name}(Type entityType)
        {{
            EntityType = entityType;
        }}
    }}
}}";
    }
}
