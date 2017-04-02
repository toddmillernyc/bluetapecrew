using System;
using System.Diagnostics;
using System.IO;
using System.Web.Mvc;

namespace BlueTapeCrew.Utils
{
    public class ImageResult : ActionResult
    {
        public ImageResult(Stream imageStream, string contentType)
        {
            if (imageStream == null)
                throw new ArgumentNullException(nameof(imageStream));
            if (contentType == null)
                throw new ArgumentNullException(nameof(contentType));
            ImageStream = imageStream;
            ContentType = contentType;
        }
        public Stream ImageStream { get; }
        public string ContentType { get; }
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var response = context.HttpContext.Response;

            response.ContentType = ContentType;

            var buffer = new byte[4096];
            while (true)
            {
                var read = ImageStream.Read(buffer, 0, buffer.Length);
                if (read == 0)
                    break;

                response.OutputStream.Write(buffer, 0, read);
            }
            try
            {
                response.End();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);   
            }
        }
    }
}
