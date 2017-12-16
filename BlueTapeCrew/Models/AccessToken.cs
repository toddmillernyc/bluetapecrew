using System;

namespace BlueTapeCrew.Models
{
    public class AccessToken
    {
        public AccessToken(string token, long secondsUntilExpiration)
        {
            Expiration = ConvertSecondsToExpire(secondsUntilExpiration);
            Token = token;
        }

        public int Id { get; set; }
        public DateTime Expiration { get; set; }
        public string Token { get; set; }
        public TokenType TokenType { get; set; }

        public bool IsExpired()
        {
            return Expiration > DateTime.UtcNow;
        }

        private DateTime ConvertSecondsToExpire(long seconds)
        {
            var now = DateTime.UtcNow;
            var expirationDate = now.AddSeconds(seconds);
            return expirationDate;
        }
    }
}
