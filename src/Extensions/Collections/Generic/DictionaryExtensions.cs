using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AspNetCoreNuxt.Extensions.Collections.Generic
{
    /// <summary>
    /// <see cref="IDictionary{TKey, TValue}"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// <see cref="IReadOnlyDictionary{TKey, TValue}"/> オブジェクトを返します。
        /// </summary>
        /// <typeparam name="TKey">キーの型。</typeparam>
        /// <typeparam name="TValue">値の型。</typeparam>
        /// <param name="dictionary"><see cref="IDictionary{TKey, TValue}"/> オブジェクト。</param>
        /// <returns><see cref="IReadOnlyDictionary{TKey, TValue}"/></returns>
        public static IReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
            => new ReadOnlyDictionary<TKey, TValue>(dictionary);
    }
}
