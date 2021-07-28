using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitProject.Models;
using RabbitProject.Rabbit;

namespace RabbitWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IRabbitClient _rabbitClient;

        public HomeController(IRabbitClient rabbitClient)
        {
            _rabbitClient = rabbitClient;
        }

        [HttpGet]
        public IActionResult Publish()
        {
            _rabbitClient.Publish(new BaseModel()
            {
                Id = Guid.NewGuid(),
                Name = "NewModel_" + DateTime.Now.ToShortDateString()
            });
            return Ok();
        }

        [HttpGet]
        public IActionResult Send()
        {
            _rabbitClient.Publish(new BaseModel()
            {
                Id = Guid.NewGuid(),
                Name = "NewModel_" + DateTime.Now.ToShortDateString()
            });
            return Ok();
        }

    }
}
