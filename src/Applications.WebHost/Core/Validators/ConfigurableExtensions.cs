using AspNetCoreNuxt.Applications.WebHost.Core.Models;
using FluentValidation.Internal;

namespace AspNetCoreNuxt.Applications.WebHost.Core.Validators
{
    public static class ConfigurableExtensions
    {
        public static TNext ConfigureIdentifiedIndexBuilder<T, TElement, TNext>(this IConfigurable<CollectionPropertyRule<T, TElement>, TNext> configurable)
            where TElement : IIdentification
            => configurable.Configure(x => x.IndexBuilder = (_, _, model, _) => $"[{model.Identifier}]");
    }
}
