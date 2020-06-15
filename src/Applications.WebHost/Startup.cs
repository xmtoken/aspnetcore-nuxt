using AspNetCoreNuxt.Applications.WebHost.Core.Mvc;
using AspNetCoreNuxt.Domains.Data;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.Versioning.Conventions;
using AspNetCoreNuxt.Extensions.AspNetCore.Routing;
using AspNetCoreNuxt.Extensions.CsvHelper.DependencyInjection;
using AspNetCoreNuxt.Extensions.DependencyInjection;
using AspNetCoreNuxt.Extensions.EntityFrameworkCore.Metadata;
using AspNetCoreNuxt.Extensions.FluentValidation.Resources;
using AspNetCoreNuxt.Extensions.Identity;
using AspNetCoreNuxt.Extensions.Newtonsoft.Json.Converters;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NSwag;
using Serilog;
using Serilog.Events;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AspNetCoreNuxt.Applications.WebHost
{
    /// <summary>
    /// アプリケーションのスタートアップ処理を提供します。
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// <see cref="IConfiguration"/> オブジェクトを表します。
        /// </summary>
        private readonly IConfiguration Configuration;

        /// <summary>
        /// <see cref="Startup"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="configuration"><see cref="IConfiguration"/> オブジェクト。</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// サービスを構成します。
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> オブジェクト。</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices<Startup>()
                    .AddAutoMapper(typeof(Startup))
                    .AddCsvHelper<Startup>()
                    .AddEntityMetadataProvider<AppDbContext>()
                    .AddEnumLabelProviders<Startup>()
                    .AddHttpClient()
                    .AddMemoryCache()
                    .AddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>()
                    .AddSingleton<ILookupNormalizer, UpperInvariantLookupNormalizer>()
                    .AddSingleton<IStringHasher, StringHasher>()
                    .AddDbContextPool<AppDbContext>(options =>
                    {
                        options.ConfigureWarnings(x =>
                        {
                            x.Ignore(CoreEventId.SensitiveDataLoggingEnabledWarning);
                        });
                        options.EnableSensitiveDataLogging();
                        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), x => x.EnableRetryOnFailure());
                    });

            services.AddHsts(options =>
                    {
                        options.IncludeSubDomains = true;
                        options.MaxAge = TimeSpan.FromDays(365);
                        options.Preload = true;
                    });

            services.AddOpenApiDocument(settings =>
                    {
                        settings.DocumentName = typeof(Startup).Assembly.GetName().Version.ToString(3);
                        settings.Title = typeof(Startup).Namespace;
                        settings.AddOperationFilter(context =>
                        {
                            if (context.MethodInfo.GetCustomAttribute<ApiVersionNeutralAttribute>() == null)
                            {
                                context.OperationDescription.Operation.Parameters.Add(new OpenApiParameter()
                                {
                                    Kind = OpenApiParameterKind.Header,
                                    Name = "x-api-version",
                                    Default = AppApiVersion.Latest.ToVersion(),
                                });
                            }
                            return true;
                        });
                    });

            services.AddRouting(options =>
                    {
                        options.LowercaseUrls = true;
                    });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.Cookie.Name = "token";
                        options.Events = new CookieAuthenticationEvents()
                        {
                            OnRedirectToLogin = context =>
                            {
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                return Task.CompletedTask;
                            },
                            OnRedirectToAccessDenied = context =>
                            {
                                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                                return Task.CompletedTask;
                            },
                        };
                    });

            services.AddAuthorization();

            services.AddApiVersioning(options =>
                    {
                        options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
                        options.Conventions.Add(new InterleavingControllerConvention(ApiVersion.Default, typeof(Startup).Assembly));
                        options.DefaultApiVersion = ApiVersion.Default;
                        options.ReportApiVersions = true;
                    });

            services.AddControllers(options =>
                    {
                        options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
                    })
                    .AddFluentValidation(config =>
                    {
                        config.RegisterValidatorsFromAssemblyContaining<Startup>();
                        config.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                        ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
                        ValidatorOptions.LanguageManager.AddJapaneseTranslations();
                    })
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver()
                        {
                            NamingStrategy = new CamelCaseNamingStrategy()
                            {
                                ProcessDictionaryKeys = true,
                            },
                        };
                        options.SerializerSettings.Converters.Add(new EmptyStringToNullConverter());
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    })
                    .ConfigureApiBehaviorOptions(options =>
                    {
                        options.SuppressInferBindingSourcesForParameters = true;
                    });

            services.AddSpaStaticFiles(options =>
                    {
                        options.RootPath = "ClientApp/dist";
                    });
        }

        /// <summary>
        /// パイプラインを構成します。
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/> オブジェクト。</param>
        /// <param name="env"><see cref="IWebHostEnvironment"/> オブジェクト。</param>
        [SuppressMessage("Performance", "CA1822:メンバーを static に設定します")]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHsts();

            app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                app.UseOpenApi();
                app.UseSwaggerUi3(settings =>
                {
                    settings.OperationsSorter = "alpha";
                    settings.TagsSorter = "alpha";
                });
            }

            app.UseRequestLocalization(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("ja-JP");
                options.SupportedCultures = new[] { new CultureInfo("ja-JP") };
                options.SupportedUICultures = new[] { new CultureInfo("ja-JP") };
            });

            app.Use(async (context, next) =>
            {
                var scheme = context.Request.Scheme.ToUpperInvariant();
                var address = context.Connection.RemoteIpAddress;
                var username = context.User.Identity.Name;
                var useragent = context.Request.Headers["User-Agent"].ToString();
                Log.Logger.Information($"Connected {scheme} from {address} by {username} via {useragent}");

                var headers = context.Request.Headers.Where(x => x.Key != "Cookie" && x.Key != "User-Agent" && x.Key != ":method" && x.Key != ":path");
                var values = string.Join(',', headers.Select(x => $"{x.Key}:{x.Value}"));
                Log.Logger.Information($"Requested {values}");

                await next();
            });

            app.UseSerilogRequestLogging(options =>
            {
                options.GetLevel = (context, elapsed, ex) => LogEventLevel.Information;
                options.MessageTemplate = "Responded {StatusCode} in {Elapsed}ms {RequestMethod} {RequestPath}";
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpaStaticFiles();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
                }
            });
        }
    }
}
