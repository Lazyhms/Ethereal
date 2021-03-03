using Ethereal.App.Models;
using Ethereal.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ethereal.App.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ValuesController : ControllerBase
    {
        private readonly IValueService ValueService;

        public ValuesController(IValueService valueService) => ValueService = valueService;

        [HttpGet]
        public Task<IActionResult> Get() => throw new InvalidOperationException("das");

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(Tests tests) => Ok(await ValueService.Insert(tests));
    }
}
