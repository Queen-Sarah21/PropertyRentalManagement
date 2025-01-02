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
    public class TenantsController : Controller
    {
        private Property_Rental_DBEntities db = new Property_Rental_DBEntities();

        // GET: Tenants
        public ActionResult Index()
        {
            var tenants = db.Tenants.Include(t => t.User);
            return View(tenants.ToList());
        }

        // GET: Tenants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tenant tenant = db.Tenants.Find(id);
            if (tenant == null)
            {
                return HttpNotFound();
            }
            return View(tenant);
        }

        // GET: Tenants/Create
        public ActionResult Create()
        {
            ViewBag.TenantId = new SelectList(db.Users, "UserId", "Password");
            return View();
        }

        // POST: Tenants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenantId,FirstName,LastName,Email,Phone")] Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                db.Tenants.Add(tenant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TenantId = new SelectList(db.Users, "UserId", "Password", tenant.TenantId);
            return View(tenant);
        }

        // GET: Tenants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tenant tenant = db.Tenants.Find(id);
            if (tenant == null)
            {
                return HttpNotFound();
            }
            ViewBag.TenantId = new SelectList(db.Users, "UserId", "Password", tenant.TenantId);
            return View(tenant);
        }

        // POST: Tenants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TenantId,FirstName,LastName,Email,Phone")] Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tenant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TenantId = new SelectList(db.Users, "UserId", "Password", tenant.TenantId);
            return View(tenant);
        }

        // GET: Tenants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tenant tenant = db.Tenants.Find(id);
            if (tenant == null)
            {
                return HttpNotFound();
            }
            return View(tenant);
        }

        // POST: Tenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tenant tenant = db.Tenants.Find(id);
            db.Tenants.Remove(tenant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Search(string searchString)
        {
            var tenants = db.Tenants.Include(t => t.User);

            if (!string.IsNullOrEmpty(searchString))
            {
                // Filter by ManagerId, FirstName, or LastName
                tenants = tenants.Where(t => t.TenantId.ToString().Contains(searchString) ||
                                                t.FirstName.Contains(searchString) ||
                                                t.LastName.Contains(searchString));
            }

            return View(tenants.ToList());
        }

        // GET: Tenants/MyDetails
        public ActionResult MyDetails()
        {
            // Get the currently logged-in user's ID
            int loggedInTenantId = int.Parse(User.Identity.Name);

            Tenant tenant = db.Tenants.FirstOrDefault(t => t.TenantId == loggedInTenantId);

            if (tenant == null)
            {
                return HttpNotFound("Tenant details not found.");
            }

            return View(tenant);
        }

        // GET: Apartments/Details/5
        public ActionResult ApartmentDetails(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Apartment apartment = db.Apartments.Find(id);
            if (apartment == null)
            {
                return HttpNotFound();
            }

            // Generate image URLs based on ApartmentNum
            var imageUrl = $"/Content/images/apartment_{apartment.ApartmentNum}.jpeg";

            var viewModel = new ApartmentDetailsViewModel
            {
                ApartmentNum = apartment.ApartmentNum,
                BuildingName = apartment.Building.BuildingName,
                Rooms = apartment.Rooms,
                Bathrooms = apartment.Bathrooms,
                Description = apartment.Description,
                Rent = apartment.Rent,
                StatusDescription = apartment.Status.Description,
                Address = apartment.Building.Address,
                City = apartment.Building.City,
                Province = apartment.Building.Province,
                ZipCode = apartment.Building.ZipCode,
                ImageUrls = new List<string> { imageUrl }
            };

            return View(viewModel);
        }

        // Action for sending the message to the manager
        public ActionResult SendMessageToManager()
        {
            var tenantId = int.Parse(User.Identity.Name);

            var message = new Message
            {
                SenderId = tenantId,       
                MessageSubject = "",
                MessageBody = "",         
                MessageDate = DateTime.Now, 
                StatusId = 8       // Unread status
            };

            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMessageToManager(Message message)
        {
            // Check if the entered LandlordId exists in the database
            var manager = db.Managers.FirstOrDefault(m => m.ManagerId == message.ReceiverId);

            if (manager == null)
            {
                // If the LandlordId doesn't exist, display an error message
                ModelState.AddModelError("ReceiverId", "The specified Manager ID does not exist.");
                return View(message); // Return the view with the validation error
            }


            // Check if the MessageBody is not empty
            if (string.IsNullOrWhiteSpace(message.MessageSubject))
            {
                ModelState.AddModelError("MessageSubject", "The message subject cannot be empty.");
                return View(message);
            }

            // Check if the MessageBody is not empty
            if (string.IsNullOrWhiteSpace(message.MessageBody))
            {
                ModelState.AddModelError("MessageBody", "The message body cannot be empty.");
                return View(message);
            }

            message.MessageDate = DateTime.Now; 
            message.StatusId = 8;               // Set to unread
            db.Messages.Add(message);
            db.SaveChanges();

            return RedirectToAction("MyDetails", "Tenants"); 
        }

        // Display all messages for the TenantId
        public ActionResult ReceiveMessages()
        {
            int tenantId = int.Parse(User.Identity.Name);
            var messages = db.Messages
                             .Where(m => m.ReceiverId == tenantId)
                             .OrderByDescending(m => m.MessageDate)
                             .ToList();
            return View(messages);
        }

        //view a tenant's specific message and manager's response
        public ActionResult ViewMessage(int id)
        {
            // Find the message by ID
            var message = db.Messages.Find(id);

            if (message == null)
            {
                return HttpNotFound("Message not found.");
            }

            // Ensure that the logged-in user is the tenant who sent the message
            if (message.ReceiverId != int.Parse(User.Identity.Name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Unauthorized access.");
            }

            if (message.StatusId == 8) // 8 = Unread
            {
                message.StatusId = 7; // 7 = Read
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
            }

            return View(message);
        }

        // View all property manager schedules
        public ActionResult ViewSchedules()
        {
            var schedules = db.Schedules.Include(s => s.Manager).ToList();
            return View(schedules);
        }

        // Book an appointment
        public ActionResult BookAppointment(int scheduleId)
        {
            var schedule = db.Schedules.Include(s => s.Manager).FirstOrDefault(s => s.ScheduleId == scheduleId);

            if (schedule == null)
            {
                return HttpNotFound();
            }

            // Get apartments managed by the selected manager
            var apartments = db.Apartments
                .Join(db.Buildings, a => a.BuildingCode, b => b.BuildingCode, (a, b) => new { Apartment = a, b.ManagerId })
                .Where(x => x.ManagerId == schedule.ManagerId && x.Apartment.StatusId == db.Statuses.FirstOrDefault(s => s.Description == "Available").StatusId)
                .Select(x => x.Apartment)
                .ToList();

            if (!apartments.Any())
            {
                ViewBag.ErrorMessage = "No available apartments are managed by this manager.";
            }

            // Ensure the TenantId is available in the session
            var tenantId = Session["TenantId"] as int?;
            if (tenantId == null)
            {
                return RedirectToAction("Login", "Accounts"); 
            }

            var appointment = new Appointment
            {
                ScheduleId = schedule.ScheduleId,
                ManagerId = schedule.ManagerId,
                TenantId = tenantId.Value 
            };

            ViewBag.Apartments = new SelectList(apartments, "ApartmentNum", "ApartmentNum");
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookAppointment(Appointment appointment)
        {
            // Check if the selected apartment is available
            if (!db.Apartments.Any(a => a.ApartmentNum == appointment.ApartmentNum &&
                a.StatusId == db.Statuses.FirstOrDefault(s => s.Description == "Available").StatusId))
            {
                ModelState.AddModelError("ApartmentNum", "The selected apartment is not available.");
            }

            // Ensure the tenant is logged in and TenantId is set in the session
            var tenantId = Session["TenantId"] as int?;
            if (tenantId == null)
            {
                ModelState.AddModelError("TenantId", "Tenant not logged in.");
            }

            if (ModelState.IsValid)
            {
                appointment.StatusId = db.Statuses.FirstOrDefault(s => s.Description == "Pending").StatusId;

                db.Appointments.Add(appointment);

                var apartment = db.Apartments.FirstOrDefault(a => a.ApartmentNum == appointment.ApartmentNum);
                if (apartment != null)
                {
                    apartment.StatusId = db.Statuses.FirstOrDefault(s => s.Description == "Pending").StatusId;
                }

                db.SaveChanges();
                return RedirectToAction("ViewAppointments");
            }

            var schedule = db.Schedules.FirstOrDefault(s => s.ScheduleId == appointment.ScheduleId);
            if (schedule != null)
            {
                var apartments = db.Apartments
                    .Join(db.Buildings, a => a.BuildingCode, b => b.BuildingCode, (a, b) => new { Apartment = a, b.ManagerId })
                    .Where(x => x.ManagerId == schedule.ManagerId && x.Apartment.StatusId == db.Statuses.FirstOrDefault(s => s.Description == "Available").StatusId)
                    .Select(x => x.Apartment)
                    .ToList();

                ViewBag.Apartments = new SelectList(apartments, "ApartmentNum", "Description");
            }

            return View(appointment);
        }

        // View tenant's appointments
        public ActionResult ViewAppointments()
        {
            // Get the currently logged-in tenant's ID
            int loggedInTenantId = int.Parse(User.Identity.Name);

            var appointments = db.Appointments
                .Where(a => a.TenantId == loggedInTenantId)
                .OrderByDescending(a => a.Schedule.ScheduleDate)
                .Include(a => a.Apartment) 
                .Include(a => a.Schedule)  
                .ToList();

            // If no appointments are found, display a message
            if (appointments.Count == 0)
            {
                ViewBag.Message = "No appointments yet."; 
            }

            return View(appointments);
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
