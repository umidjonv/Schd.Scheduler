using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Schd.Common.Response;
using Schd.Notification.Api.Controllers;
using Schd.Notification.Api.EventBus.Models;
using Schd.Notification.Api.Services;
using Schd.Notification.Data;
using Schd.Notification.Data.Enums;
using Schd.Notification.EventBus;
using Schd.Notification.Models;

namespace Schd.Notification.Controllers
{

    public class NotifyController : BaseController
    {
        private readonly ILogger<NotifyController> _logger;
        private readonly AppDbContext _db;
        private readonly CommandService _commandService;
        private readonly RabbitEventBus _eventBus;

        private void AddStateHistory(State state)
        {
            var stateHistory = new StateHistory
            {
                ClientId = state.ClientId,
                EventId = state.EventId,
                Type = state.Type
                
            };

            _db.StateHistories.AddRange(stateHistory);
        }

        public NotifyController(RabbitEventBus eventBus)
        {
            
            _eventBus = eventBus;
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(ApiResponse))]
        public async Task<IActionResult> SendCommand([FromBody]string message)
        {
            
            //var state = new State()
            //{
            //    ClientId = Guid.Parse("d56f74a7-f922-4e45-8a9b-5f4474da9f1b"),
            //    Time = DateTime.Now,
            //    Type = StateType.New
            //};
            //AddStateHistory(state);

            _eventBus.Publish(new NotifyEvent()
            {
                ServiceId = Guid.Parse("d56f74a7-f922-4e45-8a9b-5f4474da9f1b"),
                Type = NotificationType.Command,
                State = StateType.New,
                Message = JsonConvert.SerializeObject(message)
            });
            
            
            //state.Type = StateType.Sended;
            //AddStateHistory(state);

            ////_db.Commands.Add();
            //await _db.SaveChangesAsync();

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
