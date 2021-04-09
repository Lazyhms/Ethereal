using Ethereal.App.Models;
using System.Threading.Tasks;

namespace Ethereal.App.Services
{
    public class ValueService : IValueService
    {
        public async Task<object> Insert(Tests tests) => await new ValueTask<object>(tests);
    }
}