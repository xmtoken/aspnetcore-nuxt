using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreNuxt.Extensions.CsvHelper.Configuration
{
    /// <summary>
    /// <see cref="ClassMap"/> クラスの拡張機能を提供します。
    /// </summary>
    public static class ClassMapExtensions
    {
        /// <summary>
        /// 指定されたクラスのマッピングからマップ先のプロパティを示す文字列のコレクションを取得します。
        /// </summary>
        /// <param name="map"><see cref="ClassMap"/> オブジェクト。</param>
        /// <returns>マップ先のプロパティを示す文字列のコレクション。</returns>
        public static IEnumerable<string> GetMapMemberExpressions(this ClassMap map)
        {
            foreach (var expression in GetMapMemberExpressionsInternal(map))
            {
                yield return expression;
            }
        }

        /// <summary>
        /// 指定された <see cref="ClassMap"/> オブジェクトからマップ先のプロパティを示す文字列のコレクションを取得します。
        /// </summary>
        /// <param name="map"><see cref="ClassMap"/> オブジェクト。</param>
        /// <param name="prefix">プロパティを示す文字列に付与するプレフィックス。</param>
        /// <returns>マップ先のプロパティを示す文字列のコレクション。</returns>
        private static IEnumerable<string> GetMapMemberExpressionsInternal(ClassMap map, string prefix = null)
        {
            var prefixWithSeparator = prefix == null ? string.Empty : prefix + ".";

            foreach (var memberMap in map.MemberMaps.Where(x => x.Data.Member != null))
            {
                yield return prefixWithSeparator + memberMap.Data.Member.Name;
            }

            foreach (var referenceMap in map.ReferenceMaps)
            {
                var referenceMapPrefix = prefixWithSeparator + referenceMap.Data.Member.Name;
                foreach (var expression in GetMapMemberExpressionsInternal(referenceMap.Data.Mapping, referenceMapPrefix))
                {
                    yield return expression;
                }
            }
        }
    }
}
