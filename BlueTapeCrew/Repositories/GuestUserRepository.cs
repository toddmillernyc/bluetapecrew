using BlueTapeCrew.Data;
using BlueTapeCrew.Repositories.Interfaces;
using Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace BlueTapeCrew.Repositories
{
    public class GuestUserRepository : IGuestUserRepository
    {
        private readonly BtcEntities _db;

        public GuestUserRepository(BtcEntities db)
        {
            _db = db;
        }

        public async Task<GuestUser> FindBy(string sessionId) => 
            await _db.GuestUsers.FirstOrDefaultAsync(x => x.SessionId.Equals(sessionId));

        public async Task Create(GuestUser user)
        {
            _db.GuestUsers.Add(user);
            await _db.SaveChangesAsync();
        }
    }
}
