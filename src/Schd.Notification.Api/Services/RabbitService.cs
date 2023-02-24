using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using Schd.Common;
using Schd.Notification.Api.Consts;
using Schd.Notification.Api.EventBus.Providers;
using Schd.Notification.EventBus;
using Trading.RabbitMQ.Core;

namespace Schd.Notification.Api.Services
{
    public class RabbitService :IHostedService
    {
        private RabbitProvider rabbitProvider; 
        private IModel _channel;
        private readonly IServiceProvider _services;
        private RabbitEventBus rabbitEventBus;
        public RabbitService(IServiceProvider services)
        {
            _services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            rabbitProvider = _services.GetRequiredService<RabbitProvider>();
            rabbitEventBus = _services.GetRequiredService<RabbitEventBus>();
            
            
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private void CreatingExchanges()
        {
            
            _channel.ExchangeDeclare(SiteConstants.RabbitLogExchange, ExchangeType.Fanout);
            _channel.ExchangeDeclare(SiteConstants.RabbitCommandExchange, ExchangeType.Fanout);

        }

    }
}
