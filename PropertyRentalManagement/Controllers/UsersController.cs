using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PropertyRentalManagement.Models;
using System.Web.Security;

namespace PropertyRentalManagement.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private Property_Rental_DBEntities db = new Property_Rental_DBEntities();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Landlord).Include(u => u.Manager).Include(u => u.Tenant);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Landlords, "LandlordId", "FirstName");
            ViewBag.UserId = new SelectList(db.Managers, "ManagerId", "FirstName");
            ViewBag.UserId = new SelectList(db.Tenants, "TenantId", "FirstName");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    // Check if UserId already exists in the database
                    if (db.Users.Any(u => u.UserId == user.UserId))
                    {
                        // Add an error to the ModelState if UserId already exists
                        ModelState.AddModelError("UserId", "The User ID must be unique. This User ID already exists.");
                    }
                    else
                    {
                        db.Users.Add(user);
                        db.SaveChanges();
                        // Redirect to the Managers Index page
                        return RedirectToAction("Index", "Managers");
                    }
                }
            }

            ViewBag.UserId = new SelectList(db.Landlords, "LandlordId", "FirstName", user.UserId);
            ViewBag.UserId = new SelectList(db.Managers, "ManagerId", "FirstName", user.UserId);
            ViewBag.UserId = new SelectList(db.Tenants, "TenantId", "FirstName", user.UserId);
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Landlords, "LandlordId", "FirstName", user.UserId);
            ViewBag.UserId = new SelectList(db.Managers, "ManagerId", "FirstName", user.UserId);
            ViewBag.UserId = new SelectList(db.Tenants, "TenantId", "FirstName", user.UserId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Landlords, "LandlordId", "FirstName", user.UserId);
            ViewBag.UserId = new SelectList(db.Managers, "ManagerId", "FirstName", user.UserId);
            ViewBag.UserId = new SelectList(db.Tenants, "TenantId", "FirstName", user.UserId);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
