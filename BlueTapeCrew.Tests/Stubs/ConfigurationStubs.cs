using System;
using BlueTapeCrew.Models;

namespace BlueTapeCrew.Tests.Stubs
{
    public static class ConfigurationStubs
    {
        public static Uri ProductionCheckoutUri => new Uri("https://bluetapecrew.com/checkout/", UriKind.Absolute);

        public static SendgridSetting SendgridSetting =>  new SendgridSetting { ApiKey = "TestKey", SenderEmail = "sender@test.com", SenderName = "Test Sender" };
}
}
