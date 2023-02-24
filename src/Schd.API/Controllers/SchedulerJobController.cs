using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Schd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SchedulerJobController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Deactive(long id)
        {
            return Ok();
        }
    }
}
