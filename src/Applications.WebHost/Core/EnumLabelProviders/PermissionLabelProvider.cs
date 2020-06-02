using AspNetCoreNuxt.Domains;
using AspNetCoreNuxt.Extensions;

namespace AspNetCoreNuxt.Applications.WebHost.Core.EnumLabelProviders
{
    /// <summary>
    /// <see cref="Permission"/> 値に対応するラベルテキストを提供するプロバイダーを表します。
    /// </summary>
    public class PermissionLabelProvider : EnumLabelProvider<Permission>
    {
        /// <inheritdoc/>
        public override string CreateLabel(Permission value) => value switch
        {
            Permission.Default => "通常",
            _ => null,
        };
    }
}
