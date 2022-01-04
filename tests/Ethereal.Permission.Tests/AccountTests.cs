using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ethereal.Permission.Tests
{
    public class AccountTests
    {
        [Fact]
        public async Task CreateDb()
        {

            using var dbContext = AppDbContextTest.GetDbContext();

            var account = new Account
            {
                Identifier = string.Empty,
                Certificate = string.Empty,
                IdentityType = 1
            };
            dbContext.Account.Add(account);
            dbContext.SaveChanges();

            var t0 = await dbContext.Account.Where(1 == 1, s => s.Id > 3).ToListAsync();

            var t1 = await dbContext.Account.Where(1 == 1, (s => s.IdentityType == 1, s => s.IdentityType == 1)).ToListAsync();

            var t2 = await dbContext.Account.OrderBy("Id").ThenBy(nameof(account.Modified)).ToListAsync();

            var t3 = await dbContext.Account.OrderByDescending("Id").ThenByDescending(nameof(account.Modified)).ToListAsync();
        }

        [Fact]
        public async void ExecutDa()
        {
            var dataBase = AppDbContextTest.GetDbContext().Database;

            var accounts1 = dataBase.ExecuteSqlReaderInterpolated($"select Id as Id from permission.account").ToList();
            var accounts2 = dataBase.ExecuteSqlReaderInterpolated<Account>($"select Id as Id from permission.account").ToList();
            var accounts3 = dataBase.ExecuteSqlReaderInterpolated($"select Id as Id from permission.account", result => new
            {
                Id = result["Id"]
            }).ToList();

            var accounts4 = (await dataBase.ExecuteSqlReaderInterpolatedAsync($"select Id as Id from permission.account")).ToList();
            var accounts5 = (await dataBase.ExecuteSqlReaderInterpolatedAsync<Account>($"select Id as Id from permission.account")).ToList();
            var accounts6 = (await dataBase.ExecuteSqlReaderInterpolatedAsync($"select Id as Id from permission.account", result => new
            {
                Id = result["Id"]
            })).ToList();

            var accounts7 = dataBase.ExecuteSqlReader("select Id as Id from permission.account").ToList();
            var accounts8 = dataBase.ExecuteSqlReader<Account>("select Id as Id from permission.account").ToList();
            var accounts9 = dataBase.ExecuteSqlReader("select Id as Id from permission.account", null, result => new
            {
                Id = result["Id"]
            }).ToList();

            var accounts10 = (await dataBase.ExecuteSqlReaderAsync("select Id as Id from permission.account")).ToList();
            var accounts11 = (await dataBase.ExecuteSqlReaderAsync<Account>("select Id as Id from permission.account")).ToList();
            var accounts12 = (await dataBase.ExecuteSqlReaderAsync("select Id as Id from permission.account", null, result => new
            {
                Id = result["Id"]
            })).ToList();
        }
    }
}
