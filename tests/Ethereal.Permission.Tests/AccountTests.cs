using Ethereal.EntityFrameworkCore;
using Xunit;

namespace Ethereal.Permission.Tests
{
    public class AccountTests
    {
        [Fact]
        public void CreateDb()
        {
            using (var dbContext = AppDbContextTest.GetDbContext())
            {
                dbContext.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
                dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
                var account = new Account
                {
                    Id = 1,
                    Identifier = string.Empty,
                    Certificate = string.Empty,
                    IdentityType = 1
                };
                dbContext.Account.Add(account);

                dbContext.SaveChanges();
            }

            using var dbContext1 = AppDbContextTest.GetDbContext();
            dbContext1.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
            dbContext1.ChangeTracker.AutoDetectChangesEnabled = false;
            var account1 = new Account
            {
                Id = 1,
                Identifier = "1",
                Certificate = "1",
                IdentityType = 1
            };

            dbContext1.Account.Update(account1);
            dbContext1.SaveChanges();


            using var dbContext2 = AppDbContextTest.GetDbContext();
            dbContext2.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
            dbContext2.ChangeTracker.AutoDetectChangesEnabled = false;
            var account2 = new Account
            {
                Id = 1,
                Identifier = "1",
                Certificate = "1",
                IdentityType = 1
            };
            dbContext2.SoftDelete(account2);
            dbContext2.SaveChanges();

            using var dbContext3 = AppDbContextTest.GetDbContext();
            dbContext3.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
            dbContext3.ChangeTracker.AutoDetectChangesEnabled = false;
            var account3 = new Account
            {
                Id = 1,
                Identifier = "1",
                Certificate = "1",
                IdentityType = 1
            };

            dbContext2.Account.Remove(account3);
            dbContext2.SaveChanges();
        }
    }
}
