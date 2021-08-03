using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.BuildingBlocks.EventBusRabbitMQ;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Schd.Common;
using Schd.Notification.Models.EventBus;

namespace Schd.Notification
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            var config = new AppConfig();

            Configuration.GetSection("Configuration").Bind(config);

            services.AddLogging();

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(config.RabbitConnection);//, x =>
                    //{
                    //    x.Username(config.RabbitUsername);
                    //    x.Password(config.RabbitPassword);
                    //});

                    cfg.ReceiveEndpoint("notify-queue", e =>
                    {
                        e.ConfigureConsumer<NotifyConsumer>(ctx);
                    });

                });
            });

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = config.RabbitConnection,
                    DispatchConsumersAsync = true
                };

                
                factory.UserName = config.RabbitUsername;
                factory.Password = config.RabbitPassword;
                

                var retryCount = 5;
                if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            services.AddSwaggerGen();


            services.AddMassTransitHostedService();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NotifyMessage API V1");
                c.RoutePrefix = string.Empty;
            });


            app.UseRouting();
            
        }
    }
}
