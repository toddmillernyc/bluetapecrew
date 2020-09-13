using Entities;
using Microsoft.AspNetCore.Mvc;
 
namespace React.Site.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly BtcEntities _db;

        public ApiController(BtcEntities db)
        {
            _db = db;
        }

        public IActionResult Get()
        {
            return Ok(_db.Categories);
        }
    }
}
