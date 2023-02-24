using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Schd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SchedulerAdsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPost("{model}")]
        public async Task<IActionResult> Create(object model)
        {
            return Ok();
        }

        [HttpGet("{id}")] // Get one scheduler for edit
        public async Task<IActionResult> Update(long id)
        {
            return Ok();
        }

        [HttpPost("{model}")]
        public async Task<IActionResult> Update(object model)
        {
            return Ok();
        }

        [HttpDelete("{model}")]
        public async Task<IActionResult> Delete(object model)
        {
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Deactive(long id)
        {
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Active(long id)
        {
            return Ok();
        }
    }
}