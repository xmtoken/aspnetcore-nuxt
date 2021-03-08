using AspNetCoreNuxt.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Authorizations
{
    public class DefaultAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement>, IDependencyInjectionService<IAuthorizationHandler>
    {
        /// <inheritdoc/>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement)
        {
            if (!context.HasFailed && !context.HasSucceeded)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
