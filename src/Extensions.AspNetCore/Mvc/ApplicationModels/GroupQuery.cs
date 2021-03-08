using System;
using System.Collections.Generic;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels
{
    /// <inheritdoc cref="IGroupQuery{T}"/>
    internal class GroupQuery<T> : IGroupQuery<T>
    {
        /// <summary>
        /// バインディングするモデルの名前を表します。
        /// </summary>
        internal const string BindingModelName = "$group";

        /// <summary>
        /// <see cref="GroupQuery{T}"/> クラスの既定のインスタンスを取得します。
        /// </summary>
        internal static GroupQuery<T> Default { get; } = new GroupQuery<T>(Array.Empty<string>(), Array.Empty<string>());

        /// <inheritdoc cref="IGroupQuery{T}.PropertyNames"/>
        public IReadOnlyCollection<string> PropertyNames { get; }

        /// <inheritdoc/>
        IEnumerable<string> IGroupQuery<T>.PropertyNames => PropertyNames;

        /// <summary>
        /// クエリストリングで指定された値を取得します。
        /// </summary>
        public IReadOnlyCollection<string> PrimitiveValues { get; }

        /// <summary>
        /// <see cref="GroupQuery{T}"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="propertyNames">グルーピングするフィールドを表すプロパティ名のコレクション。</param>
        /// <param name="primitiveValues">クエリストリングで指定された値。</param>
        public GroupQuery(string[] propertyNames, string[] primitiveValues)
        {
            PropertyNames = Array.AsReadOnly(propertyNames);
            PrimitiveValues = Array.AsReadOnly(primitiveValues);
        }
    }
}
