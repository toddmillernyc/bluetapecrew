using BlueTapeCrew.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlueTapeCrew.Areas.Api.Controllers
{
    public class SubscribeController : ApiController
    {
        private const string SubsciptionMessage = "{0} has been subscribed to our newsletter.";

        private readonly IEmailSubscriptionService _emailSubscriptionService;

        public SubscribeController(IEmailSubscriptionService emailSubscriptionService)
        {
            _emailSubscriptionService = emailSubscriptionService;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(string emailAddress)
        {
            try
            {
                await _emailSubscriptionService.Subscribe(emailAddress);
                var subscriptionMessage = string.Format(SubsciptionMessage, emailAddress);
                return Ok(JObject.FromObject(new { subscriptionMessage }));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("emailAddress", ex);
                return BadRequest(ModelState);
            }
        }
    }
}
