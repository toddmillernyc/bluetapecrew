using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly BtcEntities _db;
        public ImageRepository(BtcEntities db) { _db = db; }
        public async Task<Image> Find(int id) => await _db.Images.FindAsync(id);
        public async Task Create(Image image)
        {
            _db.Images.Add(image);
            await _db.SaveChangesAsync();
        }
        public bool ImageExists(string name) => _db.Images.Any(x => x.Name == name);

        public async Task Delete(int id)
        {
            var image = await _db.Images.FindAsync(id);
            var productImages = await _db.ProductImages.Where(x => x.ProductId == id).ToListAsync();
            if(productImages.Any()) _db.ProductImages.RemoveRange(productImages);
            _db.Images.Remove(image);
            await _db.SaveChangesAsync();
        }
    }
}
