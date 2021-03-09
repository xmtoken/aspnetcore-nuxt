using AspNetCoreNuxt.Applications.WebHost.Core.Mvc;
using AspNetCoreNuxt.Applications.WebHost.Core.Profiles;
using AspNetCoreNuxt.Domains.Data;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ApplicationModels;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.ModelBinding;
using AspNetCoreNuxt.Extensions.AspNetCore.Mvc.Versioning.Conventions;
using AspNetCoreNuxt.Extensions.AspNetCore.Routing;
using AspNetCoreNuxt.Extensions.DependencyInjection;
using AspNetCoreNuxt.Extensions.EntityFrameworkCore.Metadata;
using AspNetCoreNuxt.Extensions.FluentValidation;
using AspNetCoreNuxt.Extensions.FluentValidation.Resources;
using AspNetCoreNuxt.Extensions.Newtonsoft.Json.Converters;
using AspNetCoreNuxt.Extensions.NSwag.Generation.Processors;
using AspNetCoreNuxt.Extensions.OpenXml.Abstractions.Excel;
using AspNetCoreNuxt.Infrastructures.OpenXml.ClosedXml.Excel;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Internal;
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
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NJsonSchema.Annotations;
using NSwag;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static AspNetCoreNuxt.Applications.WebHost.Core.Profiles.EntityProfile;

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

        public class NodaLocalDateTimeTypeConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(IEnumerable<string>) || sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
            }
        }

        /// <summary>
        /// サービスを構成します。
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> オブジェクト。</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IExpandoObjectFactory, ExpandoObjectFactory>();

            services.AddDependencyInjectionServices<Startup>()
                    .AddAutoMapper(
                    (services, config) =>
                    {
                        //config.AddCollectionMappers();
                        //config.UseEntityFrameworkCoreModel<AppDbContext>(context.Model);

                        GenericEnumerableExpressionBinder.InsertTo(config.Advanced.QueryableBinders);
                        using var scope = services.CreateScope();
                        var context = scope.ServiceProvider.GetService<AppDbContext>();
                        config.AddProfile(new EntityProfile(context));
                    }, Array.Empty<Type>())
                    .AddEntityMetadataProvider<AppDbContext>();

            services.AddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>()
                    .AddSingleton<ILookupNormalizer, UpperInvariantLookupNormalizer>()
                    //.AddSingleton<IStringHasher, StringHasher>()
                    .AddSingleton<IWorkbookFactory, WorkbookFactory>();

            services.AddHttpClient()
                    .AddLocalization()
                    .AddMemoryCache();

            services.AddDbContextPool<AppDbContext>(options =>
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
                        // https://github.com/RicoSuter/NSwag/issues/2632
                        settings.DefaultEnumHandling = NJsonSchema.Generation.EnumHandling.String;

                        // https://github.com/RicoSuter/NSwag/issues/2404#issuecomment-767649517
                        var supportedTypes = new[]
                        {
                            typeof(bool),
                            typeof(byte),
                            typeof(char),
                            typeof(decimal),
                            typeof(double),
                            typeof(float),
                            typeof(int),
                            typeof(long),
                            typeof(sbyte),
                            typeof(short),
                            typeof(string),
                            typeof(uint),
                            typeof(ulong),
                            typeof(ushort),
                            typeof(DateTime),
                            typeof(Guid),
                            typeof(TimeSpan),
                        };
                        foreach (var supportedType in supportedTypes)
                        {
                            var type = typeof(SearchConditions<>).MakeGenericType(supportedType);
                            var attribute = new JsonSchemaTypeAttribute(typeof(IEnumerable<string>));
                            //TypeDescriptor.AddAttributes(type, attribute);

                            //TypeDescriptor.AddAttributes(type, new System.ComponentModel.ProvidePropertyAttribute("", typeof(string)));

                        }

                        settings.DocumentName = typeof(Startup).Assembly.GetName().Version.ToString(3);
                        settings.DocumentProcessors.Add(new OpenApiSchemaProcessor<Startup>());
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
                        options.Cookie.HttpOnly = true;
                        options.Cookie.SameSite = SameSiteMode.Lax;
                        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                        options.Events = new CookieAuthenticationEvents()
                        {
                            // TODO:cookie name with port
                            //OnSigningIn = context =>
                            //{
                            //    var port = context.Request.Host.Port;
                            //    context.Options.Cookie.Name = port.HasValue ? $"token-{context.Request.Host.Port}" : "token";
                            //    return Task.CompletedTask;
                            //},
                            //OnValidatePrincipal = context =>
                            //{
                            //    var port = context.Request.Host.Port;
                            //    context.Options.Cookie.Name = port.HasValue ? $"token-{context.Request.Host.Port}" : "token";
                            //    return Task.CompletedTask;
                            //},
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
                        options.ModelBinderProviders.AddQueryModelBinderProviders();
                    })
                    .AddFluentValidation(config =>
                    {
                        // https://github.com/FluentValidation/FluentValidation/issues/863#issuecomment-412996694
                        // onにすればIEnumerableでルートの要素がvalidator実行される
                        // しかしrulesetがとばない
                        //config.ImplicitlyValidateRootCollectionElements = true;

                        config.AutomaticValidationEnabled = false;
                        config.RegisterValidatorsFromAssemblyContaining<Startup>();
                        config.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
#pragma warning disable CS0618
                        ValidatorOptions.Global.CascadeMode = CascadeMode.StopOnFirstFailure;
#pragma warning restore CS0618
                        ValidatorOptions.Global.LanguageManager.AddJapaneseTranslations();

                        // camelにすると[CustomizeValidator(Properties = "name")]が面倒
                        // camelにしたのはネストインデックスのためだったか？
                        // ネストのプロパティをキャメルにするため
                        // CustomizeValidator[Rulesetでいくなら有効でよき
                        ValidatorOptions.Global.PropertyNameResolver = CamelCaseNaming.PropertyNameResolver;

                        // https://github.com/FluentValidation/FluentValidation/issues/374#issuecomment-268112662
                        // 子バリデーターにrulesetを伝播させる
                        ValidatorOptions.Global.ValidatorSelectors.RulesetValidatorSelectorFactory = rulesets =>
                        {
                            var rulesetIncludingDefaultFallbackForChildProperties = rulesets.Union(new[] { "default" }).ToArray();
                            var rulesetValidatorSelector = new RulesetValidatorSelector(rulesetIncludingDefaultFallbackForChildProperties);
                            return rulesetValidatorSelector;
                        };
                    })
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver()
                        {
#pragma warning disable CS0618
                            DefaultMembersSearchFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
#pragma warning restore CS0618
                            NamingStrategy = new CamelCaseNamingStrategy()
                            {
                                ProcessDictionaryKeys = true,
                            },
                        };
                        options.SerializerSettings.Converters.Add(new EmptyStringToNullConverter());
                        options.SerializerSettings.Converters.Add(new StringEnumConverter());
                        //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
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
                options.DefaultRequestCulture = new RequestCulture("ja");
                options.SupportedCultures = new[] { new CultureInfo("ja") };
                options.SupportedUICultures = new[] { new CultureInfo("ja") };
            });

            app.UseSerilogRequestLogging(options =>
            {
                options.GetLevel = (context, elapsed, ex) => LogEventLevel.Information;
                options.MessageTemplate = "Responded {StatusCode} in {Elapsed}ms {RequestMethod} {RequestPath}";
            });

            app.UseRouting();

            app.UseAuthentication();

            app.Use(async (context, next) =>
            {
                var scheme = context.Request.Scheme.ToUpperInvariant();
                var address = context.Connection.RemoteIpAddress;
                var username = context.User.Identity.Name;
                var useragent = context.Request.Headers["User-Agent"].ToString();
                Log.Logger.Information($"Connected {scheme} from {address} by {username} via {useragent}");

                var headers = context.Request.Headers.Where(x => x.Key != "Cookie" && x.Key != "User-Agent" && x.Key != ":method" && x.Key != ":path");
                var values = string.Join(',', headers.Select(x => $"`{x.Key}:{x.Value}`"));
                Log.Logger.Information($"Requested {values}");

                await next();
            });

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
