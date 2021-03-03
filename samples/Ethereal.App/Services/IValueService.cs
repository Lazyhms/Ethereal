using Ethereal.App.Models;
using Ethereal.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Ethereal.App.Services
{
    [ServiceDescriptor(ServiceLifetime.Scoped)]
    public interface IValueService
    {
        Task<object> Insert(Tests tests);
    }
}
