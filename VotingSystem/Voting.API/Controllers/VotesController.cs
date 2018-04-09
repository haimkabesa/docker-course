using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using ServiceStack.Redis;

namespace Voting.API.Controllers
{
    [Route("api/[controller]")]
    public class VotesController : Controller
    {
        private readonly RedisManagerPool _redisManagerPool;
        private readonly IConnection _rabbitMqConnection;
        private readonly IConfiguration _configuration;

        public VotesController(RedisManagerPool redisManagerPool,
            IConnection rabbitMqConnection,
            IConfiguration configuration)
        {
            _redisManagerPool = redisManagerPool;
            _rabbitMqConnection = rabbitMqConnection;
            _configuration = configuration;
        }
        
        [HttpGet("{option}")]
        public int Get(string option)
        {
            using (var client = _redisManagerPool.GetClient())
            {
                return client.Get<int>(option);
            }
        }

        

        // POST api/values
        [HttpPost("{option}")]
        public void Post(string option)
        {
            using (var channel = _rabbitMqConnection.CreateModel())
            {
                var body = Encoding.UTF8.GetBytes(option);

                channel.BasicPublish(exchange: "",
                    routingKey: "votes",
                    basicProperties: null,
                    body: body);
            }
        }

       
    }
}
