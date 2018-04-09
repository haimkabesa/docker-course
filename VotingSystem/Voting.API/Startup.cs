using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using ServiceStack.Redis;

namespace Voting.API
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
            services.AddMvc();
            services.AddSingleton<RedisManagerPool>(_ => new RedisManagerPool(Configuration["REDIS"]));
            services.AddSingleton<IConnection>(_ =>
            {
                var factory = new ConnectionFactory() { HostName = Configuration["RABBITMQ"] };
                factory.UserName = Configuration["RABBITMQ_USER"];
                factory.Password = Configuration["RABBITMQ_PASS"];
                var connection = factory.CreateConnection();
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "votes",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                }

                return connection;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }

    
}
