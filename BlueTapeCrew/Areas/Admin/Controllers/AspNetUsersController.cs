using BlueTapeCrew.Areas.Admin.Models;
using BlueTapeCrew.Data;
using BlueTapeCrew.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace BlueTapeCrew.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AspNetUsersController : Controller
    {
        private readonly BtcEntities _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public AspNetUsersController(UserManager<ApplicationUser> userManager, BtcEntities db)
        {
            _userManager = userManager;
            _db = db;
        }

        private async Task<ApplicationUser> FindUser(string id) => await _userManager.FindByIdAsync(id);

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return BadRequest("User id NULL");
            var aspNetUser = await FindUser(id);
            if (aspNetUser == null) return NotFound();
            return View(aspNetUser);
        }

        public ActionResult Create()
        {
            return View();
        }

        //todo: fix bugs in view
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
                if(result.Succeeded == false) throw new Exception("There was an error creating user");
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
            var user = await FindUser(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationUser aspNetUser)
        {
            if (!ModelState.IsValid) return View(aspNetUser);
            _db.Entry(aspNetUser).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) BadRequest();
            var aspNetUser = await FindUser(id);
            if (aspNetUser == null) NotFound();
            return View(aspNetUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await FindUser(id);
            if (user == null) return RedirectToAction("Index");

            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db?.Dispose();
            _userManager?.Dispose();
        }
    }
}
