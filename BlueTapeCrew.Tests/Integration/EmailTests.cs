using System.Linq;
using System.Threading.Tasks;
using BlueTapeCrew.Email;
using BlueTapeCrew.Models;
using BlueTapeCrew.Tests.Stubs;
using Xunit;

namespace BlueTapeCrew.Tests.Integration
{
    [Collection("IntegrationTest")]
    public class EmailTests
    {
        private readonly IntegrationTextFixture _fixture;

        public EmailTests(IntegrationTextFixture fixture) { _fixture = fixture; }

        [Fact(Skip = "Skipping Integration Tests while setting up azure pipeline")]
        public async Task SendEmail_GivenValidSmtpCretentials_SendsEmail()
        {
            
            //arrange
            var settings = _fixture.SiteSettings;
            var order = OrderStubs.Orders(settings.ContactEmailAddress).FirstOrDefault();
            var textBody = EmailTemplates.GetOrderConfirmationTextBody(order, true);
            var htmlBody = EmailTemplates.GetOrderConfirmationHtmlBody(order);
            var request = new SmtpRequest(settings, htmlBody, textBody, order.Email);

            //act
            await _fixture.EmailService.SendEmail(request);

            //assert
        }



    }
}
