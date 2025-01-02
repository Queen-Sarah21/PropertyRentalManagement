using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PropertyRentalManagement.Models;

namespace PropertyRentalManagement.Controllers
{
    [Authorize]
    public class LeasesController : Controller
    {
        private Property_Rental_DBEntities db = new Property_Rental_DBEntities();

        // GET: Leases
        public ActionResult Index()
        {
            var leases = db.Leases.Include(l => l.Apartment).Include(l => l.Status).Include(l => l.Tenant);
            return View(leases.ToList());
        }

        // GET: Leases/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leas leas = db.Leases.Find(id);
            if (leas == null)
            {
                return HttpNotFound();
            }
            return View(leas);
        }

        // GET: Leases/Create
        public ActionResult Create()
        {
            ViewBag.ApartmentNum = new SelectList(db.Apartments, "ApartmentNum", "BuildingCode");
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "Description");
            ViewBag.TenantId = new SelectList(db.Tenants, "TenantId", "FirstName");
            return View();
        }

        // POST: Leases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LeaseId,TenantId,ApartmentNum,StartDate,EndDate,MonthlyRent,StatusId")] Leas leas)
        {
            if (ModelState.IsValid)
            {
                db.Leases.Add(leas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApartmentNum = new SelectList(db.Apartments, "ApartmentNum", "BuildingCode", leas.ApartmentNum);
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "Description", leas.StatusId);
            ViewBag.TenantId = new SelectList(db.Tenants, "TenantId", "FirstName", leas.TenantId);
            return View(leas);
        }

        // GET: Leases/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leas leas = db.Leases.Find(id);
            if (leas == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApartmentNum = new SelectList(db.Apartments, "ApartmentNum", "BuildingCode", leas.ApartmentNum);
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "Description", leas.StatusId);
            ViewBag.TenantId = new SelectList(db.Tenants, "TenantId", "FirstName", leas.TenantId);
            return View(leas);
        }

        // POST: Leases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LeaseId,TenantId,ApartmentNum,StartDate,EndDate,MonthlyRent,StatusId")] Leas leas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApartmentNum = new SelectList(db.Apartments, "ApartmentNum", "BuildingCode", leas.ApartmentNum);
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "Description", leas.StatusId);
            ViewBag.TenantId = new SelectList(db.Tenants, "TenantId", "FirstName", leas.TenantId);
            return View(leas);
        }

        // GET: Leases/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leas leas = db.Leases.Find(id);
            if (leas == null)
            {
                return HttpNotFound();
            }
            return View(leas);
        }

        // POST: Leases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Leas leas = db.Leases.Find(id);
            db.Leases.Remove(leas);
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
