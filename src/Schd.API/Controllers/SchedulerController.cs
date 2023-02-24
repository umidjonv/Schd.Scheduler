using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schd.API.Data.Classes;
using Schd.API.Data.Interfaces;
using Schd.API.Models;

namespace Schd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SchedulerController : ControllerBase
    {
        private readonly SchedulerRepository _schedulerTemplates;
        public SchedulerController(SchedulerRepository schedulerTemplates)
        {
            _schedulerTemplates = schedulerTemplates;
        }

        [HttpGet] // Get all schedules
        public async Task<IActionResult> Get()
        {
            var result = await _schedulerTemplates.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SchedulerTemplateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (model == null)
                return NotFound();

            await _schedulerTemplates.CreateAsync(model);
            return Ok();
        }

        [HttpGet] // Get one scheduler for edit
        public async Task<IActionResult> Update(long id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (id == 0)
                return NotFound();

            var model = await _schedulerTemplates.GetByIdAsync(id);

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody]SchedulerTemplateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (model == null)
                return NotFound();

            await _schedulerTemplates.UpdateAsync(model);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (id == 0)
                return NotFound();

            _schedulerTemplates.DeleteById(id);
            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> Active([FromBody]long id)
        {
            if(id == 0)
                return BadRequest();
            
            var entity = await _schedulerTemplates.GetByIdAsync(id);
            if(entity.Id == 0)
                return NotFound();
            
            await _schedulerTemplates.ActiveAsync(id);
            return Ok();
        }
    }
}