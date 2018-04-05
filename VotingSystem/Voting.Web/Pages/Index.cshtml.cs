using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Voting.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly VotesClient _votesClient;

        public IndexModel(IConfiguration configuration, VotesClient votesClient)
        {
            _configuration = configuration;
            _votesClient = votesClient;
            Option1 = configuration["Option1"];
            Option2 = configuration["Option2"];
        }

        public string Option2 { get; set; }

        public string Option1 { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            await _votesClient.AddVoteAsync(id);
            return new OkResult();
        }
    }
}
