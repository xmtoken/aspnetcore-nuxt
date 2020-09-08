using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;
using System.Linq;

namespace AspNetCoreNuxt.Extensions.NSwag.Generation.Processors
{
    /// <summary>
    /// <see cref="ISchema"/> インターフェイスを実装したクラスをスキーマとして追加するドキュメントプロセッサーを表します。
    /// </summary>
    /// <typeparam name="T"><see cref="ISchema"/> インターフェイスをスキャンするアセンブリに含まれるオブジェクトの型。</typeparam>
    public class SchemaProcessor<T> : IDocumentProcessor
    {
        /// <inheritdoc/>
        public void Process(DocumentProcessorContext context)
        {
            foreach (var type in typeof(T).Assembly.GetTypes().Where(x => typeof(ISchema).IsAssignableFrom(x)))
            {
                context.SchemaGenerator.Generate(type, context.SchemaResolver);
            }
        }
    }
}
