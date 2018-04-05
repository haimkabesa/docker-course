using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Voting.Web
{
    public class VotesClient
    {
        //In this case we are encapsulating the HttpClient,
        //but it could be exposed if that is desired.
        private HttpClient _client;
        private ILogger<VotesClient> _logger;

        public VotesClient(IConfiguration configuration, ILogger<VotesClient> logger)
        {
            _client = new HttpClient(){BaseAddress = new Uri($"http://{configuration["votes-api-address"]}/")};
            _logger = logger;
        }

        public async Task<int> GetCount(string option)
        {
            try
            {
                //Here we are making the assumption that our HttpClient instance
                //has already had its base address set.
                var response = await _client.GetAsync($"api/votes/{option}");
                response.EnsureSuccessStatusCode();

                return int.Parse(await response.Content.ReadAsStringAsync());
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occured connecting to values API {ex.ToString()}");
                return 0;
            }
        }

        public async Task AddVoteAsync(string option)
        {
            await _client.PostAsync("api/votes",
                new FormUrlEncodedContent(new[] {new KeyValuePair<string, string>("option", option),}));
        }
    }
}