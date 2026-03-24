using Asp.Versioning;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using NRC.Const.CodesAPI.API.Auth;
using NRC.Const.CodesAPI.Application.Abstractions.PublicReview;
using NRC.Const.CodesAPI.API.Middleware;
using NRC.Const.CodesAPI.Application.Interfaces;
using NRC.Const.CodesAPI.Application.Services;
using NRC.Const.CodesAPI.Domain.Auth;
using NRC.Const.CodesAPI.Infrastructure.Persistence;
using NRC.Const.CodesAPI.Infrastructure.Persistence.DbContexts;
using NRC.Const.CodesAPI.Infrastructure.Persistence.Outbox.UnitOfWork;
using NRC.Const.CodesAPI.Infrastructure.Persistence.Outbox.Writers;
using NRC.Const.CodesAPI.Infrastructure.Repositories;
using NRC.Const.CodesAPI.Infrastructure.Services;
using NRC.Const.CodesAPI.Infrastructure.Request;
using NRC.Const.CodesAPI.Infrastructure.Services.Repositories;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using IAuthorizationService = NRC.Const.CodesAPI.Domain.Auth.IAuthorizationService;
using NRC.Const.CodesAPI.Infrastructure.Audit;
using NRC.Const.CodesAPI.API.Profiles;
using NRC.Const.CodesAPI.Infrastructure.Extensions;
using NRC.Const.CodesAPI.Application.DTOs.InterfaceDTOs.RequestParameters.CodesSavedSearches;

namespace NRC.Const.CodesAPI.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configure Serilog early to catch startup errors
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            // Use Serilog for logging
            ConfigureSerilog(builder);

            // Register core services
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            ConfigureControllers(builder.Services);
            ConfigureAuth(builder.Services, builder.Configuration);
            ConfigureApiVersioning(builder.Services);
            ConfigureSwagger(builder.Services);
            ConfigureDataAccess(builder);

            builder.Services.AddProblemDetails();
            builder.Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            var app = builder.Build();

            // Configure middleware pipeline
            ConfigureMiddleware(app, builder);
            app.Run();
        }

        static void ConfigureSerilog(WebApplicationBuilder builder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            builder.Host.UseSerilog((context, loggerConfig) =>
            {
                loggerConfig.MinimumLevel.Debug().WriteTo.Console();

                if (environment != Environments.Development)
                {
                    loggerConfig.WriteTo.ApplicationInsights(new TelemetryConfiguration
                    {
                        InstrumentationKey = builder.Configuration["ApplicationInsightsInstrumentationKey"]
                    }, TelemetryConverter.Traces);
                }
            });
        }

        static void ConfigureControllers(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = false;
            })
            .AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            })
            .AddNewtonsoftJson(opts =>
            {
                opts.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            })
            .AddXmlDataContractSerializerFormatters();
        }

        static void ConfigureAuth(IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(config.GetSection("AzureAd"));
            services.AddAuthorization();

            services.AddSingleton<IAuthorizationPolicyProvider, DynamicAuthorizationPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, DynamicRoleAuthorizationHandler>();
        }

        static void ConfigureApiVersioning(IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            })
            .AddMvc()
            .AddApiExplorer(options => options.SubstituteApiVersionInUrl = true);
        }

        static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
            services.AddSwaggerGen();
        }

        static void ConfigureDataAccess(WebApplicationBuilder builder)
        {
            string connection = builder.Configuration["ConnectionStrings:CodesDBConnectionString"];

            builder.Services.AddDbContext<VariationsDbContext>(o => o.UseSqlServer(connection));
            builder.Services.AddScoped<IVariationsRepository, VariationsRepository>();

            builder.Services.AddDbContext<CommitteeDbContext>(o => o.UseSqlServer(connection));
            builder.Services.AddScoped<ICommitteeRepository, CommitteeRepository>();

            builder.Services.AddDbContext<UserResourceDbContext>(o => o.UseSqlServer(connection));
            builder.Services.AddScoped<IResourcesRepository, ResourcesRepository>();

            builder.Services.AddDbContext<PublicReviewDbContext>(o => o.UseSqlServer(connection));
            builder.Services.AddScoped<IPublicReviewRepository, PublicReviewRepository>();

            builder.Services.AddDbContext<CodesAuditLogDbContext>(o => o.UseSqlServer(connection));
            builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            builder.Services.AddScoped<IAuditLogService, AuditLogService>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddDbContext<PublicReviewDbContext>(o => o.UseSqlServer(connection));
            builder.Services.AddScoped<IPublicReviewRepository, PublicReviewRepository>();
            builder.Services.AddScoped<IPublicReviewService, PublicReviewService>();
            builder.Services.AddAuditableDbContext<PublicReviewCommentDbContext>(connection);

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton(TimeProvider.System);
            builder.Services.AddScoped<AuditInterceptor>();
            
            builder.Services.AddDbContext<CodesPublicReviewCommenterDbContext>(o => o.UseSqlServer(connection));
            
            builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            builder.Services.AddScoped<IAuditLogService, AuditLogService>();
            builder.Services.AddAutoMapper(typeof(CodesAuditLogProfile).Assembly);

            builder.Services.AddScoped<IPublicReviewCommentRepository, PublicReviewCommentRepository>();

            builder.Services.AddDbContext<CodesPublicReviewCommenterDbContext>(o => o.UseSqlServer(connection));
            builder.Services.AddScoped<ICodesPublicReviewCommenterRepository, CodesPublicReviewCommenterRepository>();

            builder.Services.AddDbContext<CodesPRCommentPCADbContext>(o => o.UseSqlServer(connection));
            builder.Services.AddScoped<ICodesPRCommentPCARepository, CodesPRCommentPCARepository>();

            builder.Services.AddDbContext<SupportStatusDbContext>(o => o.UseSqlServer(connection));
            builder.Services.AddScoped<ICodesPRCommentSupportStatusRepository, SupportStatusesRepository>();

            builder.Services.AddDbContext<TasksDbContext>(o => o.UseSqlServer(connection));
            builder.Services.AddScoped<ICodesTasks, CodesTasks>();

            builder.Services.AddDbContext<MeetingsDbContext>(o => o.UseSqlServer(connection));
            builder.Services.AddScoped<IMeetingsRepository, MeetingsRepository>();

            builder.Services.AddDbContext<UserDbContext>(o => o.UseSqlServer(connection));
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();

            builder.Services.AddScoped<IUserContextService, UserContextService>();
            builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();

            builder.Services.AddDbContext<ContactDbContext>(o => o.UseSqlServer(connection));
            builder.Services.AddDbContext<CodesCCRDbContext>(o => o.UseSqlServer(connection));
            builder.Services.AddDbContext<ReferenceDataDbContext>(o => o.UseSqlServer(connection));

            builder.Services.AddScoped<ICodesCCRsRepository, CodesCCRsRepository>();

            builder.Services.AddScoped<ICodeChangeTypeRepository, CodeChangeTypeRepository>();
            builder.Services.AddScoped<ICodesCCRFormatTypeRepository, CodesCCRFormatTypeRepository>();
            builder.Services.AddScoped<ICodeSortingOutputRepository, CodeSortingOutputRepository>();

            //builder.Services.AddScoped<IDependencyValidationService, DependencyValidationService>();

            builder.Services.AddDbContext<PCFDbContext>(o => o.UseSqlServer(connection));
            builder.Services.AddScoped<IPCFRepository, PCFRepository>();
            builder.Services.AddScoped<IPCFService, PCFService>();

            builder.Services.AddDbContext<SubjectsDbContext>(o => o.UseSqlServer(connection));
            builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();

            builder.Services.AddScoped<ICodesCycleRepository, CodesCycleRepository>();
            builder.Services.AddScoped<IPublicReviewUnitOfWork, PublicReviewUnitOfWork>();
            builder.Services.AddScoped<IPublicReviewOutboxWriter, PublicReviewOutboxWriter>();

            builder.Services.AddScoped<ICurrentRequestResource, CurrentRequestResource>();

            builder.Services.AddScoped<ICodesCCRService, CodesCCRService>();
            builder.Services.AddScoped<ICodesResourceService, CodesResourceService>();

            builder.Services.AddDbContext<ReferenceDocumentUpdateDbContext>(o => o.UseSqlServer(connection));
            builder.Services.AddScoped<IReferenceDocumentUpdateRepository, ReferenceDocumentUpdateRepository>();
            builder.Services.AddScoped<IReferenceDocumentUpdateService, ReferenceDocumentUpdateService>();

            builder.Services.AddScoped<IContactService,ContactService>();
            builder.Services.AddScoped<IUnifiedContactRepository, UnifiedContactRepository>();
            builder.Services.AddScoped<ICodesCCRProponentRepository, CodesCCRProponentRepository>();

            builder.Services.AddScoped<ICodesPublicCommentUnifiedService, CodesPublicReviewCommentsUnifiedService>();

            builder.Services.AddDbContext<CodesSavedSearchDbContext>(o => o.UseSqlServer(connection));
            builder.Services.AddScoped<ICodesSavedSearchRepository, CodesSavedSearchRepository>();
        }
        static void SetupSwaggerInDev(WebApplication app, WebApplicationBuilder builder)
        {
            if (!app.Environment.IsDevelopment())
                return;

            app.UseSwagger();
            app.UseSwaggerUI(setup =>
            {
                var descriptions = app.DescribeApiVersions();
                foreach (var desc in descriptions)
                {
                    setup.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", desc.GroupName.ToUpperInvariant());
                }


                setup.OAuthClientId(builder.Configuration["AzureAdSwagger:ClientId"]);
                setup.OAuthScopes(new[] { builder.Configuration["AzureAd:Scopes"] });
                setup.OAuthUsePkce();
                setup.OAuthScopeSeparator(" ");
                setup.OAuthUseBasicAuthenticationWithAccessCodeGrant();

            });

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<PublicReviewCommentDbContext>();

                    // Ensure database is created
                    context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            app.UseWhen(
                ctx => ctx.Request.Path.StartsWithSegments("/swagger") &&
                       (ctx.Request.Path.Value!.EndsWith("swagger.json") || ctx.Request.Path.Value!.EndsWith("oauth2-redirect.html")),
                swaggerApp =>
                {
                    swaggerApp.UseAuthentication();
                    swaggerApp.UseAuthorization();
                    swaggerApp.Use(async (ctx, next) =>
                    {
                        if (!ctx.User.Identity?.IsAuthenticated ?? true)
                        {
                            ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            await ctx.Response.WriteAsync("Unauthorized");
                            return;
                        }

                        await next();
                    });
                });
        }

        static void ConfigureMiddleware(WebApplication app, WebApplicationBuilder builder)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler();
                app.UseHsts();
            }

            app.UseForwardedHeaders();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ResourceResolutionMiddleware>();

            SetupSwaggerInDev(app, builder);

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    if (app.Environment.IsDevelopment())
                        context.Response.Redirect("/swagger");
                    else
                    {
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync("{\"message\": \"Welcome to Codes Staff Portal POC API.\"}");
                    }
                    return;
                }

                await next();
            });

            app.MapControllers();
            app.Run();

         
        }

    }
}

