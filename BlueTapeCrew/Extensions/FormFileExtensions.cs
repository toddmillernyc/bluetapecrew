using Microsoft.AspNetCore.Http;
using System.IO;

namespace BlueTapeCrew.Extensions
{
    public static class FormFileExtensions
    {
        public static byte[] ToBytes(this IFormFile file)
        {
            if (file.Length == 0) return new byte[0];
            using var ms = new MemoryStream();
            file.CopyTo(ms);
            return ms.ToArray();
        }
    }
}
