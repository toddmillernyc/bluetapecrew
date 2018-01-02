using System;
using System.Data.Entity;
using System.Threading.Tasks;
using BlueTapeCrew.Contracts.Repositories;
using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;

namespace BlueTapeCrew.Repositories
{
    public class AccessTokenRepository : IAccessTokenRepository, IDisposable
    {
        private readonly BtcEntities _db;

        public AccessTokenRepository()
        {
            _db = new BtcEntities();
        }

        public async Task<AccessToken> GetFirst(TokenType tokenType)
        {
            return await _db.AccessTokens.FirstOrDefaultAsync(x => x.TokenType == tokenType);
        }

        public async Task Create(AccessToken token)
        {
            _db.AccessTokens.Add(token);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {

            var accessToken = await _db.AccessTokens.FindAsync(id);
            if (accessToken != null)
            {
                _db.AccessTokens.Remove(accessToken);
                await _db.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}