using AspNetCoreNuxt.Domains.Data.QueryableExtensions._Examples;
using AspNetCoreNuxt.Domains.Entities._Examples;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features._Examples.Customers.UseCases
{
    public partial class CustomerUseCase
    {
        public override Task<CustomerEntity[]> GetAsync(SearchSpecification<CustomerEntity> specification, ISortQuery<CustomerEntity> sorting, IPagingQuery paging, IFieldQuery<CustomerEntity> field, CancellationToken cancellationToken = default)
        {
            var source = Context
                .Set<CustomerEntity>()
                .LeftJoin(Context.Set<ManyEntity>());

            return GetCoreAsync(source, specification, sorting, paging, field, cancellationToken);
        }
    }
}
