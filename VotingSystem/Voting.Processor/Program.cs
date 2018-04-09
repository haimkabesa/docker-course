using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceStack.Redis;

namespace Voting.Processor
{
    class Program
    {
        static void Main(string[] args)
        {
            var redisManagerPool = new RedisManagerPool(Environment.GetEnvironmentVariable("REDIS"));
            var factory = new ConnectionFactory() { HostName = Environment.GetEnvironmentVariable("RABBITMQ") };
            factory.UserName = Environment.GetEnvironmentVariable("RABBITMQ_USER");
            factory.Password = Environment.GetEnvironmentVariable("RABBITMQ_PASS");

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "votes",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                    using (var redisClient=redisManagerPool.GetClient())
                    {
                        redisClient.IncrementValue(message);
                    }
                };
                channel.BasicConsume(queue: "votes",
                    autoAck: true,
                    consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
