using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using BlueTapeCrew.Contracts.Services;
using Newtonsoft.Json.Linq;
using ServiceStack.Text;

namespace BlueTapeCrew.Areas.Api.Controllers
{
    public class SubscribeController : ApiController
    {
        private const string SubsciptionMessage = "You have been subscribed to our newsletter.";

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
                return Ok(JObject.FromObject(new { SubsciptionMessage }));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("emailAddress", ex);
                return BadRequest(ModelState);
            }
        }
    }
}
