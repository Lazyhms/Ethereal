using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Ethereal.Permission.Tests
{
    public class AppDbContextTest : DbContext
    {
        public AppDbContextTest(DbContextOptions<AppDbContextTest> builder) :
            base(builder) => Database.EnsureCreated();

        public DbSet<Account> Account { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            base.OnModelCreating(modelBuilder);


        private static readonly IServiceCollection serviceDescriptors = new ServiceCollection();

        public static AppDbContextTest GetDbContext()
        {
            serviceDescriptors.AddDbContext<AppDbContextTest>(opts =>
            {
                opts.EnableSensitiveDataLogging();
                opts.UseLoggerFactory(LoggerFactory.Create(builder =>
                {
                    builder.AddConsole();
                    builder.AddDebug();
                }));
                opts.UseMySql("server=127.0.0.1;userid=sa;pwd=okok;port=3306;database=Permission;sslmode=none;Charset=utf8;AutoEnlist=false", ServerVersion.AutoDetect("server=127.0.0.1;userid=sa;pwd=okok;port=3306;database=Permission;sslmode=none;Charset=utf8;AutoEnlist=false"), opts =>
                {
                    opts.SchemaBehavior(MySqlSchemaBehavior.Translate, (a, b) => a + b);
                });
                opts.UseEthereal(opts =>
                {
                });
            });
            return serviceDescriptors.BuildServiceProvider().GetService<AppDbContextTest>();
        }
    }
}