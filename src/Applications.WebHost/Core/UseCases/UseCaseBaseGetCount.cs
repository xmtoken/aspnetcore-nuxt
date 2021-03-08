using AspNetCoreNuxt.Applications.WebHost.Core.Linq;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Core.UseCases
{
    public partial class UseCaseBase<T>
    {
        /// <summary>
        /// 指定された条件に一致するデータの件数を非同期に取得します。
        /// </summary>
        /// <param name="specification"><see cref="SearchSpecification{T}"/> オブジェクト。</param>
        /// <param name="paging"><see cref="IPagingQuery"/> オブジェクト。</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> オブジェクト。</param>
        /// <returns>条件に一致するデータの件数。</returns>
        public virtual Task<int> GetCountAsync(SearchSpecification<T> specification, IPagingQuery paging, CancellationToken cancellationToken = default)
            => GetCountCoreAsync(Context.Set<T>(), specification, paging, cancellationToken);

        /// <summary>
        /// 指定された条件に一致するデータの件数を非同期に取得します。
        /// </summary>
        /// <param name="source"><see cref="IQueryable{T}"/> オブジェクト。</param>
        /// <param name="specification"><see cref="SearchSpecification{T}"/> オブジェクト。</param>
        /// <param name="paging"><see cref="IPagingQuery"/> オブジェクト。</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> オブジェクト。</param>
        /// <returns>条件に一致するデータの件数。</returns>
        protected Task<int> GetCountCoreAsync(IQueryable<T> source, SearchSpecification<T> specification, IPagingQuery paging, CancellationToken cancellationToken = default)
            => source.TryAddSpecificationQuery(specification)
                     .TryAddPagingQuery(paging)
                     .CountAsync(cancellationToken);
    }
}
