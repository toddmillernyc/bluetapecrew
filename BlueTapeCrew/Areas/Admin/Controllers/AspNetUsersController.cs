using BlueTapeCrew.Areas.Admin.Models;
using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlueTapeCrew.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AspNetUsersController : Controller
    {
        private readonly BtcEntities _db = new BtcEntities();
        private readonly ApplicationUserManager _userManager;

        public AspNetUsersController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUserManager UserManager => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        public ActionResult Index()
        {
            return View(_db.AspNetUsers);
        }

        public ActionResult Details(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var aspNetUser = _db.AspNetUsers.Find(id);
            if (aspNetUser == null) return HttpNotFound();
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
                var result = await UserManager.CreateAsync(user, model.Password);
                if (model.IsAdmin)
                {
                    await UserManager.AddToRoleAsync(user.Id, "Admin");
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", ex.Message);
            }
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = await _db.AspNetUsers.FindAsync(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Address,City,State,PostalCode")] AspNetUser aspNetUser)
        {
            if (!ModelState.IsValid) return View(aspNetUser);
            _db.Entry(aspNetUser).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var aspNetUser = _db.AspNetUsers.Find(id);
            if (aspNetUser == null) return HttpNotFound();
            return View(aspNetUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var user = _db.AspNetUsers.Find(id);
            if (user == null) return RedirectToAction("Index");

            _db.AspNetUsers.Remove(user);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
