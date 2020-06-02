using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace AspNetCoreNuxt.Extensions.AutoMapper
{
    /// <summary>
    /// <see cref="IQueryable{T}"/> インターフェイスの拡張機能を提供します。
    /// </summary>
    [SuppressMessage("Naming", "CA1724:型名は名前空間と同一にすることはできません")]
    public static class QueryableExtensions
    {
        /// <summary>
        /// 指定された情報をもとにシーケンスを <typeparamref name="T"/> 型へ射影します。
        /// </summary>
        /// <typeparam name="T">シーケンスの要素の型。</typeparam>
        /// <param name="source"><see cref="IQueryable{T}"/> オブジェクト。</param>
        /// <param name="configuration"><see cref="IConfigurationProvider"/> オブジェクト。</param>
        /// <param name="membersToExpand">射影する項目のプロパティを示す文字列のコレクション。</param>
        /// <returns><see cref="IQueryable{T}"/> オブジェクト。</returns>
        public static IQueryable<T> ProjectTo<T>(this IQueryable source, IConfigurationProvider configuration, IEnumerable<string> membersToExpand)
        {
            var expressions = GenerateExpressions<T>(membersToExpand).ToArray();
            return source.ProjectTo<T>(configuration, parameters: null, membersToExpand: expressions);
        }

        /// <summary>
        /// 指定されたプロパティを示す文字列を検証し、有効なパラメーター値をプロパティ名の大文字と小文字の違いを整形し返します。
        /// </summary>
        /// <typeparam name="T">プロパティのトップレベルのオブジェクトの型。</typeparam>
        /// <param name="propertyNames">プロパティを示す文字列のコレクション。</param>
        /// <returns>プロパティを示す文字列のコレクション。</returns>
        private static IEnumerable<string> GenerateExpressions<T>(IEnumerable<string> propertyNames)
        {
            if (propertyNames == null)
            {
                yield break;
            }

            foreach (var propertyName in propertyNames)
            {
                if (ValidateExpression<T>(propertyName, out var expression))
                {
                    yield return expression;
                }
            }
        }

        /// <summary>
        /// 指定されたプロパティを示す文字列を検証し、プロパティ名の大文字と小文字の違いを整形します。
        /// </summary>
        /// <typeparam name="T">プロパティのトップレベルのオブジェクトの型。</typeparam>
        /// <param name="propertyName">プロパティを示す文字列。</param>
        /// <param name="expression">プロパティ名の大文字と小文字の違いを整形したプロパティを示す文字列。</param>
        /// <returns>検証に成功した場合は true。それ以外の場合は false。</returns>
        private static bool ValidateExpression<T>(string propertyName, out string expression)
        {
            expression = null;

            if (string.IsNullOrEmpty(propertyName))
            {
                return false;
            }

            var type = typeof(T);
            var properties = new List<string>();
            foreach (var name in propertyName.Split('.'))
            {
                var property = type.GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                if (property == null)
                {
                    return false;
                }

                var genericEnumerableInterface = GetGenericEnumerableInterface(property.PropertyType);
                if (genericEnumerableInterface == null)
                {
                    type = property.PropertyType;
                }
                else
                {
                    type = genericEnumerableInterface.GenericTypeArguments[0];
                }

                properties.Add(property.Name);
            }

            expression = string.Join('.', properties);
            return true;

            static Type GetGenericEnumerableInterface(Type type)
            {
                if (type == typeof(string))
                {
                    return null;
                }

                if (type.IsInterface && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return type;
                }

                return type.GetInterfaces().SingleOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));
            }
        }
    }
}
