using BlueTapeCrew.Services.Interfaces;
using System.Threading.Tasks;

namespace BlueTapeCrew.Services
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
            var settings = await _siteSettingsService.Get();
            var freeShippingThreshold = settings?.FreeShippingThreshold ?? 0.0m;
            var flatShippingRate = settings?.FlatShippingRate ?? 0.0m;
            return subtotal >= freeShippingThreshold
                ? 0.00m
                : flatShippingRate;
        }
    }
}