using AspNetCoreNuxt.Extensions.AspNetCore;
using AspNetCoreNuxt.Extensions.Collections;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Models
{
    /// <inheritdoc cref="IPaging"/>
    public class Paging : IPaging
    {
        /// <summary>
        /// 1 から始まるページインデックスを取得または設定します。
        /// </summary>
        [FromQuery(Name = "index")]
        public virtual string PageIndex { get; set; }

        /// <inheritdoc cref="IPaging.PageSize"/>
        [FromQuery(Name = "size")]
        public virtual int PageSize => 30;

        /// <inheritdoc/>
        int IPaging.PageIndex
            => QueryStringConverter.ConvertToInt32(PageIndex) ?? default;
    }
}
