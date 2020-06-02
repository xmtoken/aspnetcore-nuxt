using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCoreNuxt.Domains.Data.Design
{
    /// <summary>
    /// アプリケーションを表します。
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// アプリケーションのエントリポイントを表します。
        /// </summary>
        /// <param name="args">コマンドライン引数。</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// <see cref="IHostBuilder"/> オブジェクトを作成します。
        /// </summary>
        /// <param name="args">コマンドライン引数。</param>
        /// <returns><see cref="IHostBuilder"/> オブジェクト。</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<AppDbContext>(options =>
                            {
                                options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection"), x =>
                                {
                                    x.MigrationsAssembly(typeof(Program).Assembly.FullName);
                                });
                            });
                });
    }
}
