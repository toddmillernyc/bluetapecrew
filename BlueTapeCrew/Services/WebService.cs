using BlueTapeCrew.Interfaces;
using System;

namespace BlueTapeCrew.Services
{
    public class WebService : IWebService
    {
        public string FormatAuthorizationCredentials(string username, string password)
        {
            var authorizationString = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));
            return authorizationString;
        }


    }
}