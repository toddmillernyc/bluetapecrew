using Entities;

namespace BlueTapeCrew.Extensions
{
    public static class ImageExtensions
    {
        public static string ToHtmlImageSource(this Image image) => image.ImageData.ToHtmlImageSource(image.MimeType);
    }
}
