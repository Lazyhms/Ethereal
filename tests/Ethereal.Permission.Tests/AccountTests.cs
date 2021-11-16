using Microsoft.EntityFrameworkCore;
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

            var t1 = await dbContext.Account.WhereIf(1 == 1, (s => s.IdentityType == 1, s => s.IdentityType == 1)).ToListAsync();

            var t2 = await dbContext.Account.OrderBy("Id").ToListAsync();

            var t3 = await dbContext.Account.OrderByDescending("Id").ToListAsync();
        }
    }
}
