using Ethereal.App.Models;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Ethereal.App.Services
{
    [ServiceDescriptor(ServiceLifetime.Scoped)]
    public interface IValueService
    {
        Task<object> Insert(Tests tests);
    }
}
