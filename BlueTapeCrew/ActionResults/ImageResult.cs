using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace BlueTapeCrew.ActionResults
{
    public class ImageResult : ActionResult
    {
        public ImageResult(Stream imageStream, string contentType)
        {
            ImageStream = imageStream ?? throw new ArgumentNullException(nameof(imageStream));
            ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
        }

        public Stream ImageStream { get; }
        public string ContentType { get; }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            try
            {
                var response = context.HttpContext.Response;
                response.ContentType = ContentType;

                var buffer = new byte[4096];
                while (true)
                {
                    var read = ImageStream.Read(buffer, 0, buffer.Length);
                    if (read == 0) break;
                    await response.BodyWriter.WriteAsync(buffer);
                }
                await response.CompleteAsync();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}