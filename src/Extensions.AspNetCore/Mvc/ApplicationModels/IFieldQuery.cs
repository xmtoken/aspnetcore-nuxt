using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels
{
    /// <summary>
    /// フィールドのクエリ情報を表します。
    /// </summary>
    /// <typeparam name="T">コンポーネントの型。</typeparam>
    public interface IFieldQuery<T>
    {
        /// <summary>
        /// 取得するフィールドを表すプロパティ名のコレクションを取得します。
        /// <list type="bullet">
        /// <item>複数のフィールドを指定する場合はカンマでプロパティ名を区切ります。</item>
        /// <item>ナビゲーションプロパティを指定する場合はピリオドでプロパティ名を区切ります。</item>
        /// </list>
        /// </summary>
        [FromQuery(Name = FieldQuery<T>.BindingModelName)]
        IEnumerable<string> PropertyNames { get; }
    }
}
