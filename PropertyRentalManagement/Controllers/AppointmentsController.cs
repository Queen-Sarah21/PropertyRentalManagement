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
    public class AppointmentsController : Controller
    {
        private Property_Rental_DBEntities db = new Property_Rental_DBEntities();


        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            ViewBag.ApartmentNum = new SelectList(db.Apartments, "ApartmentNum", "BuildingCode");
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "FirstName");
            ViewBag.ScheduleId = new SelectList(db.Schedules, "ScheduleId", "ScheduleId");
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "Description");
            // Get the logged-in tenant's ID from the session or User.Identity
            var tenantId = int.Parse(User.Identity.Name); // Assuming the tenant ID is stored in the user identity.
            ViewBag.TenantId = new SelectList(db.Tenants, "TenantId", "FirstName", tenantId); // Pre-select the current tenant

            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentId,TenantId,ManagerId,ApartmentNum,ScheduleId,StatusId")] Appointment appointment)
        {
            // Ensure the logged-in tenant is creating the appointment
            var tenantId = int.Parse(User.Identity.Name); // Assuming the tenant ID is stored in the user identity
            if (appointment.TenantId != tenantId)
            {
                ModelState.AddModelError("TenantId", "You can only create an appointment for yourself.");
            }

            // Validate if the apartment number corresponds to the selected manager
            var apartment = db.Apartments.SingleOrDefault(a => a.ApartmentNum == appointment.ApartmentNum);
            if (apartment == null || apartment.Building.ManagerId != appointment.ManagerId)
            {
                ModelState.AddModelError("ApartmentNum", "The selected apartment does not belong to the chosen manager.");
            }

            // Ensure that the TenantId and ManagerId are valid
            var tenantExists = db.Tenants.Any(t => t.TenantId == appointment.TenantId);
            if (!tenantExists)
            {
                ModelState.AddModelError("TenantId", "The selected tenant does not exist.");
            }

            var managerExists = db.Managers.Any(m => m.ManagerId == appointment.ManagerId);
            if (!managerExists)
            {
                ModelState.AddModelError("ManagerId", "The selected manager does not exist.");
            }

            // If no validation errors, save the appointment
            if (ModelState.IsValid)
            {
                // Set the StatusId to 4 (Pending) automatically
                appointment.StatusId = 4;

                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("ViewAppointments", "Tenants");
            }

            // Populate the ViewBag for the dropdowns
            ViewBag.ApartmentNum = new SelectList(db.Apartments, "ApartmentNum", "BuildingCode", appointment.ApartmentNum);
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "FirstName", appointment.ManagerId);
            ViewBag.ScheduleId = new SelectList(db.Schedules, "ScheduleId", "ScheduleId", appointment.ScheduleId);
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "Description", appointment.StatusId);
            ViewBag.TenantId = new SelectList(db.Tenants, "TenantId", "FirstName", appointment.TenantId);

            return View(appointment);
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
