namespace Site.Security.Jwt
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int TokenExpirationDays { get; set; }
    }
}