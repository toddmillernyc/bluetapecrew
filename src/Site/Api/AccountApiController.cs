using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Site.Security.Jwt;
using Site.Services;
using Site.ViewModels;

namespace Site.Api
{
    [JwtAuthorize]
    [ApiController]
    [Route("api/account")]
    public class AccountApiController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;

        public AccountApiController(IUserService userService, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }

        public async Task<ManageViewModel> Get()
        {
            var user = await _userService.Find(User.Identity?.Name);
            var orders = await _orderService.GetBy(User.Identity?.Name);
            var model = new ManageViewModel(user, orders);
            return model;
        }
    }
}
