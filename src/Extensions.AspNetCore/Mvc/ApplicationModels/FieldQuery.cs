using System;
using System.Collections.Generic;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels
{
    /// <inheritdoc cref="IFieldQuery{T}"/>
    internal class FieldQuery<T> : IFieldQuery<T>
    {
        /// <summary>
        /// バインディングするモデルの名前を表します。
        /// </summary>
        internal const string BindingModelName = "$fields";

        /// <summary>
        /// <see cref="FieldQuery{T}"/> クラスの既定のインスタンスを取得します。
        /// </summary>
        internal static FieldQuery<T> Default { get; } = new FieldQuery<T>(Array.Empty<string>(), Array.Empty<string>());

        /// <inheritdoc cref="IFieldQuery{T}.PropertyNames"/>
        private IReadOnlyCollection<string> PropertyNames { get; }

        /// <inheritdoc/>
        IEnumerable<string> IFieldQuery<T>.PropertyNames => PropertyNames;

        /// <summary>
        /// クエリストリングで指定された値を取得します。
        /// </summary>
        public IReadOnlyCollection<string> PrimitiveValues { get; }

        /// <summary>
        /// <see cref="FieldQuery{T}"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="propertyNames">取得するフィールドを表すプロパティ名のコレクション。</param>
        /// <param name="primitiveValues">クエリストリングで指定された値。</param>
        public FieldQuery(string[] propertyNames, string[] primitiveValues)
        {
            PropertyNames = Array.AsReadOnly(propertyNames);
            PrimitiveValues = Array.AsReadOnly(primitiveValues);
        }
    }
}
