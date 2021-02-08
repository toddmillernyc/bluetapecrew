using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;
using Site.Security.Identity;
using Site.Services;
using Site.ViewModels;

namespace Site.Controllers
{
    [Authorize]
    [RequireHttps]
    public class ManageController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IUserRegistrationService _userRegistration;
        private readonly IUserService _userService;
        
        public ManageController(
            IMapper mapper,
            IOrderService orderService,
            IUserRegistrationService userRegistration,
            IUserService userService)
        {
            _mapper = mapper;
            _orderService = orderService;
            _userRegistration = userRegistration;
            _userService = userService;
        }

        [Route("account")]
        public async Task<IActionResult> Index()
        {
            var user = await _userService.Find(User.Identity?.Name);
            var orders = await _orderService.GetBy(User.Identity?.Name);
            var model = new ManageViewModel(user, orders);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("account")]
        public async Task<IActionResult> Index(ApplicationUser user)
        {
            //this mapping shouldn't need to be here
            //todo: consolidate and refactor these models
            var updateModel = _mapper.Map<CheckoutRequest>(user);
            await _userService.UpdateUser(updateModel);
            return RedirectToAction("Index","Manage");
        }

        public ActionResult ChangePassword() => View();
        public ActionResult SetPassword() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (await _userRegistration.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
            {
                await _userRegistration.SignInBy(User.Identity.Name);
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (await _userRegistration.SetPassword(User.Identity.Name, model.NewPassword))
            {
                await _userRegistration.SignInBy(User.Identity.Name);
                return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
            }
            return View(model);
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            Error
        }
    }
}