namespace Site.Security.Jwt.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateJwtToken(string userId);
    }
}
