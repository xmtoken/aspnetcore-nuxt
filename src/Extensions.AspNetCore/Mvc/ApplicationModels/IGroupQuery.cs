using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels
{
    /// <summary>
    /// グルーピングのクエリ情報を表します。
    /// </summary>
    /// <typeparam name="T">コンポーネントの型。</typeparam>
    public interface IGroupQuery<T>
    {
        /// <summary>
        /// グルーピングするフィールドを表すプロパティ名のコレクションを取得します。
        /// <list type="bullet">
        /// <item>複数のフィールドを指定する場合はカンマでプロパティ名を区切ります。</item>
        /// <item>ナビゲーションプロパティには対応していません。</item>
        /// <item>最大で 7 つのフィールドでグルーピングできます。</item>
        /// </list>
        /// </summary>
        [FromQuery(Name = GroupQuery<T>.BindingModelName)]
        IEnumerable<string> PropertyNames { get; }
    }
}
