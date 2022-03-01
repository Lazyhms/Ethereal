using Ethereal.App.ScheduledTasks;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO.Compression;
using System.Linq;

namespace Ethereal.App
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logger, IHostApplicationLifetime hostApplicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //// ONE
            //hostApplicationLifetime.ApplicationStarted.Register(() =>
            //{
            //    RecurringJob.AddOrUpdate<IValueService>("²âÊÔ", c => c.Write(), Cron.Minutely(), TimeZoneInfo.Local);
            //});

            app.UseCors("CorsPolicy");

            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UsePathBase(Configuration.GetValue<string>("PathBase") ?? string.Empty);

            app.UseSerilogRequestLogging();

            app.UseResponseCompression();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(opts =>
            {
                opts.RoutePrefix = string.Empty;
                opts.SwaggerEndpoint(Configuration, "SwaggerDoc");
            });

            app.UseHangfireDashboard(options: new DashboardOptions
            {
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
                endpoints.MapSwagger();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                        builder.SetIsOriginAllowed(host => true)
                               .SetIsOriginAllowedToAllowWildcardSubdomains()
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials());
            });


            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/signin";
                options.LogoutPath = "/signout";
            })
            .AddWeixin(options =>
            {
                options.ClientId = "wx057a224f4bc3166c";
                options.ClientSecret = "bce001b1eb54b2fd3500f62d15c412ab";
            }).AddQQ(options =>
            {
                options.ClientId = "1";
                options.ClientSecret = "2";
            }).AddGitHub(options =>
            {
                options.ClientId = "29add1b8175d6b0ddd22";
                options.ClientSecret = "370a035e6b46698157a3405a1411b4d8698cb184";
                options.Scope.Add("user:email");
            });

            services.AddHttpContextAccessor();

            services.AddControllers(options =>
            {
                options.Filters.Add<HttpGlobalExceptionFilter>();
                options.ModelBinderProviders.UseDefaultModelBinderProviders();
                options.InputFormatters.Add(new TextPlainInputFormatter());
            }).AddJsonOptions(options =>
            {
            }).ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errorMessage = context.ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage);
                    return new JsonResult(new { Messages = errorMessage }) { StatusCode = StatusCodes.Status400BadRequest };
                };
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(Configuration, "SwaggerDoc");

                options
                    .DocInclusionGroupName("KOKO");

                options
                    .AddJwtSecurityDefinition()
                    .AddJwtSecurityRequirement();
            });

            services.AddDbContextPool<TestContext>(options =>
            {
                options.UseEthereal();
                options.UseSqlServer(Configuration.GetConnectionString("ConnectionStrings"), serverOptions =>
                {
                });
            });

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            services.RegisterAssemblyTypes(typeof(Startup).Assembly);

            services.AddHangfireServer(options =>
            {

                options.Queues = new[] { "default" };
                options.WorkerCount = Environment.ProcessorCount;
                options.ServerName = "Test";
                options.SchedulePollingInterval = TimeSpan.FromSeconds(3);
            });

            services.AddHangfire(options =>
            {
                options.UseDashboardMetric(DashboardMetrics.ServerCount)
                       .UseDashboardMetric(DashboardMetrics.EnqueuedAndQueueCount)
                       .UseDashboardMetric(DashboardMetrics.RecurringJobCount)
                       .UseDashboardMetric(DashboardMetrics.ScheduledCount)
                       .UseDashboardMetric(DashboardMetrics.AwaitingCount)
                       .UseDashboardMetric(DashboardMetrics.ProcessingCount)
                       .UseDashboardMetric(DashboardMetrics.RetriesCount)
                       .UseDashboardMetric(DashboardMetrics.SucceededCount)
                       .UseDashboardMetric(DashboardMetrics.FailedCount)
                       .UseDashboardMetric(DashboardMetrics.DeletedCount);

                options.UseSerilogLogProvider();
                options.UseMemoryStorage();
            });

            // TWO ÍÆ¼ö?
            services.AddHostedService<RecurringTasks>();
        }
    }
}