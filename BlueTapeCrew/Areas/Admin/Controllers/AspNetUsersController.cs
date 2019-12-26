using BlueTapeCrew.Areas.Admin.Models;
using BlueTapeCrew.ViewModels;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BlueTapeCrew.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AspNetUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AspNetUsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return BadRequest("User id NULL");
            var aspNetUser = await _userManager.FindByIdAsync(id);
            if (aspNetUser == null) return NotFound();
            return View(aspNetUser);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AdminCreateUserModel model)
        {
            if(model.Password != model.ConfirmPassword)
                ModelState.AddModelError("ConfirmPassword", "Passwords Don't match");
            if(await _userManager.FindByEmailAsync(model.Email) != null)
                ModelState.AddModelError("Email", "The email already exists");
            if (!ModelState.IsValid) return View(model);

            try
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded == false)
                {
                    foreach (var identityError in result.Errors)
                    {
                        ModelState.AddModelError("", identityError.Description);
                        return View(model);
                    }
                }
                if (model.IsAdmin)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", ex.Message);
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return BadRequest("User Id null");
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var model = new EditUserViewModel(user);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var user = await _userManager.FindByIdAsync(vm.Id);
            await _userManager.UpdateAsync(vm.Map(user));
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) BadRequest();
            var aspNetUser = await _userManager.FindByIdAsync(id);
            if (aspNetUser == null) NotFound();
            return View(aspNetUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return RedirectToAction("Index");

            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) => _userManager?.Dispose();
    }
}
