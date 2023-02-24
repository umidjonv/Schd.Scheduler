using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Schd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SchedulerProcessController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}