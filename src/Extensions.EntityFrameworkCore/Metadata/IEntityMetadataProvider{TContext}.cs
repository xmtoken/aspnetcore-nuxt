using Microsoft.EntityFrameworkCore;

namespace AspNetCoreNuxt.Extensions.EntityFrameworkCore.Metadata
{
    /// <inheritdoc cref="IEntityMetadataProvider"/>
    /// <typeparam name="TContext">メタデータを取得する <see cref="DbContext"/> クラスの型。</typeparam>
    public interface IEntityMetadataProvider<TContext> : IEntityMetadataProvider
        where TContext : DbContext
    {
    }
}
