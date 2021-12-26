using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Schd.Common;
using Schd.Notification.Data;
using Microsoft.EntityFrameworkCore;
using Schd.Notification.Api.EventBus.Models;
using Schd.Notification.Api.EventBus.Abstractions;
using Schd.Notification.Api.Infrastructure;
using Schd.Notification.Api.EventBus.Providers;
using Schd.Notification.Api.EventBus;
using Schd.Notification.EventBus;
using System.Collections.Generic;
using Schd.Notification.Api.Services;
using System;

namespace Schd.Notification
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureEventBus(IServiceCollection services, AppConfig config)
        {
            var client = new AppService();
            Configuration.GetSection("AppService").Bind(client);
            services.AddSingleton(client);

            services.AddSingleton<IIntegrationEventHandler, LogNotificationHandler>();
            services.AddSingleton<IIntegrationEventHandler, MessageNotificationHandler>();
            services.AddSingleton<IIntegrationEventHandler, CommandNotificationHandler>();
            
            services.AddSingleton<EventBusManager>();

            services.AddScoped(services=>new RegistrationService(client, 
                new List<IIntegrationEventHandler> 
                {
                    services.GetService<LogNotificationHandler>(),
                    services.GetService<MessageNotificationHandler>(),
                    services.GetService<CommandNotificationHandler>()
                }
                ));

            var rabbitProvider = new RabbitProvider(config.RabbitConnection, config.RabbitUsername, config.RabbitPassword);

            services.AddSingleton(rabbitProvider);

            //var rabbitEventBus = new RabbitEventBus();
            services.AddSingleton<RabbitEventBus>();
            

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var config = new AppConfig();
            var dbConnection = Configuration.GetConnectionString("DefaultConnection");
            Configuration.GetSection("Configuration").Bind(config);

            
            services.AddLogging();
            services.AddDbContext<IAppDbContext, AppDbContext>(options=>options.UseNpgsql(dbConnection));

            services.AddMassTransitHostedService();


            services.AddSwaggerGen();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
               
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NotifyMessage API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            
            //ConfigureEventBus(app);
        }
    }
}
