using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;

namespace BlueTapeCrew.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AspNetUsersController : Controller
    {
        private readonly BtcEntities _db = new BtcEntities();

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
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Address,City,State,PostalCode")] AspNetUser aspNetUser)
        {
            if (!ModelState.IsValid) return View(aspNetUser);

            _db.AspNetUsers.Add(aspNetUser);
            _db.SaveChanges();
            return RedirectToAction("Index");
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
