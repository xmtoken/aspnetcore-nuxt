using AspNetCoreNuxt.Domains.Data.QueryableExtensions._Examples;
using AspNetCoreNuxt.Domains.Entities._Examples;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features._Examples.Customers.UseCases
{
    public partial class CustomerUseCase
    {
        public override Task<CustomerEntity> GetByPrimaryKeyAsync(object[] keyValues, IFieldQuery<CustomerEntity> field, CancellationToken cancellationToken = default)
        {
            var source = Context
                .Set<CustomerEntity>()
                .LeftJoin(Context.Set<ManyEntity>());
            return GetByPrimaryKeyCoreAsync(source, keyValues, field, cancellationToken);
        }
    }
}
