using System;
using System.Linq;

namespace BlueTapeCrew.Extensions
{
    public static class GuidExtensions
    {
        public static string ToSessionIdString(this Guid input) => input.ToString().Replace("-", "").Substring(0,24);
    }
}