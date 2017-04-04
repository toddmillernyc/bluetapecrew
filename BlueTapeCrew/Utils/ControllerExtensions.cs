using System.IO;
using System.Web.Mvc;

namespace BlueTapeCrew.Utils
{ 
    public static class ControllerExtensions
    {
        public static ImageResult Image(this Controller controller, byte[] imageBytes, string contentType)
        {
            return new ImageResult(new MemoryStream(imageBytes), contentType);
        }
    }
}