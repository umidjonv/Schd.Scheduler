using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Schd.Common;
using Schd.Notification.Data;
using Microsoft.EntityFrameworkCore;
using Schd.Notification.Api.EventBus.Abstractions;
using Schd.Notification.Api.Infrastructure;
using Schd.Notification.Api.EventBus.Providers;
using Schd.Notification.Api.EventBus;
using Schd.Notification.EventBus;
using System.Collections.Generic;
using Schd.Notification.Api.Services;
using System;
using Schd.Notification.Api.Infrastructure.Hubs;

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
            
            //services.AddSingleton(client);

            services.AddSingleton<LogNotificationHandler>();
            services.AddSingleton<MessageNotificationHandler>();
            services.AddSingleton<CommandNotificationHandler>();

            

            services.AddSingleton<EventBusManager>();

            var rabbitProvider = new RabbitProvider(config.RabbitConnection, config.RabbitUsername, config.RabbitPassword);
            services.AddSingleton(rabbitProvider);

            services.AddSingleton<RabbitEventBus>();

            

            services.AddHostedService<RegistrationService>();

            services.AddCors(options =>
            {

                options.AddPolicy("AllowAll", builder => builder.SetIsOriginAllowed(a => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            });

            //var rabbitEventBus = new RabbitEventBus();



        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var config = new AppConfig();
            var dbConnection = Configuration.GetConnectionString("DefaultConnection");
            Configuration.GetSection("Configuration").Bind(config);

            
            services.AddLogging();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); 
            services.AddDbContext<IAppDbContext, AppDbContext>(options=>options.UseNpgsql(dbConnection));

            ConfigureEventBus(services, config);

            services.AddSignalR();
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
            app.UseCors("AllowAll");
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<LogHub>("/hub/log");
                endpoints.MapHub<MessageHub>("/hub/message");
                endpoints.MapHub<CommandHub>("/hub/command");
                endpoints.MapControllers();
            });
            
            
            //ConfigureEventBus(app);
        }
    }
}
