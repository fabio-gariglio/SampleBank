using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace SampleBank.API.Controllers
{
    [Route("accounts")]
    public class AccountsController : Controller
    {
        [HttpGet]
        public IEnumerable<object> Get()
        {
            return new[]
            {
                new {value = true}
            };
        }
    }
}
