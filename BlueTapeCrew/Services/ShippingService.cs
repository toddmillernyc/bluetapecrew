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

        public async Task<decimal> Caclulate(decimal subtotal)
        {
            if (subtotal == 0.00m) return 0.00m;
            var settings = await _siteSettingsService.Get();
            return subtotal >= settings.FreeShippingThreshold 
                    ? 0.00m 
                    : settings.FlatShippingRate;
        }
    }
}