using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ethereal.EFCore.PostgreSQL.Tests
{
    public class AppDbContextTest : DbContext
    {
        public AppDbContextTest(DbContextOptions<AppDbContextTest> builder) :
            base(builder)
        {
            Database.EnsureCreated();
        }

        public DbSet<Demand> Demand { get; set; }

        public DbSet<CorrespondingDiscipline> CorrespondingDiscipline { get; set; }

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
                opts.UseNpgsql("host=127.0.0.1;port=5432;database=shdc_dev;username=postgres;password=okok;Persist Security Info=true;", opts =>
                {
                })
                .UseEthereal(opts =>
                {
                    opts.WithNamingPolicy();
                })
                .UseEtherealNpgsql();
            });
            return serviceDescriptors.BuildServiceProvider().GetService<AppDbContextTest>();
        }
    }
}
