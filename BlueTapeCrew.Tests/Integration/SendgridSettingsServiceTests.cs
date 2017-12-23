using System.Threading.Tasks;
using BlueTapeCrew.Tests.Stubs;
using Xunit;

namespace BlueTapeCrew.Tests.Integration
{
    [Collection("IntegrationTest")]
    public class SendgridSettingsServiceTests
    {
        private readonly IntegrationTextFixture _fixture;
        public SendgridSettingsServiceTests(IntegrationTextFixture fixture) {_fixture = fixture; }

        [Fact]
        public async Task SetSendgridSettings_GivenValidSendgridSettings_CreatesSendgridSettings()
        {
            //arrange
            var sendgridSetting = ConfigurationStubs.SendgridSetting;

            //act
            var acutal = await _fixture.SendgridSettingsService.Set(sendgridSetting);

            //assert
            Assert.True(acutal.Id > 0);

            //teardown
            _fixture.Teardown(acutal);
        }
    }
}
