using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Ethereal.Permission.Tests
{
    public class AppDbContextTest : DbContext
    {
        public AppDbContextTest(DbContextOptions<AppDbContextTest> builder) :
            base(builder)
        {
            Database.EnsureCreated();

            SaveChangesFailed += (sender, args) =>
            {
                if (args.Exception is DbUpdateException dbUpdateException)
                {
                    if (true)
                    {

                    }
                }
            };
        }

        public DbSet<Account> Account { get; set; }

        public DbSet<Role> Role { get; set; }

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
                //opts.UseMySql("server=127.0.0.1;userid=sa;pwd=okok;port=3306;database=Permission;sslmode=none;Charset=utf8;AutoEnlist=false", ServerVersion.AutoDetect("server=127.0.0.1;userid=sa;pwd=okok;port=3306;database=Permission;sslmode=none;Charset=utf8;AutoEnlist=false"), opts =>
                //{
                //    opts.SchemaBehavior(MySqlSchemaBehavior.Translate, (a, b) => a + b);
                //})
                opts.UseNpgsql("host=127.0.0.1;port=5432;database=Permission;username=postgres;password=qazwsx;Persist Security Info=true;Connection Pruning Interval=5;", opts =>
                {
                })
                //opts.UseSqlServer("Server=127.0.0.1;Database=Permission;User Id=sa;Password=okok;MultipleActiveResultSets=true", options =>
                //{
                //})
                .UseEthereal(opts =>
                {
                    opts.WithNamingPolicy();
                });
            });
            return serviceDescriptors.BuildServiceProvider().GetService<AppDbContextTest>();
        }
    }
}