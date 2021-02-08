using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Site.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscribeController : ControllerBase
    {
        private const string SubscriptionMessage = "{0} has been subscribed to our newsletter.";

        private readonly IEmailSubscriptionService _emailSubscriptionService;

        public SubscribeController(IEmailSubscriptionService emailSubscriptionService)
        {
            _emailSubscriptionService = emailSubscriptionService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(string emailAddress)
        {
            try
            {
                await _emailSubscriptionService.Subscribe(emailAddress);
                var subscriptionMessage = string.Format(SubscriptionMessage, emailAddress);
                return Ok(subscriptionMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
