using BlueTapeCrew.Data;
using BlueTapeCrew.Models.Entities;
using BlueTapeCrew.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BlueTapeCrew.Controllers
{
    [Authorize]
    [RequireHttps]
    public class ManageController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly BtcEntities _db;

        public ManageController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, BtcEntities db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        private async Task<ApplicationUser> GetCurrentUser() => await _userManager.FindByNameAsync(User.Identity.Name);

        [Route("account")]
        public async Task<IActionResult> Index(ManageMessageId? message)
        {
            return View(new ManageViewModel
            {
                Orders =
                    _db.Orders.Include(x => x.OrderItems)
                        .Where(x => x.UserName.Equals(User.Identity.Name))
                        .OrderByDescending(x => x.DateCreated).ToList(),
                User = await GetCurrentUser()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("account")]
        public async Task<IActionResult> Index(ApplicationUser user)
        {
            var model = await _userManager.FindByIdAsync(user.Id);
            if (model != null)
            {
                model.Email = user.Email;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.PhoneNumber = user.PhoneNumber;
                model.Address = user.Address;
                model.City = user.City;
                model.State = user.State;
                model.PostalCode = user.PostalCode;
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Index","Manage");
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUser();
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                if (user != null)
                {
                    await _signInManager.SignInAsync(user, false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await GetCurrentUser();
            if (user == null) return NotFound("User not found");
            var result = await _userManager.AddPasswordAsync(user, model.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
            }
            AddErrors(result);

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            _db?.Dispose();
            _userManager?.Dispose();
            base.Dispose(disposing);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            Error
        }
    }
}