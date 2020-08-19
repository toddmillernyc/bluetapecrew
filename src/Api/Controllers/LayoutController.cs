using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Controllers
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
        public async Task<IActionResult> Get()
        {
            var vm = await _viewModelService.GetLayoutViewModel();
            return Ok(vm);
        }
    }
}