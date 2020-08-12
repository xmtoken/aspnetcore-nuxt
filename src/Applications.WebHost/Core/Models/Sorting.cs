using AspNetCoreNuxt.Extensions.Collections;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Models
{
    /// <inheritdoc cref="ISorting"/>
    [ModelBinder(typeof(SortingModelBinder))]
    public class Sorting : ISorting
    {
        /// <summary>
        /// 並び替え方向を取得または設定します。
        /// </summary>
        public virtual SortDirection SortDirection { get; set; }

        /// <summary>
        /// 並び替えのキーとなるプロパティの名前を取得または設定します。
        /// </summary>
        public virtual string SortPropertyName { get; set; }
    }
}
