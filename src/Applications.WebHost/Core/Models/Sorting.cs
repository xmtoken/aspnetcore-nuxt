using AspNetCoreNuxt.Extensions.AspNetCore;
using AspNetCoreNuxt.Extensions.Collections;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Models
{
    /// <inheritdoc cref="ISorting"/>
    public class Sorting : ISorting
    {
        /// <inheritdoc cref="ISorting.SortPropertyName"/>
        [FromQuery(Name = "sort")]
        public virtual string SortPropertyName { get; }

        /// <inheritdoc cref="ISorting.SortDirection"/>
        [FromQuery(Name = "sort-direction")]
        public virtual string SortDirection { get; }

        /// <inheritdoc/>
        SortDirection ISorting.SortDirection
            => QueryStringConverter.ConvertToEnum<SortDirection>(SortDirection) ?? default;

        /// <summary>
        /// <see cref="Sorting"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="sortPropertyName">並び替えのキーとなるプロパティの名前。</param>
        /// <param name="sortDirection">並び替え方向。</param>
        public Sorting(string sortPropertyName, string sortDirection)
        {
            SortPropertyName = sortPropertyName;
            SortDirection = sortDirection;
        }
    }
}
