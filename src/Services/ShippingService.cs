using System.Threading.Tasks;
using Services.Interfaces;

namespace Services
{
    public class ShippingService : IShippingService
    {
        private readonly ISiteSettingsService _siteSettingsService;

        public ShippingService(ISiteSettingsService siteSettingsService)
        {
            _siteSettingsService = siteSettingsService;
        }

        public async Task<decimal> Calculate(decimal subtotal)
        {
            if (subtotal == 0.00m) return 0.00m;
            var profile = await _siteSettingsService.GetSiteProfile();
            var freeShippingThreshold = profile?.FreeShippingThreshold ?? 0.0m;
            var flatShippingRate = profile?.FlatShippingRate ?? 0.0m;
            return subtotal >= freeShippingThreshold
                ? 0.00m
                : flatShippingRate;
        }
    }
}