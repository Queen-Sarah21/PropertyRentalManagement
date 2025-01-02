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
    public class SchedulesController : Controller
    {
        private Property_Rental_DBEntities db = new Property_Rental_DBEntities();

        // GET: Schedules
        public ActionResult Index()
        {
            // Get the logged-in manager's ID
            var managerId = int.Parse(User.Identity.Name);

            // Retrieve only the schedules for the logged-in manager
            var schedules = db.Schedules.Where(s => s.ManagerId == managerId).Include(s => s.Manager);
            return View(schedules.ToList());
        }

        // GET: Schedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null || schedule.ManagerId != int.Parse(User.Identity.Name))
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // GET: Schedules/Create
        public ActionResult Create()
        {
            // Get the logged-in manager's ID
            var managerId = int.Parse(User.Identity.Name);

            // Pass only the logged-in manager to the dropdown
            ViewBag.ManagerId = new SelectList(db.Managers.Where(m => m.ManagerId == managerId), "ManagerId", "FirstName");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ScheduleId,ManagerId,ScheduleDate,StartTime,EndTime")] Schedule schedule)
        {
            // Ensure that the logged-in manager is creating their own schedule
            schedule.ManagerId = int.Parse(User.Identity.Name);

            if (ModelState.IsValid)
            {
                db.Schedules.Add(schedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Ensure the ManagerId dropdown contains only the logged-in manager
            ViewBag.ManagerId = new SelectList(db.Managers.Where(m => m.ManagerId == schedule.ManagerId), "ManagerId", "FirstName");
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null || schedule.ManagerId != int.Parse(User.Identity.Name))
            {
                return HttpNotFound();
            }

            // Only show the logged-in manager's name in the dropdown
            ViewBag.ManagerId = new SelectList(db.Managers.Where(m => m.ManagerId == schedule.ManagerId), "ManagerId", "FirstName", schedule.ManagerId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ScheduleId,ManagerId,ScheduleDate,StartTime,EndTime")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                schedule.ManagerId = int.Parse(User.Identity.Name);
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ManagerId = new SelectList(db.Managers.Where(m => m.ManagerId == schedule.ManagerId), "ManagerId", "FirstName", schedule.ManagerId);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null || schedule.ManagerId != int.Parse(User.Identity.Name))
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            db.Schedules.Remove(schedule);
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
