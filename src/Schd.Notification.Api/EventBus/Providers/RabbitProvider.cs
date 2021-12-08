using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OpenApi.Extensions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Schd.Common.Helpers;
using Schd.Notification.Data.Enums;

namespace Schd.Notification.Api.EventBus.Providers
{
    public class RabbitProvider:IRabbitProvider
    {
        private readonly string _exchangeName;
        private readonly string _routeKey;

        public IModel Channel { get; set; }

        public RabbitProvider(string exchangeName, string routeKey)
        {
            _exchangeName = exchangeName;
            _routeKey = routeKey;
        }

        public async Task<bool> Connect(string connection)
        {
            using var client = new HttpClient();

            var response = await client.PutAsync(connection, null);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }

            return true;
        }

        public async Task<bool> Connect(string connection, string user, string password, string host, string vhost = "default_host")
        {
            var authToken = Encoding.ASCII.GetBytes($"{user}:{password}");

            using var client = new HttpClient()
            {
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken))
                }
            };

            var response = await client.PutAsync($"http://{host}:15672/api/vhosts/{vhost}", null);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }

            return true;
        }

        public async Task<bool> Connect(HttpClient client, string host, string vhost)
        {

            var response = await client.PutAsync($"http://{host}:15672/api/vhosts/{vhost}", null);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }

            return true;
        }

        public void CreateChannel(string host, string vhost, string user, string password)
        {
            var factory = new ConnectionFactory()
            {
                HostName = host,
                VirtualHost = vhost,
                UserName = user,
                Password = password
            };
            var connection = factory.CreateConnection(host);
            var channel = connection.CreateModel();
            
            Channel = channel;
            
        }

        public void DeclareExchange(string exchange)
        {
            Channel.ExchangeDeclare(exchange, ExchangeType.Direct);
        }


        public void QueueDeclare(string queueName = "default_queue")
        {
            Channel.QueueDeclare(queueName);
        }

        public void Bind(string exchangeName, string queue, string route)
        {
            Channel.QueueBind(queue, exchangeName, route);
        }

        public AsyncDefaultBasicConsumer Consume(string queue)
        {
            var consumer = new AsyncEventingBasicConsumer(Channel);
            
            Channel.BasicConsume(queue, false, consumer);
            
            return consumer;
        }

        public void Publish(string exchangeName, object data, string route)
        {
            byte[] messageBodyBytes = data.ObjectToBytes();
            
            //IBasicProperties props = Channel.CreateBasicProperties();
            //props.ContentType = "text/plain";
            //props.DeliveryMode = 2;
            //props.Expiration = "36000000";

            Channel.BasicPublish(exchangeName, route, null, messageBodyBytes);
        }

        
    }
}
