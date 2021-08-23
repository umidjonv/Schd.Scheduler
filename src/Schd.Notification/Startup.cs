using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Schd.Common;
using Schd.Notification.Models.EventBus;
using Schd.Notification.Data;
using Microsoft.EntityFrameworkCore;

namespace Schd.Notification
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            var config = new AppConfig();

            Configuration.GetSection("Configuration").Bind(config);

            services.AddLogging();

            services.AddDbContext<IAppDbContext, AppDbContext>(options=>options.UseNpgsql(config.DbConnection));

            services.AddMassTransit(x =>
            {
                x.AddConsumer<NotifyConsumer>();

                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(config.RabbitConnection, x =>
                    {
                        x.Username(config.RabbitUsername);
                        x.Password(config.RabbitPassword);
                    });

                    cfg.ReceiveEndpoint("notify-queue", e =>
                    {
                        e.ConfigureConsumer<NotifyConsumer>(ctx);
                    });

                });
            });

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
