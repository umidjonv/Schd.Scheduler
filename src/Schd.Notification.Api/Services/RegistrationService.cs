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
using Microsoft.Extensions.Configuration;
using Schd.Notification.Data.Enums;
using Schd.Notification.Api.EventBus;

namespace Schd.Notification.Api.Services
{
    public class RegistrationService:IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly RabbitEventBus _eventBus;
        public AppService appService;
        public List<IIntegrationEventHandler<NotifyEvent>> handlers;
        private EventBusManager _eventBusManager;

        public RegistrationService(IServiceProvider serviceProvider, IConfiguration configuration, RabbitEventBus eventBus)
        {
            
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _eventBus = eventBus;
        }

        public async Task RegisterHandlers()
        {
            
            foreach (var handler in handlers) 
            {
                _eventBusManager.SubscribeHandler(appService.Id, handler);

                _eventBus.CreateQueue(appService.Id,handler.GetNotificationType());

                switch (handler.GetNotificationType())
                {
                    case NotificationType.Log:
                        
                        _eventBus.Consume<NotifyEvent, LogNotificationHandler>(appService.Id);

                        break;
                    case NotificationType.Message:
                        _eventBus.Consume<NotifyEvent, MessageNotificationHandler>(appService.Id);

                        break;
                    case NotificationType.Command:
                        _eventBus.Consume<NotifyEvent, CommandNotificationHandler>(appService.Id);

                        break;

                }
                
            }

        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            appService = new AppService();
            _configuration.GetSection("AppService").Bind(appService);
            _eventBusManager = _serviceProvider.GetService<EventBusManager>();
            var logHandler = _serviceProvider.GetRequiredService<LogNotificationHandler>();
            var messageHandler = _serviceProvider.GetRequiredService<MessageNotificationHandler>();
            var commandHandler = _serviceProvider.GetRequiredService<CommandNotificationHandler>();

            handlers =
                new List<IIntegrationEventHandler<NotifyEvent>>
                {
                    logHandler,
                    messageHandler,
                    commandHandler
                };

            _eventBus.DeclareExchange(logHandler.GetNotificationType());
            _eventBus.DeclareExchange(messageHandler.GetNotificationType());
            _eventBus.DeclareExchange(commandHandler.GetNotificationType());

            await RegisterHandlers();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
