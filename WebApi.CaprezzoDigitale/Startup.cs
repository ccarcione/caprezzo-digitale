using System.Collections.Generic;
using System.IO;
using System.Linq;
using EmailLibTool;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WebApi.CaprezzoDigitale.CronJobs;
using WebApi.CaprezzoDigitale.Middleware;
using WebApi.CaprezzoDigitale.Models;

namespace WebApi.CaprezzoDigitale
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
            });
            services.AddHostedService<CrawlingBollettiniArpaCronJob>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WebApi Caprezzo Digitale",
                    Version = "v1"
                });
            });

            services.AddSingleton<EmailSend>(x => new EmailSend(

                smtpServer: Configuration.GetValue<string>("EmailConfiguration:SmtpServer"),
                smtpPort: Configuration.GetValue<int>("EmailConfiguration:SmtpPort"),
                useSsl: Configuration.GetValue<bool>("EmailConfiguration:UseSsl"),

                emailMittente: Configuration.GetValue<string>("EmailConfiguration:Mittente"),
                smtpUsername: Configuration.GetValue<string>("EmailConfiguration:SmtpUsername"),
                smtpPassword: Configuration.GetValue<string>("EmailConfiguration:SmtpPassword"),
                authenticate: Configuration.GetValue<bool>("EmailConfiguration:Authenticate"),

                aliasName: Configuration.GetValue<string>("EmailConfiguration:AliasName"),

                recoveryEmail: Configuration.GetValue<bool>("EmailConfiguration:RecoveryFailedSendEmail"),
                recoveryEmailPath: Configuration.GetValue<string>("EmailConfiguration:RecoveryFailedSendEmailPath"),
                backupSendEmail: Configuration.GetValue<bool>("EmailConfiguration:BackupSendEmail"),
                backupSendEmailPath: Configuration.GetValue<string>("EmailConfiguration:BackupSendEmailPath")
                ));

            services.AddSingleton(x => new Options()
            {
                WebApiOptions = Configuration.GetSection("WebApi.caprezzo_digitale.Models.Options").Get<Dictionary<string, string>>(),
                EmailDestinatariFeedback = Configuration.GetSection("WebApi.caprezzo_digitale.Models.Options.EmailDestinatariFeedback").Get<IEnumerable<string>>(),
                EmailDestinatariLog = Configuration.GetSection("WebApi.caprezzo_digitale.Models.Options.EmailDestinatariLog").Get<IEnumerable<string>>(),
                ApiKeyAuth = Configuration.GetSection("WebApi.caprezzo_digitale.Models.Options.ApiKeyAuth").Get<IEnumerable<ApiKeyAuth>>()
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment() || env.EnvironmentName.Equals("master", System.StringComparison.InvariantCultureIgnoreCase))
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.DocumentTitle = $"Swagger UI - caprezzo_digitale - {env.EnvironmentName}";
                    c.RoutePrefix = "api-docs";
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"caprezzo_digitale {env.EnvironmentName} API V1");
                });

                app.UseDeveloperExceptionPage();
            }

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                Models.Options options = scope.ServiceProvider.GetRequiredService<Models.Options>();

                string publicStaticFilesFolderPath = Path.Combine(env.ContentRootPath, SeedMockData.GetWindowsLinuxPath(options.WebApiOptions["publicStaticFiles_FolderPath"]));
                if (!Directory.Exists(publicStaticFilesFolderPath))
                {
                    Directory.CreateDirectory(publicStaticFilesFolderPath);
                }
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(publicStaticFilesFolderPath),
                    RequestPath = string.Concat("/", options.WebApiOptions["publicStaticFiles_RequestPath"]),
                });

                string galleriaFolderPath = Path.Combine(env.ContentRootPath, SeedMockData.GetWindowsLinuxPath(options.WebApiOptions["galleria_FolderPath"]));
                if (!Directory.Exists(galleriaFolderPath))
                {
                    Directory.CreateDirectory(galleriaFolderPath);
                }
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(galleriaFolderPath),
                    RequestPath = string.Concat("/", options.WebApiOptions["galleria_RequestPath"]),
                });
            }
            app.UseMiddleware<CustomErrorHandlingMiddleware>();
            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            UpdateDatabaseMigrate(app, env, logger);
        }

        public void UpdateDatabaseMigrate(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                string databaseName = context.Database.GetDbConnection().Database;
                if (!(context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                {
                    logger.LogDebug($"Database {databaseName} non trovato. Inizializzazione database con migrazione.");
                    context.Database.Migrate();
                }
                logger.LogDebug($"Database {databaseName} ok!");
                if (context.Database.GetPendingMigrations().Any())
                {
                    logger.LogDebug($"Database {databaseName} non aggiornato. Applicazione migrazioni mancanti.");
                    context.Database.Migrate();
                }
                logger.LogDebug($"Pending Migrations Database {databaseName} ok.");

                var options = scope.ServiceProvider.GetRequiredService<Models.Options>();
                bool isSeedAllData;
                bool.TryParse(options.WebApiOptions["seedAllData"], out isSeedAllData);
                if (isSeedAllData)
                {
                    SeedMockData.SeedAllData(app, env, logger);
                }
            }
        }
    }
}

