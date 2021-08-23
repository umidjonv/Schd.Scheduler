using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Schd.Common.Response;
using Schd.Notification.Data;
using Schd.Notification.Models;

namespace Schd.Notification.Controllers
{

    public class NotifyController : BaseController
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
            _publishEndpoint.Publish(new Notify()
            {
                Id= Guid.NewGuid(),
                Message = "Sending Message",
                CreationDate = DateTime.Now,
                //ServiceName = "NotifyMessage"
            });

            return Ok();
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public IActionResult ClientRegister()
        {

            return Ok();
        }




    }
}
