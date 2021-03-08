using AspNetCoreNuxt.Extensions.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc
{
    /// <inheritdoc cref="IExpandoObjectFactory"/>
    public class ExpandoObjectFactory : IExpandoObjectFactory
    {
        /// <summary>
        /// <see cref="IAuthorizationService"/> オブジェクトを表します。
        /// </summary>
        private readonly IAuthorizationService AuthorizationService;

        /// <summary>
        /// <see cref="IHttpContextAccessor"/> オブジェクトを表します。
        /// </summary>
        private readonly IHttpContextAccessor HttpContextAccessor;

        /// <summary>
        /// <see cref="ExpandoObjectFactory"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="authorizationService"><see cref="IAuthorizationService"/> オブジェクト。</param>
        /// <param name="httpContextAccessor"><see cref="IHttpContextAccessor"/> オブジェクト。</param>
        public ExpandoObjectFactory(IAuthorizationService authorizationService, IHttpContextAccessor httpContextAccessor)
        {
            AuthorizationService = authorizationService;
            HttpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public Task<object> CreateAsync(Type modelType, object model, IEnumerable<string> propertyNames, IEnumerable<IAuthorizationRequirement> requirements, bool includeUnauthorizedProperty, bool includeNullObject)
            => CreateCoreAsync(modelType, model, modelType, model, propertyNames, requirements, includeUnauthorizedProperty, includeNullObject);

        /// <summary>
        /// 指定されたプロパティ名のコレクションと承認要件をもとに <see cref="ExpandoObject"/> オブジェクトへ変換します。
        /// </summary>
        /// <param name="rootModelType"><see cref="ExpandoObject"/> オブジェクトへ変換するルートコンポーネントのモデルの型。</param>
        /// <param name="rootModel"><see cref="ExpandoObject"/> オブジェクトへ変換するルートコンポーネントのモデル。</param>
        /// <param name="modelType"><see cref="ExpandoObject"/> オブジェクトへ変換するモデルの型。</param>
        /// <param name="model"><see cref="ExpandoObject"/> オブジェクトへ変換するモデル。</param>
        /// <param name="propertyNames"><see cref="ExpandoObject"/> オブジェクトへ含めるプロパティ名のコレクション。</param>
        /// <param name="requirements"><see cref="ExpandObjectAuthorizationHandler{TRequirement, TRootResource, TResource}"/> クラスを実装する承認ハンドラーで検証する承認要件を表す <see cref="IAuthorizationRequirement"/> オブジェクトのコレクション。</param>
        /// <param name="includeUnauthorizedProperty">承認要件を満たさないモデルのプロパティの値を null として <see cref="ExpandoObject"/> オブジェクトのメンバーを定義する場合は true。それ以外の場合は false。</param>
        /// <param name="includeNullObject">変換するモデルに含まれるオブジェクト型のプロパティが null の場合も <see cref="ExpandoObject"/> オブジェクトのメンバーを定義する場合は true。それ以外の場合は false。</param>
        /// <returns></returns>
        private async Task<object> CreateCoreAsync(Type rootModelType, object rootModel, Type modelType, object model, IEnumerable<string> propertyNames, IEnumerable<IAuthorizationRequirement> requirements, bool includeUnauthorizedProperty, bool includeNullObject)
        {
            var includeAllProperties = propertyNames.Count() == 1 && propertyNames.Single() == "*";
            var dictionary = new ExpandoObject() as IDictionary<string, object>;
            foreach (var property in modelType.GetProperties())
            {
                if (includeAllProperties ||
                    propertyNames.Any(x => x.Equals(property.Name, StringComparison.OrdinalIgnoreCase)) ||
                    propertyNames.Any(x => x.StartsWith($"{property.Name}.", StringComparison.OrdinalIgnoreCase)))
                {
                    var authorizationResourceType = typeof(ExpandObjectAuthorizationResource<,>).MakeGenericType(rootModelType, modelType);
                    var authorizationResourceConstructor = ActivatorHelper.GetConstructor(authorizationResourceType, rootModelType, modelType, typeof(string));
                    var authorizationResource = authorizationResourceConstructor.Invoke(new object[] { rootModel, model, property.Name });
                    var authorizationResult = await AuthorizationService.AuthorizeAsync(HttpContextAccessor.HttpContext.User, authorizationResource, requirements);
                    if (authorizationResult.Succeeded || includeUnauthorizedProperty)
                    {
                        var propertyValue = model is null ? null : property.GetValue(model);
                        if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                        {
                            if (authorizationResult.Succeeded)
                            {
                                dictionary.Add(property.Name, propertyValue);
                            }
                            else
                            {
                                dictionary.Add(property.Name, null);
                            }
                        }
                        else if (propertyValue is not null || includeNullObject)
                        {
                            var prefix = $"{property.Name}.";
                            var nestedPropertyNames
                                = includeAllProperties
                                ? propertyNames
                                : propertyNames
                                    .Where(x => x.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                                    .Select(x => x.Remove(0, prefix.Length))
                                    .ToArray();
                            if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                            {
                                if (propertyValue is IEnumerable items)
                                {
                                    var genericEnumerableInterface = property.PropertyType.GetInterface(typeof(IEnumerable<>).Name);
                                    var list = new List<object>();
                                    foreach (var item in items)
                                    {
                                        var itemType = genericEnumerableInterface?.GenericTypeArguments[0] ?? item?.GetType();
                                        var itemValue = await CreateCoreAsync(
                                            rootModelType,
                                            rootModel,
                                            itemType ?? typeof(object),
                                            item,
                                            nestedPropertyNames,
                                            requirements,
                                            includeUnauthorizedProperty,
                                            includeNullObject);
                                        list.Add(itemValue);
                                    }
                                    dictionary.Add(property.Name, list);
                                }
                                else
                                {
                                    dictionary.Add(property.Name, Array.Empty<object>());
                                }
                            }
                            else
                            {
                                var value = await CreateCoreAsync(
                                    rootModelType,
                                    rootModel,
                                    property.PropertyType,
                                    propertyValue,
                                    nestedPropertyNames,
                                    requirements,
                                    includeUnauthorizedProperty,
                                    includeNullObject);
                                dictionary.Add(property.Name, value);
                            }
                        }
                    }
                }
            }
            return dictionary;
        }
    }
}
