using System.IO;
using BlueTapeCrew.ActionResults;

namespace BlueTapeCrew.Extensions
{
    public static class ByteArrayExtensions
    {
        public static ImageResult ToImageResult(this byte[] imageBytes, string mimeType) => new ImageResult(new MemoryStream(imageBytes), mimeType);
    }
}