using Ethereal.App.Models;
using Ethereal.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ethereal.App.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [AllowAnonymous]
    [ApiExplorerSettings(GroupName = "KOKO")]
    public class ValuesController : ControllerBase
    {
        private readonly IValueService ValueService;

        public ValuesController(IValueService valueService) => ValueService = valueService;

        /// <summary>
        /// Post测试
        /// </summary>
        /// <param name="tests"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Post(Tests tests)
        {
            if (tests == null)
            {
                return NoContent();
            }
            return Ok(await ValueService.Insert(tests));
        }
    }
}