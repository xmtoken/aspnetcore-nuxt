using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreNuxt.Extensions.CsvHelper.Configuration
{
    /// <summary>
    /// <see cref="IWriterConfiguration"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class WriterConfigurationExtensions
    {
        /// <summary>
        /// 指定されたクラスのマッピングからマップ先のプロパティを示す文字列のコレクションを取得します。
        /// </summary>
        /// <typeparam name="T">マップ先のクラスの型。</typeparam>
        /// <param name="configuration"><see cref="IWriterConfiguration"/> オブジェクト。</param>
        /// <returns>マップ先のプロパティを示す文字列のコレクション。</returns>
        public static IEnumerable<string> GetMapMemberExpressions<T>(this IWriterConfiguration configuration)
        {
            var map = configuration.Maps[typeof(T)];
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
