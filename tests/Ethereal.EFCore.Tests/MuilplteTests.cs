using Ethereal.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ethereal.EFCore.Tests
{
    public class MuilplteTests
    {
        [Fact]
        public async Task DbContextExtension_TestAsync()
        {
            var context = GetDbContext();

            var id = Guid.Parse("f0158dca-e568-47ec-9980-07503ab389ef");

            //await context.Stus.AddAsync(new Stu
            //{
            //    Id = id,
            //    Name = "121",
            //});
            //await context.SaveChangesAsync();

            //var stu = new Stu
            //{
            //    Id = id,
            //    Name = "312312331235435",
            //    Score = 20
            //};
            //context.Stus.Update(stu, true, b => b.Name);
            //await context.SaveChangesAsync();

            //context.Stus.SoftDelete(new Stu { Id = id });
            //context.Stus.Delete(id);
            //await context.SaveChangesAsync();

            var stu1 = new Stu
            {
                Id = id,
                Name = "31231233123543531231",
                Score = 20
            };

            //context.Add(stu1);
            //await context.SaveChangesAsync();

            //context.Update(stu1, true, b => b.Name);
            //await context.SaveChangesAsync();

            //context.SoftDelete<Stu, Guid>(id);
            //await context.SaveChangesAsync();

            context.Delete(stu1);
            await context.SaveChangesAsync();

        }

        [Fact]
        public async Task WhereExtension_Tests()
        {
            using var context = GetDbContext();

            var t1 = await context.Stus.Where(1 == 1, s => s.Name.Equals("1")).ToListAsync();
            var t2 = await context.Stus.Where(1 == 2, s => s.Name.Equals("1")).ToListAsync();
        }

        [Fact]
        public async Task Bewteen_Tests()
        {
            using var context = GetDbContext();
            var t1 = await context.Stus.Where(s => s.Score > 0 && s.Created.Between(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-1))).ToListAsync();
        }

        [Fact]
        public async Task PaginationExtension_Tests()
        {
            using var context = GetDbContext();

            var t1 = await context.Stus.PaginationAsync(1, 10);
            var t2 = await context.Stus.PaginationByAsync(s => s.Created, 1, 10);
            var t3 = await context.Stus.PaginationByDescendingAsync(s => s.Name.Contains("1"), o => o.Created, 1, 10);
        }

        private AppDbContextTest GetDbContext()
        {
            IServiceCollection serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddDbContext<AppDbContextTest>(opts =>
            {
                opts.EnableSensitiveDataLogging();
                opts.UseLoggerFactory(LoggerFactory.Create(builder =>
                {
                    builder.AddConsole();
                    builder.AddDebug();
                }));
                opts.UseSqlServer("Server=127.0.0.1;Database=Ethereal;User Id=sa;Password=okok;MultipleActiveResultSets=true", opts =>
                {
                });
                opts.UseEthereal(opts =>
                {
                });
            });
            return serviceDescriptors.BuildServiceProvider().GetService<AppDbContextTest>();
        }
    }
}
