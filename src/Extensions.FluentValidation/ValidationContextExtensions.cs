using FluentValidation;

namespace AspNetCoreNuxt.Extensions.FluentValidation
{
    public static class ValidationContextExtensions
    {
        public static ICommonContext GetParentContext(this IValidationContext context)
            => context.ParentContext;

        public static ValidationContext<T> GetParentContext<T>(this IValidationContext context)
            => (ValidationContext<T>)context.ParentContext;
    }
}
