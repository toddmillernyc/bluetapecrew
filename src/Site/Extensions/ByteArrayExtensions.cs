using System;
using System.IO;
using Site.ActionResults;

namespace Site.Extensions
{
    public static class ByteArrayExtensions
    {
        public static ImageResult ToImageResult(this byte[] imageBytes, string mimeType) => new ImageResult(new MemoryStream(imageBytes), mimeType);
        public static string ToHtmlImageSource(this byte[] imageBytes, string mimeType) => "data:" + mimeType + ";base64," + Convert.ToBase64String(imageBytes);
    }
}