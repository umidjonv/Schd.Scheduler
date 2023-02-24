using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Schd.EventBus;
using Schd.Notification.Api.EventBus.Abstractions;
using Schd.Notification.Api.EventBus.Models;
using Schd.Notification.Api.EventBus.Providers;
using Schd.Notification.Data.Enums;

namespace Schd.Notification.Api.EventBus
{
    public interface IRabbitProvider:IProvider
    {
        //IModel Channel { get; set; }
        Task<bool> Connect(string connection);
        Task<bool> Connect(string connection, string user, string password, string host, string vhost = "default_host");
        Task<bool> Connect(HttpClient client, string host, string vhost);
        //void CreateChannel(string host, string vhost, string user, string password);

        void DeclareExchange(string exchangeName);
        void QueueDeclare(string queueName);
        void Bind(string exchangeName, string queue, string route);
        void Publish(string exchangeName, object data, string route);
        void Consume<T, TH>(string queue) where T : NotifyEvent
            where TH : IIntegrationEventHandler<T>;

        
            


    }
}
