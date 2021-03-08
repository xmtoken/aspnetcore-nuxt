using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Authorizations
{
    public static class Operations
    {
        public static OperationAuthorizationRequirement Create { get; } =
           new OperationAuthorizationRequirement { Name = nameof(Create) };

        public static OperationAuthorizationRequirement Delete { get; } =
            new OperationAuthorizationRequirement { Name = nameof(Delete) };

        public static OperationAuthorizationRequirement Read { get; } =
            new OperationAuthorizationRequirement { Name = nameof(Read) };

        public static OperationAuthorizationRequirement Update { get; } =
            new OperationAuthorizationRequirement { Name = nameof(Update) };
    }
}
