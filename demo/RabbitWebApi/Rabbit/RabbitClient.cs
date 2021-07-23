using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitProject.Models;
using RabbitProject.Rabbit.Enums;

namespace RabbitProject.Rabbit
{
    public class RabbitClient:IRabbitClient
    {
        private const string CHANNEL_NAME = "notification";
        private ConnectionFactory _factory;
        private readonly IConnection _connection;
        private IModel _currentChannel;

        private void CreateExchange()
        {
            _currentChannel.ExchangeDeclare(CHANNEL_NAME, ExchangeType.Fanout);
            _currentChannel.QueueDeclare(CHANNEL_NAME, false, false, false);
        }

        public RabbitClient()
        {
            _factory = new ConnectionFactory();
            _factory.Uri = new Uri("amqp://guest:guest@localhost:5672/");

            _connection = _factory.CreateConnection();
            CreateModel();
        }

        public RabbitClient(string username, string password, string hostname)
        {
            _factory = new ConnectionFactory();

            _factory.UserName = username;
            _factory.Password = password;
            _factory.VirtualHost = "/";
            _factory.HostName = hostname;

            _connection = _factory.CreateConnection();
            CreateModel();

        }

        public void CreateModel()
        {
            _currentChannel = _connection.CreateModel();
            CreateExchange();
        }

        public void CloseConnection()
        {
            _currentChannel.Close();
            _connection.Close();
        }

        public void Publish<T>(T model) where T : IBaseModel
        {
            Publish(model, RouteType.Notify);
        }

        public void Publish<T>(T model, RouteType type) where T:IBaseModel
        {
            var data = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            _currentChannel.BasicPublish(CHANNEL_NAME, $"{type}", null, data);
        }
    }

    



}
