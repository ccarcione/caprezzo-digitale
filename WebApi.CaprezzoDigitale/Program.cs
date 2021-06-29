using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NpgsqlTypes;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;
using Serilog.Sinks.SystemConsole.Themes;

namespace WebApi.CaprezzoDigitale
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
                    {
                        {"Message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
                        {"level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
                        {"data", new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
                        {"pathDataObject", new SinglePropertyColumnWriter("pathDataObject", PropertyWriteMethod.ToString, NpgsqlDbType.Text)},
                        {"typeDataObject", new SinglePropertyColumnWriter("typeDataObject", PropertyWriteMethod.ToString, NpgsqlDbType.Text)},
                        {"idDataObject", new SinglePropertyColumnWriter("idDataObject", PropertyWriteMethod.ToString, NpgsqlDbType.Text)}
                    };
            var file = File.CreateText("SerilogSelf.log");
            Serilog.Debugging.SelfLog.Enable(TextWriter.Synchronized(file));

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Debug)
                .MinimumLevel.Override("WebApi.caprezzo_digitale", LogEventLevel.Debug)
                .Enrich.FromLogContext()
                .WriteTo.PostgreSQL(Environment.GetEnvironmentVariable("LogConnection"), "Logs", columnWriters, needAutoCreateTable: true)
                .WriteTo.File(
                    Path.Combine(Directory.GetCurrentDirectory(), "LogFiles", Assembly.GetExecutingAssembly().GetName().Name, "webapi.caprezzo_digitale.log"),
                    fileSizeLimitBytes: 1_000_000,
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true, 
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1))
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            try
            {
                Log.Information("Starting host...");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureHostConfiguration(configHost => configHost.AddEnvironmentVariables())
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.SetBasePath(Directory.GetCurrentDirectory());
                    configApp.AddJsonFile("appsettings.json");
                    configApp.AddJsonFile(
                        $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                        optional: false, reloadOnChange: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
