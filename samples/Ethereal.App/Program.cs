using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Ethereal.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "{Date}: Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.CaptureStartupErrors(false);
                    webBuilder.UseContentRoot(AppContext.BaseDirectory);
                    webBuilder.ConfigureAppConfiguration(configure =>
                    {
                        configure.SetBasePath(AppContext.BaseDirectory);
                    });
                    webBuilder.ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                    }).UseSerilog((context, configure) =>
                    {
                        configure
                                .Enrich.FromLogContext()
                                .Enrich.WithMachineName()
                                .Enrich.WithEnvironmentUserName()
                                .Enrich.WithThreadId()
                                .Enrich.WithThreadName();
                        if (!context.Configuration.GetSection("Serilog").Exists())
                        {
                            configure
                                .MinimumLevel.Override("Default", LogEventLevel.Warning)
                                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                                .WriteTo.Async(w => w.Debug())
                                .WriteTo.Async(w => w.Console())
                                .WriteTo.Async(w => w.File(Path.Combine(AppContext.BaseDirectory, "Logs\\Information\\.log"), shared: true, restrictedToMinimumLevel: LogEventLevel.Information, rollingInterval: RollingInterval.Day))
                                .WriteTo.Async(w => w.File(Path.Combine(AppContext.BaseDirectory, "Logs\\Error\\.log"), shared: true, restrictedToMinimumLevel: LogEventLevel.Error, rollingInterval: RollingInterval.Day));
                        }
                        else
                        {
                            configure
                                .Enrich.WithProperty("Date", $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}")
                                .Enrich.WithProperty("Path", AppContext.BaseDirectory)
                                .ReadFrom.Configuration(context.Configuration);
                        }
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
