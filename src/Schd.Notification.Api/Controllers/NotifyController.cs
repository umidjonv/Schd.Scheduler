using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Initializers.PropertyProviders;
using Schd.Common.Response;
using Schd.Notification.Api.Controllers;
using Schd.Notification.Api.Services;
using Schd.Notification.Data;
using Schd.Notification.Data.Domain;
using Schd.Notification.Data.Enums;
using Schd.Notification.Models;

namespace Schd.Notification.Controllers
{

    public class NotifyController : BaseController
    {
        private readonly ILogger<NotifyController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly AppDbContext _db;
        private readonly CommandService _commandService;

        private void AddStateHistory(State state)
        {
            var stateHistory = new StateHistory
            {
                ClientId = state.ClientId,
                NotifyId = state.NotifyId,
                Type = state.Type
                
            };

            _db.StateHistories.AddRange(stateHistory);
        }

        public NotifyController(ILogger<NotifyController> logger, IPublishEndpoint publishEndpoint, AppDbContext db, CommandService commandService)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _db = db;
            _commandService = commandService;
        }

        [HttpGet]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<IActionResult> SendCommand([FromBody]Command command)
        {
            if (command == null)
            {
            }

            var state = new State()
            {
                ClientId = command.ClientId,
                Time = DateTime.Now,
                Type = StateType.New
            };
            AddStateHistory(state);


            

            await _publishEndpoint.Publish(notify);

            state.Type = StateType.Sended;
            AddStateHistory(state);

            _db.Commands.Add();
            await _db.SaveChangesAsync();

            return Ok("Command sent");
        }



        [HttpPost]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public IActionResult ClientRegister()
        {

            return Ok();
        }




    }
}
