using BlueTapeCrew.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlueTapeCrew.Areas.Api.Controllers
{
    public class CategoriesController : ApiController
    {
        private readonly BtcEntities _db;

        public CategoriesController(BtcEntities db)
        {
            _db = db;
        }

        public async Task<Dictionary<int, string>> Get() =>
            await _db.Categories.ToDictionaryAsync(x => x.Id, x => x.CategoryName);
    }

}
