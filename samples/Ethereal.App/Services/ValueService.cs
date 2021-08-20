using Ethereal.App.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Ethereal.App.Services
{
    public class ValueService : IValueService
    {
        private readonly ILogger _logger;
        public ValueService(ILogger<ValueService> logger) => _logger = logger;

        public async Task<object> Insert(Tests tests) => await new ValueTask<object>(tests);


        public async Task Write()
        {
            _logger.LogInformation($"周期任务测试: 说我是帅哥");
            await Task.CompletedTask;
        }
    }
}