using System;
using System.Linq;
using System.Threading.Tasks;
using BlueTapeCrew.Email;
using BlueTapeCrew.Models;
using BlueTapeCrew.Services.Interfaces;
using Btc.Tests.Stubs;
using Xunit;

namespace Btc.Tests.Integration
{
    [Collection("IntegrationTest")]
    public class EmailTests
    {
        private readonly IntegrationTextFixture _fixture;

        public EmailTests(IntegrationTextFixture fixture) { _fixture = fixture; }

        [Fact]
        public async Task SendEmail_GivenValidSmtpCredentials_SendsEmail()
        {
            try
            {
                //arrange
                var settingsService = _fixture.Resolve<ISiteSettingsService>();
                var settings = await settingsService.Get();
                var sut = _fixture.Resolve<IEmailService>();
                var testAddress = Guid.NewGuid().ToString().Substring(0, 5) + "@mailinator.com";
                var order = OrderStubs.Orders(testAddress).FirstOrDefault();
                var textBody = EmailTemplates.GetOrderConfirmationTextBody(order, true);
                var htmlBody = EmailTemplates.GetOrderConfirmationHtmlBody(order);
                var request = new SmtpRequest(settings, htmlBody, textBody, order.Email);

                //act
                await sut.SendEmail(request);

                //assert
                Assert.True(true);
            }
            catch (Exception ex)
            {
                Assert.True(false, ex.Message);
            }
        }
    }
}
