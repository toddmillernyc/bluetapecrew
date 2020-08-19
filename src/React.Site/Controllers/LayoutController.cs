using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace React.Site.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LayoutController : ControllerBase
    {
        private readonly IViewModelService _viewModelService;

        public LayoutController(IViewModelService viewModelService)
        {
            _viewModelService = viewModelService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()=> Ok(await _viewModelService.GetLayoutViewModel());
    }
}