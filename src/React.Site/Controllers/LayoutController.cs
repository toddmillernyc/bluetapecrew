using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Models;

namespace React.Site.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LayoutController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new LayoutViewModel
            {
                ContactEmail = "info@bluetapecrew.com",
                ContactPhone = "555-555-5555"
            });
        }
    }
}