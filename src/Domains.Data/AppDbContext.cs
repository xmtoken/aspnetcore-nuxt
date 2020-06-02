using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AspNetCoreNuxt.Domains.Data
{
    /// <summary>
    /// アプリケーションのデータベースコンテキストを表します。
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// <see cref="AppDbContext"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="options"><see cref="DbContextOptions{TContext}"/> オブジェクト。</param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}
