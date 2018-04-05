using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Voting.API.Controllers
{
    [Route("api/[controller]")]
    public class VotesController : Controller
    {
        
        [HttpGet("{option}")]
        public int Get(string option)
        {
            return 0;
        }

        

        // POST api/values
        [HttpPost("{option}")]
        public void Post(string option)
        {

        }

       
    }
}
