using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels
{
    /// <summary>
    /// ソートのクエリ情報を表します。
    /// </summary>
    /// <typeparam name="T">コンポーネントの型。</typeparam>
    public interface ISortQuery<T>
    {
        /// <summary>
        /// ソートするフィールドを表すプロパティ名のコレクションを取得します。
        /// <list type="bullet">
        /// <item>複数のフィールドを指定する場合はカンマでプロパティ名を区切ります。</item>
        /// <item>ナビゲーションプロパティを指定する場合はピリオドでプロパティ名を区切ります。</item>
        /// <item>コレクション型のナビゲーションプロパティには対応していません。</item>
        /// <item>昇順でソートする場合はプロパティ名の前に + を付けます。</item>
        /// <item>降順でソートする場合はプロパティ名の前に - を付けます。</item>
        /// </list>
        /// </summary>
        /// <remarks>
        /// <see cref="SortQueryModelBinder{T}"/> クラスによって <see cref="ISorting"/> オブジェクトへ変換され
        /// <see cref="SortQuery{T}.Sortings"/> プロパティへ設定されるため <see cref="PropertyNames"/> プロパティにはソート方向を表す文字列は含まれません。
        /// </remarks>
        [FromQuery(Name = SortQuery<T>.BindingModelName)]
        IEnumerable<string> PropertyNames { get; }

        /// <summary>
        /// ソートを適用するメソッドを取得します。
        /// </summary>
        /// <returns>ソートを適用するメソッド。</returns>
        Func<IQueryable<T>, IQueryable<T>> GetSortMethod();
    }
}
