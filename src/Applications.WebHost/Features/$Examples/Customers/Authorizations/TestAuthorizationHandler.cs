using AspNetCoreNuxt.Applications.WebHost.Core.Authorizations;
using AspNetCoreNuxt.Domains.Entities._Examples;
using AspNetCoreNuxt.Extensions.AspNetCore.Authorization;
using AspNetCoreNuxt.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Features._Examples.Customers.Authorizations
{
    public class TestAuthorizationHandler : ExpandObjectAuthorizationHandler<OperationAuthorizationRequirement, CustomerEntity, CustomerEntity>, IDependencyInjectionService<IAuthorizationHandler>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, ExpandObjectAuthorizationResource<CustomerEntity, CustomerEntity> resource)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;

            DefaultCustomerAuthorizationHandler xx;
            if (requirement.Name == Operations.Update.Name)
            {
                if (resource.PropertyName == nameof(CustomerEntity.Id))
                {
                    context.Fail();
                    return Task.CompletedTask;
                }
            }

            if (!context.HasFailed && !context.HasSucceeded)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
