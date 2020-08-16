using Microsoft.AspNetCore.Http;

namespace Site.Services
{
    public class CookieService : ICookieService
    {
        public void SetCurrentProduct(HttpContext context, int productId)
        {
            context.Response.Cookies.Append("CurrentProductId", productId.ToString());
        }

        public void SetCurrentCategory(HttpContext context, string categoryName)
        {
            context.Response.Cookies.Append("CurrentCategoryName", categoryName);
        }
    }
}