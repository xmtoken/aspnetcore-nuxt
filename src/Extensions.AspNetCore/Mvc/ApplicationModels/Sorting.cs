using AspNetCoreNuxt.Extensions.Collections;

namespace AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels
{
    /// <inheritdoc cref="ISorting"/>
    internal class Sorting : ISorting
    {
        /// <inheritdoc/>
        public string PropertyName { get; }

        /// <inheritdoc/>
        public SortDirection SortDirection { get; }

        /// <summary>
        /// <see cref="Sorting"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="propertyName">プロパティ名。</param>
        /// <param name="sortDirection">ソート方向を表す <see cref="Collections.SortDirection"/> 値。</param>
        public Sorting(string propertyName, SortDirection sortDirection)
        {
            PropertyName = propertyName;
            SortDirection = sortDirection;
        }
    }
}
