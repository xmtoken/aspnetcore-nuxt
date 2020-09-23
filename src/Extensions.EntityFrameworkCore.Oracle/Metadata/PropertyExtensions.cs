using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.RegularExpressions;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore.Metadata
{
    /// <summary>
    /// <see cref="IProperty"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class PropertyExtensions
    {
        /// <summary>
        /// プロパティの有効桁数と小数点以下桁数を取得します。
        /// </summary>
        /// <param name="property"><see cref="IProperty"/> オブジェクト。</param>
        /// <returns>プロパティの有効桁数と小数点以下桁数。</returns>
        public static (int Precision, int Scale)? GetPrecision(this IProperty property)
        {
            var columnType = property.GetColumnType();
            if (columnType != null && Regex.Match(columnType, @"^number\(([0-9]+)(\s*,\s*[0-9]+)*\)$", RegexOptions.IgnoreCase) is Match match && match.Success)
            {
                var precision = int.Parse(match.Groups[1].Value);
                var scale = int.Parse(match.Groups[2].Value);
                return (precision, scale);
            }
            return null;
        }
    }
}
