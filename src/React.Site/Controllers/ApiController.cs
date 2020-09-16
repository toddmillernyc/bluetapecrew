using Entities;
using HotChocolate;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Mvc;
using React.Site.GraphQL;

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
            var schema = SchemaBuilder.New()
                .AddQueryType<Query>()
                .Create();

            var executor = schema.MakeExecutable();
            var json = executor.Execute("{ hello }").ToJson();
            return Ok(json);
        }
    }
}
