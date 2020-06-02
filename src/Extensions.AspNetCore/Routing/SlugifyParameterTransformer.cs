using Microsoft.AspNetCore.Routing;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Routing
{
    /// <summary>
    /// ルート値をケバブケースへ変換するトランスフォーマーを表します。
    /// </summary>
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        /// <inheritdoc/>
        public string TransformOutbound(object value)
            => value?.ToString().ToKebabCace();
    }
}
