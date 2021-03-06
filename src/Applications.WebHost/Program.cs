using AspNetCoreNuxt.Applications.WebHost.Core.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace AspNetCoreNuxt.Applications.WebHost
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
        [SuppressMessage("Design", "CA1031:一般的な例外の種類はキャッチしません")]
        public static void Main(string[] args)
        {
            Log.Logger = CreateLogger();

            try
            {
                Log.Information($"Application assembly version: {typeof(Program).Assembly.GetName().Version.ToString(3)}");
                Log.Information($"Application api version: {AppApiVersion.Latest.ToVersion()}");
                Log.Information($"Application spa version: {AppSpaVersion.Latest.ToVersion()}");

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, ex.Message);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// <see cref="IHostBuilder"/> オブジェクトを作成します。
        /// </summary>
        /// <param name="args">コマンドライン引数。</param>
        /// <returns><see cref="IHostBuilder"/> オブジェクト。</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        /// <summary>
        /// <see cref="ILogger"/> オブジェクトを作成します。
        /// </summary>
        /// <returns><see cref="ILogger"/> オブジェクト。</returns>
        private static ILogger CreateLogger()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            return new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .CreateLogger();
        }
    }
}
