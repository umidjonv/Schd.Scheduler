using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Schd.Common;
using Schd.Notification.Api.Consts;
using Trading.RabbitMQ.Core;

namespace Schd.Notification.Api.Services
{
    public class RabbitService
    {
        private IConnection _rabbitConnection;
        private IModel _channel;

        public RabbitService(string username, string password, string vhost, string hostName)
        {

            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = username;
            factory.Password = password;
            factory.VirtualHost = vhost;
            factory.HostName = hostName;

            _rabbitConnection = factory.CreateConnection();

            _channel = _rabbitConnection.CreateModel();
        }

        private void CreatingExchanges()
        {
            
            _channel.ExchangeDeclare(SiteConstants.RabbitLogExchange, ExchangeType.Fanout);
            _channel.ExchangeDeclare(SiteConstants.RabbitCommandExchange, ExchangeType.Fanout);

        }

    }
}
