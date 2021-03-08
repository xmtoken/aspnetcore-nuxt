using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels
{
    /// <summary>
    /// 集計のクエリ情報を表します。
    /// </summary>
    /// <typeparam name="T">コンポーネントの型。</typeparam>
    public interface IAggregateQuery<T>
    {
        /// <summary>
        /// 集計するフィールドを表すプロパティ名のコレクションを取得します。
        /// <list type="bullet">
        /// <item>複数のフィールドを指定する場合はカンマでプロパティ名を区切ります。</item>
        /// <item>ナビゲーションプロパティには対応していません。</item>
        /// <item>平均値を集計する場合はプロパティ名の前に avg: を付けます。</item>
        /// <item>最大値を集計する場合はプロパティ名の前に max: を付けます。</item>
        /// <item>最小値を集計する場合はプロパティ名の前に min: を付けます。</item>
        /// <item>合計値を集計する場合はプロパティ名の前に sum: を付けます。</item>
        /// </list>
        /// </summary>
        /// <remarks>
        /// <see cref="AggregateQueryModelBinder{T}"/> クラスによって <see cref="IAggregation"/> オブジェクトへ変換され
        /// <see cref="AggregateQuery{T}.Aggregations"/> プロパティへ設定されるため <see cref="PropertyNames"/> プロパティには集計演算子を表す文字列は含まれません。
        /// </remarks>
        [FromQuery(Name = AggregateQuery<T>.BindingModelName)]
        IEnumerable<string> PropertyNames { get; }

        /// <summary>
        /// <see cref="IAggregation"/> オブジェクトのコレクションを取得します。
        /// </summary>
        /// <returns><see cref="IAggregation"/> オブジェクトのコレクション。</returns>
        IEnumerable<IAggregation> GetAggregations();
    }
}
