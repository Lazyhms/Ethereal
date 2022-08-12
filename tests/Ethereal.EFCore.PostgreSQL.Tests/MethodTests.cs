using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ethereal.EFCore.PostgreSQL.Tests
{

    public class MethodTests
    {


        [Fact]
        public async Task MthodTests()
        {
            using var dbContext = AppDbContextTest.GetDbContext();

            var t = await dbContext.Demand.AsNoTracking().Select(selector: s => EF.Functions.Unnest(s.ClinicalResearchStaging)).ToListAsync();
        }

    }
}
