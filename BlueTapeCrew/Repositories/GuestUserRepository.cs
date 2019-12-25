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

        public Task<GuestUser> FindBy(string sessionId)
        {
            //todo: fix iqueryable not implemented error 
            foreach (var user in _db.GuestUsers)
                if (user.SessionId == sessionId) return Task.FromResult(user);
            return null;
        }
            

        public async Task Create(GuestUser user)
        {
            _db.GuestUsers.Add(user);
            await _db.SaveChangesAsync();
        }
    }
}
