using BlueTapeCrew.Data;
using BlueTapeCrew.Repositories.Interfaces;
using Entities;
using System.Threading.Tasks;

namespace BlueTapeCrew.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly BtcEntities _db;
        public ImageRepository(BtcEntities db) { _db = db; }
        public async Task<Image> Find(int id) => await _db.Images.FindAsync(id);
    }
}
