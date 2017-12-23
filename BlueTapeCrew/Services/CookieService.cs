﻿using System.Web;
using BlueTapeCrew.Contracts.Services;

namespace BlueTapeCrew.Services
{
    public class CookieService : ICookieService
    {
        public void SetCurrentProduct(HttpContext context, int productId)
        {
            context.Response.Cookies["UserSettings"]["CurrendProductId"] = productId.ToString();
        }

        public void SetCurrentCategory(HttpContext context, string categoryName)
        {
            context.Response.Cookies["UserSettings"]["CurrentCategoryName"] = categoryName;
        }
    }
}