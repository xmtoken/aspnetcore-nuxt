using AspNetCoreNuxt.Domains;
using AspNetCoreNuxt.Extensions;

namespace AspNetCoreNuxt.Applications.WebHost.Core.EnumLabelProviders
{
    /// <summary>
    /// <see cref="Gender"/> 値に対応するラベルテキストを提供するプロバイダーを表します。
    /// </summary>
    public class GenderLabelProvider : EnumLabelProvider<Gender>
    {
        /// <inheritdoc/>
        public override string CreateLabel(Gender value) => value switch
        {
            Gender.None => "未設定",
            Gender.Male => "男性",
            Gender.Female => "女性",
            _ => null,
        };
    }
}
