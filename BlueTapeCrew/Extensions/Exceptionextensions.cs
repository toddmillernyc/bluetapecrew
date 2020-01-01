using System;

namespace BlueTapeCrew.Extensions
{
    public static class ExceptionExtensions
    {
        public static string ToInnerExceptionMessage(this Exception ex)
        {
            while (ex.InnerException != null) ex = ex.InnerException;
            return ex.Message;
        }
    }
}
