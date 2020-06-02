using AspNetCoreNuxt.Extensions.AspNetCore;
using AspNetCoreNuxt.Extensions.Collections;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Models
{
    /// <inheritdoc cref="IPaging"/>
    public class Paging : IPaging
    {
        /// <inheritdoc cref="IPaging.PageIndex"/>
        [FromQuery(Name = "index")]
        public virtual string PageIndex { get; }

        /// <inheritdoc cref="IPaging.PageSize"/>
        [FromQuery(Name = "size")]
        public virtual int PageSize => 30;

        /// <inheritdoc/>
        int IPaging.PageIndex
            => QueryStringConverter.ConvertToInt32(PageIndex) ?? default;

        /// <summary>
        /// <see cref="Paging"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="pageIndex">1 から始まるページインデックス。</param>
        public Paging(string pageIndex)
        {
            PageIndex = pageIndex;
        }
    }
}
