using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;


namespace Trading.RabbitMQ.Core
{
    public class RabbitClient
    {
         
        private static async Task CreateVHost(HttpClient client, StringContent content, string host, string name)
        {
            try
            {
                var response = await client.PutAsync($"http://{host}:15672/api/vhosts/{name}", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static async Task CreateVHosts(string host, string user, string password, params string[] names)
        {
            using var client = new HttpClient();

            var model = new
            {
            };
            using var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var authData = Encoding.UTF8.GetBytes($"{user}:{password}");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authData));

            foreach (var name in names)
            {
                await CreateVHost(client, content, host, name);
            }
        }

        public IModel CreateChannel(string host, string vhost, string user, string password)
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

            return channel;
        }

    }
}
