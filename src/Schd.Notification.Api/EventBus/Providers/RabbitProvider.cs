using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OpenApi.Extensions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Schd.Common.Helpers;
using Schd.Notification.Api.EventBus.Abstractions;
using Schd.Notification.Api.EventBus.Models;
using Schd.Notification.Data.Enums;

namespace Schd.Notification.Api.EventBus.Providers
{
    public class RabbitProvider:IProvider
    {
        
        public IModel Channel { get; set; }
        private IConnection _connection;
        public RabbitProvider()
        {
        }
        
        public RabbitProvider(string host, string user, string password, string vhost = "/")
        {
            _ = this.Connect(host, user, password,  vhost);
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

        public async Task<bool> Connect(string host, string user, string password,  string vhost = "/")
        {
            var authToken = Encoding.ASCII.GetBytes($"{user}:{password}");

            try
            {

                //using var client = new HttpClient()
                //{
                //    DefaultRequestHeaders =
                //{
                //    Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken))
                //}
                //};

                //var response = await client.PutAsync($"http://{host}:15672/api/vhosts/{vhost}", null);

                //if (!response.IsSuccessStatusCode)
                //{
                //    throw new Exception(response.ReasonPhrase);
                //}

                var factory = new ConnectionFactory()
                {
                    HostName = host,
                    VirtualHost = vhost,
                    UserName = user,
                    Password = password
                };
                _connection = factory.CreateConnection(host);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");                
            }

            CreateChannel();
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

        public void CreateChannel()
        {
            if (_connection.IsOpen)
            {
                var channel = _connection.CreateModel();

                Channel = channel;
            }
            
            
        }

        public void DeclareExchange(string exchange)
        {
            if (Channel != null && _connection.IsOpen)
                Channel.ExchangeDeclare(exchange, ExchangeType.Direct);
            else
                throw new Exception("Not connected");
        }


        public void QueueDeclare(string queueName = "default_queue")
        {
            Channel.QueueDeclare(queueName);
        }

        public void Bind(string exchangeName, string queue, string route)
        {
            Channel.QueueBind(queue, exchangeName, route);
        }

        public void Publish(string exchangeName, object data, string route)
        {
            byte[] messageBodyBytes = JsonConvert.SerializeObject(data).ObjectToBytes();

            //IBasicProperties props = Channel.CreateBasicProperties();
            //props.ContentType = "text/plain";
            //props.DeliveryMode = 2;
            //props.Expiration = "36000000";

            Channel.BasicPublish(exchangeName, route, null, messageBodyBytes);
        }



    }
}
