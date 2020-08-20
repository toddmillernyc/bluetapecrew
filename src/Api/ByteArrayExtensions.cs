using System.IO;

namespace Api
{
    public static class ByteArrayExtensions
    {
        public static ImageResult ToImageResult(this byte[] imageBytes, string mimeType) => new ImageResult(new MemoryStream(imageBytes), mimeType);
    }
}