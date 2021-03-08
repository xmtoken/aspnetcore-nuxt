using AspNetCoreNuxt.Domains.Data;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features._Examples.Customers.UseCases
{
    public partial class CustomerUseCase : CustomerUseCaseBase
    {
        public CustomerUseCase(AppDbContext context, IExpandoObjectFactory expandoObjectFactory, IMapper mapper)
            : base(context, expandoObjectFactory, mapper)
        {
        }

        public override Task RemoveAsync(object[] keyValues)
        {
            Context.SavingChanges += (sender, e) =>
            {
                var a = 0;
            };

            return RemoveCoreAsync(keyValues);
        }
    }
}
