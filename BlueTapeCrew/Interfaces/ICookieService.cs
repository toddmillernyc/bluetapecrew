using System.Web;

namespace BlueTapeCrew.Interfaces
{
    public interface ICookieService
    {
        void SetCurrentProduct(HttpContext context, int productId);
        void SetCurrentCategory(HttpContext context, string categoryName);
    }
}