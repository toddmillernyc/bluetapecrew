using System.IO;
using Microsoft.AspNetCore.Http;

namespace Site.Extensions
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
