using System;

namespace BlueTapeCrew.Tests.Stubs
{
    public static class ConfigurationStubs
    {
        public static Uri ProductionCheckoutUri => new Uri("https://bluetapecrew.com/checkout/", UriKind.Absolute);
}
}
