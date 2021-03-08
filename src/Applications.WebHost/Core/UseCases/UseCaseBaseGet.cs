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
        /// 指定された条件に一致するデータを非同期に取得します。
        /// </summary>
        /// <param name="specification"><see cref="SearchSpecification{T}"/> オブジェクト。</param>
        /// <param name="sort"><see cref="ISortQuery{T}"/> オブジェクト。</param>
        /// <param name="paging"><see cref="IPagingQuery"/> オブジェクト。</param>
        /// <param name="field"><see cref="IFieldQuery{T}"/> オブジェクト。</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> オブジェクト。</param>
        /// <returns>条件に一致するデータのコレクション。</returns>
        public virtual Task<T[]> GetAsync(SearchSpecification<T> specification, ISortQuery<T> sort, IPagingQuery paging, IFieldQuery<T> field, CancellationToken cancellationToken = default)
            => GetCoreAsync(Context.Set<T>(), specification, sort, paging, field, cancellationToken);

        /// <summary>
        /// 指定された条件に一致するデータを非同期に取得します。
        /// </summary>
        /// <param name="source"><see cref="IQueryable{T}"/> オブジェクト。</param>
        /// <param name="specification"><see cref="SearchSpecification{T}"/> オブジェクト。</param>
        /// <param name="sort"><see cref="ISortQuery{T}"/> オブジェクト。</param>
        /// <param name="paging"><see cref="IPagingQuery"/> オブジェクト。</param>
        /// <param name="field"><see cref="IFieldQuery{T}"/> オブジェクト。</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> オブジェクト。</param>
        /// <returns>条件に一致するデータのコレクション。</returns>
        protected Task<T[]> GetCoreAsync(IQueryable<T> source, SearchSpecification<T> specification, ISortQuery<T> sort, IPagingQuery paging, IFieldQuery<T> field, CancellationToken cancellationToken = default)
            => source.TryAddSpecificationQuery(specification)
                     .TryAddSortingQuery(sort)
                     .TryAddPagingQuery(paging)
                     .TryAddProjectionQuery(Mapper, field)
                     .ToArrayAsync(cancellationToken);
    }
}
