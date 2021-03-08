using AspNetCoreNuxt.Extensions.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc
{
    /// <summary>
    /// <see cref="IExpandoObjectFactory"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    public static class ExpandoObjectFactoryExtensions
    {
        /// <summary>
        /// 指定されたプロパティ名のコレクションと承認要件をもとに <see cref="ExpandoObject"/> オブジェクトへ変換します。
        /// </summary>
        /// <param name="factory"><see cref="IExpandoObjectFactory"/> オブジェクト。</param>
        /// <param name="modelType"><see cref="ExpandoObject"/> オブジェクトへ変換するモデルの型。</param>
        /// <param name="model"><see cref="ExpandoObject"/> オブジェクトへ変換するモデル。</param>
        /// <param name="propertyNames"><see cref="ExpandoObject"/> オブジェクトへ含めるプロパティ名のコレクション。</param>
        /// <returns><see cref="ExpandoObject"/> オブジェクト。</returns>
        public static Task<object> CreateAsync(this IExpandoObjectFactory factory, Type modelType, object model, IEnumerable<string> propertyNames)
            => factory.CreateAsync(modelType, model, propertyNames, Array.Empty<IAuthorizationRequirement>(), includeUnauthorizedProperty: true, includeNullObject: false);

        /// <summary>
        /// 指定されたプロパティ名のコレクションと承認要件をもとに <see cref="ExpandoObject"/> オブジェクトへ変換します。
        /// </summary>
        /// <param name="factory"><see cref="IExpandoObjectFactory"/> オブジェクト。</param>
        /// <param name="modelType"><see cref="ExpandoObject"/> オブジェクトへ変換するモデルの型。</param>
        /// <param name="model"><see cref="ExpandoObject"/> オブジェクトへ変換するモデル。</param>
        /// <param name="propertyNames"><see cref="ExpandoObject"/> オブジェクトへ含めるプロパティ名のコレクション。</param>
        /// <param name="requirement"><see cref="ExpandObjectAuthorizationHandler{TRequirement, TRootResource, TResource}"/> クラスを実装する承認ハンドラーで検証する承認要件を表す <see cref="IAuthorizationRequirement"/> オブジェクト。</param>
        /// <param name="includeUnauthorizedProperty">承認要件を満たさないモデルのプロパティの値を null として <see cref="ExpandoObject"/> オブジェクトのメンバーを定義する場合は true。それ以外の場合は false。</param>
        /// <param name="includeNullObject">変換するモデルに含まれるオブジェクト型のプロパティが null の場合も <see cref="ExpandoObject"/> オブジェクトのメンバーを定義する場合は true。それ以外の場合は false。</param>
        /// <returns><see cref="ExpandoObject"/> オブジェクト。</returns>
        public static Task<object> CreateAsync(this IExpandoObjectFactory factory, Type modelType, object model, IEnumerable<string> propertyNames, IAuthorizationRequirement requirement, bool includeUnauthorizedProperty, bool includeNullObject)
            => factory.CreateAsync(modelType, model, propertyNames, new[] { requirement }, includeUnauthorizedProperty, includeNullObject);
    }
}
