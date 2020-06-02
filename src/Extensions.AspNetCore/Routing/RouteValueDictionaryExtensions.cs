using Microsoft.AspNetCore.Routing;
using System;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Routing
{
    /// <summary>
    /// <see cref="RouteValueDictionary"/> クラスの拡張機能を提供します。
    /// </summary>
    public static class RouteValueDictionaryExtensions
    {
        /// <summary>
        /// 指定されたキーのルート値を取得します。
        /// </summary>
        /// <typeparam name="T">ルート値の型。</typeparam>
        /// <param name="routes"><see cref="RouteValueDictionary"/> オブジェクト。</param>
        /// <param name="key">キーの名前。</param>
        /// <returns>ルート値。</returns>
        public static T GetValue<T>(this RouteValueDictionary routes, string key)
            => (T)Convert.ChangeType(routes[key], typeof(T));
    }
}
