using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;
using System.Linq;

namespace AspNetCoreNuxt.Extensions.NSwag.Generation.Processors
{
    /// <summary>
    /// <see cref="IOpenApiSchema"/> インターフェイスを実装したクラスをスキーマとして追加するドキュメントプロセッサーを表します。
    /// </summary>
    /// <typeparam name="T"><see cref="IOpenApiSchema"/> インターフェイスをスキャンするアセンブリに含まれるオブジェクトの型。</typeparam>
    public class OpenApiSchemaProcessor<T> : IDocumentProcessor
    {
        /// <inheritdoc/>
        public void Process(DocumentProcessorContext context)
        {
            foreach (var type in typeof(T).Assembly.GetTypes().Where(x => x.IsClass && x.IsConcrete()))
            {
                if (typeof(IOpenApiSchema).IsAssignableFrom(type))
                {
                    context.SchemaGenerator.Generate(type, context.SchemaResolver);
                }
            }
        }
    }
}
