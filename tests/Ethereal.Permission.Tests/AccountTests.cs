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
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            var list = await dbContext.Account.ToListAsync();


            var account = new Account
            {
                Identifier = string.Empty,
                Certificate = string.Empty,
                IdentityType = 1
            };


            await dbContext.Account.WhereIf(1 == 1, (s => s.IdentityType == 1, s => s.IdentityType == 1)).ToListAsync();

            dbContext.Account.Add(account);
            dbContext.SaveChanges();
        }
    }
}
