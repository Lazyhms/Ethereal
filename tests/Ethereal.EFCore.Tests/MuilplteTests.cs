using Ethereal.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ethereal.EFCore.Tests
{
    public class MuilplteTests
    {
        private readonly IServiceCollection serviceDescriptors;

        public MuilplteTests()
        {
            serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddDbContext<AppDbContextTest>(opts =>
            {
                opts.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                opts.EnableSensitiveDataLogging();
                opts.UseLoggerFactory(LoggerFactory.Create(builder =>
                {
                    builder.AddConsole();
                    builder.AddDebug();
                }));
                opts.UseSqlServer("Server=127.0.0.1;Database=Ethereal;User Id=sa;Password=okok;MultipleActiveResultSets=true", opts =>
                {
                });
                //opts.UseMySql("server=127.0.0.1;userid=sa;pwd=okok;port=3306;database=Ethereal;sslmode=none;Charset=utf8;AutoEnlist=false", ServerVersion.AutoDetect("server=127.0.0.1;userid=sa;pwd=okok;port=3306;database=Permission;sslmode=none;Charset=utf8;AutoEnlist=false"), opts =>
                //{
                //    opts.SchemaBehavior(MySqlSchemaBehavior.Translate, (a, b) => a + b);
                //});
                opts.UseEthereal(opts =>
                {
                });
            });
        }

        [Fact]
        public async Task DbContextExtensions_TestAsync()
        {
            var context = GetDbContext();

            var id = Guid.NewGuid();

            var stu1 = new Stu
            {
                Id = id,
                Name = "31231233123543531231",
                Score = 20
            };

            context.Add(stu1);
            await context.SaveChangesAsync();

            context.Update(stu1, true, b => b.Name);
            await context.SaveChangesAsync();

            context.SoftDelete<Stu, Guid>(id);
            await context.SaveChangesAsync();

            context.Delete<Stu, Guid>(id);
            await context.SaveChangesAsync();


            await context.Stus.AddAsync(new Stu
            {
                Id = id,
                Name = "121",
            });
            await context.SaveChangesAsync();

            context.Update(new Stu
            {
                Id = id,
                Name = "312312331235435",
                Score = 21
            }, true, b => b.Name, b => b.Score);
            await context.SaveChangesAsync();

            context.SoftDelete(new Stu { Id = id });
            await context.SaveChangesAsync();

            context.Delete(new Stu { Id = id });
            await context.SaveChangesAsync();
        }


        [Fact]
        public async Task DbSetExtensions_TestAsync()
        {
            var context = GetDbContext();

            var id = Guid.NewGuid();

            var stu1 = new Stu
            {
                Id = id,
                Name = "31231233123543531231",
                Score = 20
            };

            context.Stus.Add(stu1);
            await context.SaveChangesAsync();

            context.Stus.Update(stu1, true, b => b.Name);
            await context.SaveChangesAsync();

            context.Stus.SoftDelete(id);
            await context.SaveChangesAsync();

            context.Stus.Delete(id);
            await context.SaveChangesAsync();


            await context.Stus.AddAsync(new Stu
            {
                Id = id,
                Name = "121",
            });
            await context.SaveChangesAsync();

            context.Stus.Update(new Stu
            {
                Id = id,
                Name = "312312331235435",
                Score = 21
            }, true, b => b.Name, b => b.Score);
            await context.SaveChangesAsync();

            context.Stus.SoftDelete<Stu>(new Stu { Id = id });
            await context.SaveChangesAsync();

            context.Stus.Delete<Stu>(new Stu { Id = id });
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task DbSetExtensions_Scope_TestAsync()
        {
            using var context = GetDbContext();

            var id = Guid.NewGuid();
            var stu1 = new Stu
            {
                Id = id,
                Name = "31231233123543531231",
                Score = 20
            };
            context.Stus.Add(stu1);
            await context.SaveChangesAsync();

            using var context1 = GetDbContext();

            context1.Stus.Update(new Stu
            {
                Id = id,
                Name = "312312331235435",
                Score = 21
            }, true, b => b.Name, b => b.Score);
            await context1.SaveChangesAsync();
        }

        [Fact]
        public async Task DbContextExtensions_Scope_TestAsync()
        {
            using var context = GetDbContext();

            var id = Guid.NewGuid();
            var stu1 = new Stu
            {
                Id = id,
                Name = "31231233123543531231",
                Score = 20
            };
            context.Add(stu1);
            await context.SaveChangesAsync();

            using var context1 = GetDbContext();

            context1.Update(new Stu
            {
                Id = id,
                Name = "312312331235435",
                Score = 21
            }, true, b => b.Name, b => b.Score);
            await context1.SaveChangesAsync();
        }


        [Fact]
        public async Task PaginationExtension_Tests()
        {
            using var context = GetDbContext();

            var t1 = await context.Stus.PaginationAsync(1, 10);
            var t2 = await context.Stus.PaginationByAsync(s => s.Created, 1, 10);
            var t3 = await context.Stus.PaginationByDescendingAsync(s => s.Name.Contains("1"), o => o.Created, 1, 10);
        }

        [Fact]
        public async Task WhereExtension_Tests()
        {
            using var context = GetDbContext();

            var t1 = await context.Stus.Where(1 == 1, s => s.Name.Equals("1")).ToListAsync();
            var t2 = await context.Stus.Where(1 == 2, s => s.Name.Equals("1")).ToListAsync();
        }

        private AppDbContextTest GetDbContext() => serviceDescriptors.BuildServiceProvider().GetService<AppDbContextTest>();
    }
}