using AspNetCoreNuxt.Extensions.AspNetCore;
using AspNetCoreNuxt.Extensions.Collections;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Models
{
    /// <inheritdoc cref="ISorting"/>
    public class Sorting : ISorting
    {
        /// <summary>
        /// 並び替え方向を取得または設定します。
        /// </summary>
        [FromQuery(Name = "sort")]
        public virtual string SortPropertyName { get; set; }

        /// <summary>
        /// 並び替えのキーとなるプロパティの名前を取得または設定します。
        /// </summary>
        [FromQuery(Name = "sort-direction")]
        public virtual string SortDirection { get; set; }

        /// <inheritdoc/>
        SortDirection ISorting.SortDirection
            => QueryStringConverter.ConvertToEnum<SortDirection>(SortDirection) ?? default;
    }
}
