using System;

namespace Services.Extensions
{
    public static class StringExtensions
    {
        public static string ToHtmlImageSource(this byte[] imageBytes, string mimeType) => "data:" + mimeType + ";base64," + Convert.ToBase64String(imageBytes);
    }
}
