using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ethereal.Snowflake.Tests
{
    public class UuidTests
    {
        private readonly System.Snowflake idworker = new System.Snowflake(1, 1);

        [Fact]
        public async Task MulitlpTestAsync()
        {
            var threadNum = 10;
            var idNum = 10000;

            var bags = new ConcurrentBag<long>();
            var tasks = new List<Task>();
            for (var i = 0; i < threadNum; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    Parallel.For(0, idNum, i =>
                    {
                        bags.Add(idworker.Generate());
                    });
                }));
            }
            Task.WaitAll(tasks.ToArray());

            var t = bags.Count;
            var t1 = bags.Distinct().Count();

            Assert.Equal(bags.Count, threadNum * idNum);
            Assert.Equal(bags.Distinct().Count(), threadNum * idNum);

            await Task.CompletedTask;
        }

        [Fact]
        public async Task SingleTest()
        {
            var _ = idworker.Generate();

            await Task.CompletedTask;
        }
    }
}