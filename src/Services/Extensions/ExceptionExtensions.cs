using System;

namespace Services.Extensions
{
    public static class ExceptionExtensions
    {
        public static Exception ToInner(this Exception ex)
        {
            while (ex.InnerException != null) ex = ex.InnerException;
            return ex;
        }
    }
}
