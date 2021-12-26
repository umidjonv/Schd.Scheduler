using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schd.Notification.Api.EventBus.Abstractions;
using Schd.Notification.Api.EventBus.Models;
using Schd.Notification.Api.Infrastructure;
using Schd.Notification.EventBus;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Schd.Notification.Api.Services
{
    public class RegistrationService
    {
        public AppService appService;
        public List<IIntegrationEventHandler> handlers;
        private readonly IServiceProvider _services;
        private readonly EventBusManager eventBusManager;

        public RegistrationService(AppService appService, List<IIntegrationEventHandler> handlers)
        {
            this.appService = appService;
            this.handlers = handlers;
        }

        public async Task Registrate()
        {
            var eventBus = _services.GetService<RabbitEventBus>();
            foreach (var handler in handlers) 
            {
                eventBusManager.SubscribeHandler(appService.Name, handler);
                
                //eventBus.CreateQueue(appService.Name)
            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

    }
}
