using Microsoft.AspNetCore.Http;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface ICookieService
    {
        void SetCurrentProduct(HttpContext context, int productId);
        void SetCurrentCategory(HttpContext context, string categoryName);
    }
}