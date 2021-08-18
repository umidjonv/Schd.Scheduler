using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Schd.Common.Response;
using Schd.Notification.Models;

namespace Schd.Notification.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class NotifyController : ControllerBase
    {
        private readonly ILogger<NotifyController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public NotifyController(ILogger<NotifyController> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public IActionResult Send()
        {
            _publishEndpoint.Publish(new Models.EventBus.NotifyMessage
            {
                Id= Guid.NewGuid(),
                Message = "Sending Message",
                SendDate = DateTime.Now,
                ServiceName = "NotifyMessage"
            });

            return Ok();
        }


        
        
    }
}
