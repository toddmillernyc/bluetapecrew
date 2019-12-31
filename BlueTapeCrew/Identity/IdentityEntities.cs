using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlueTapeCrew.Identity
{
    public class IdentityEntities : IdentityDbContext<ApplicationUser>
    {
        public IdentityEntities(DbContextOptions<IdentityEntities> options) : base(options) { }
    }
}