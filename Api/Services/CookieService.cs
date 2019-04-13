using Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Api.Services
{
    public class CookieService : ICookieService
    {
        public void SetCurrentProduct(HttpContext context, int productId)
        {
            context.Response.Cookies.Append("UserSettings.CurrentProductId", productId.ToString());
        }

        public void SetCurrentCategory(HttpContext context, string categoryName)
        {
            context.Response.Cookies.Append("UserSettings.CurrentCategoryName", categoryName);
        }
    }
}