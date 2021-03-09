using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO.Compression;
using System.Linq;

namespace Ethereal.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

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

            services.AddControllers(options =>
            {
                options.Filters.Add<HttpGlobalExceptionFilter>();
                options.ModelBinderProviders.UseDefaultModelBinderProviders();
            }).AddJsonOptions(options =>
            {
                options.UseDefaultJsonOptions();
            }).ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errorMessage = context.ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage);
                    return new JsonResult(new { Messages = errorMessage }) { StatusCode = StatusCodes.Status400BadRequest };
                };
            }).SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(Configuration, "SwaggerDoc");

                options
                    .DocInclusionGroupName("KOKO");

                options
                    .AddJwtSecurityDefinition()
                    .AddJwtSecurityRequirement();

                options.IncludeXmlComments();
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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(opts =>
            {
                opts.RoutePrefix = string.Empty;

                opts.SwaggerEndpoint(Configuration, "SwaggerDoc");
            });

            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
            });
        }
    }
}
