using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PropertyRentalManagement.Models;
using System.Web.Security;

namespace PropertyRentalManagement.Controllers
{
    [Authorize] 
    public class ManagersController : Controller
    {
        private Property_Rental_DBEntities db = new Property_Rental_DBEntities();

        // GET: Managers
        public ActionResult Index()
        {
      
            var managers = db.Managers.Include(m => m.User);
            return View(managers.ToList());
        }

        // GET: Managers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // GET: Managers/Create
        public ActionResult Create()
        {
            ViewBag.ManagerId = new SelectList(db.Users, "UserId", "Password");
            return View();
        }

        // POST: Managers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ManagerId,FirstName,LastName,Email,Phone")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                // Check if ManagerId is 5 digits
                if (!System.Text.RegularExpressions.Regex.IsMatch(manager.ManagerId.ToString(), @"^\d{5}$"))
                {
                    ModelState.AddModelError("ManagerId", "The Manager ID must be exactly 5 digits.");
                }

                // Check if ManagerId already exists
                else if (db.Managers.Any(m => m.ManagerId == manager.ManagerId))
                {
                    ModelState.AddModelError("ManagerId", "The Manager ID must be unique.");
                }

                // Check if Email already exists
                else if (db.Managers.Any(m => m.Email == manager.Email))
                {
                    ModelState.AddModelError("Email", "The Email must be unique.");
                }

                // Check if Phone already exists
                else if (db.Managers.Any(m => m.Phone == manager.Phone))
                {
                    ModelState.AddModelError("Phone", "The Phone number must be unique.");
                }
                // Check if the Manager exists based on a certain condition (e.g., ManagerId or Email)
                else if (!db.Users.Any(u => u.UserId == manager.ManagerId)) // Assuming UserId refers to the Manager
                {
                    ModelState.AddModelError("ManagerId", "This user does not exist. Please ensure the user id is an existing id.");
                }
                else

                {
                    db.Managers.Add(manager);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.ManagerId = new SelectList(db.Users, "UserId", "Password", manager.ManagerId);
            return View(manager);
        }

        // GET: Managers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManagerId = new SelectList(db.Users, "UserId", "Password", manager.ManagerId);
            return View(manager);
        }

        // POST: Managers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ManagerId,FirstName,LastName,Email,Phone")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manager).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManagerId = new SelectList(db.Users, "UserId", "Password", manager.ManagerId);
            return View(manager);
        }

        // GET: Managers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manager manager = db.Managers.Find(id);
            db.Managers.Remove(manager);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Search(string searchString)
        {
            var managers = db.Managers.Include(m => m.User);

            if (!string.IsNullOrEmpty(searchString))
            {
                // Filter by ManagerId, FirstName, or LastName
                managers = managers.Where(m => m.ManagerId.ToString().Contains(searchString) ||
                                                m.FirstName.Contains(searchString) ||
                                                m.LastName.Contains(searchString));
            }

            return View(managers.ToList());
        }

        // GET: Managers/MyDetails
        public ActionResult MyDetails()
        {
            // Get the currently logged-in user's ID
            int loggedInManagerId = int.Parse(User.Identity.Name);

            // Find the manager with the corresponding ID
            Manager manager = db.Managers.FirstOrDefault(m => m.ManagerId == loggedInManagerId);

            if (manager == null)
            {
                return HttpNotFound("Manager details not found.");
            }

            return View(manager);
        }

        // GET: Managers/Buildings
        public ActionResult Buildings()
        {
            // Get the currently logged-in manager's ID
            int loggedInManagerId = int.Parse(User.Identity.Name);

            // Retrieve buildings where the logged-in manager is the owner
            var buildings = db.Buildings.Where(b => b.ManagerId == loggedInManagerId).ToList();

            return View(buildings);
        }

        public ActionResult Apartments()
        {
            int managerId = int.Parse(User.Identity.Name); 

            // Retrieve buildings owned by this manager
            var buildings = db.Buildings.Where(b => b.ManagerId == managerId).Select(b => b.BuildingCode).ToList(); 

            if (buildings.Count == 0)
            {
                return View("NoBuildings"); 
            }

            // Get all apartments belonging to the buildings owned by this manager
            var apartments = db.Apartments.Where(a => buildings.Contains(a.BuildingCode)).ToList(); 

            return View(apartments);
        }

        public ActionResult ViewApartmentsByStatus(int? statusId)
        {
            // Get the currently logged-in manager's ID
            int managerId = int.Parse(User.Identity.Name); 

            // Get all statuses related to apartments
            ViewBag.Statuses = db.Statuses.Where(s => s.Description == "Available" ||
                s.Description == "Maintenance" ||
                s.Description == "Occupied").ToList();

            // If no status is selected, show all apartments
            var apartments = db.Apartments.Include(a => a.Building).Include(a => a.Status).Where(a => a.Building.ManagerId == managerId).AsQueryable();

            if (statusId.HasValue)
            {
                apartments = apartments.Where(a => a.StatusId == statusId.Value);
            }

            return View(apartments.ToList());
        }

        //Report any events to the property owner when necessary
        public ActionResult SendMessageToLandlord()
        {
            var managerId = int.Parse(User.Identity.Name);

            // Initialize a new message object to pass to the view
            var message = new Message
            {
                SenderId = managerId,       
                MessageSubject = "Property Event Report", // Default subject
                MessageBody = "",            
                MessageDate = DateTime.Now,  
                StatusId = 8                // Unread status
            };

            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMessageToLandlord(Message message)
        {
            // Check if the entered LandlordId exists in the database
            var landlord = db.Landlords.FirstOrDefault(l => l.LandlordId == message.ReceiverId);

            if (landlord == null)
            {
                // If the LandlordId doesn't exist, display an error message
                ModelState.AddModelError("ReceiverId", "The specified Landlord ID does not exist.");
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

            return RedirectToAction("MyDetails", "Managers"); 
        }

        // Display all messages for the ManagerId
        public ActionResult ReceiveMessages()
        {
            int managerId = int.Parse(User.Identity.Name); 
            var messages = db.Messages
                             .Where(m => m.ReceiverId == managerId)
                             .OrderByDescending(m => m.MessageDate)
                             .ToList();
            return View(messages);
        }

        // View a specific message and mark it as read
        public ActionResult ViewMessage(int id)
        {
            var message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound("Message not found.");
            }

            // Check if the logged-in user is the receiver of this message
            if (message.ReceiverId != int.Parse(User.Identity.Name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Unauthorized access.");
            }

            // Change message status to 'Read' (StatusId = 9)
            if (message.StatusId == 8) // 8 = Unread
            {
                message.StatusId = 7; // 9 = Read
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
            }

            return View(message);
        }

        // Action for sending the message to the landlord
        public ActionResult SendMessageToTenant()
        {
            // Assuming you can get the manager's ID from the logged-in user
            var managerId = int.Parse(User.Identity.Name);

            var message = new Message
            {
                SenderId = managerId,       
                MessageSubject = "", 
                MessageBody = "",        
                MessageDate = DateTime.Now,  
                StatusId = 8             
            };

            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMessageToTenant(Message message)
        {
            // Check if the entered LandlordId exists in the database
            var tenant = db.Tenants.FirstOrDefault(t => t.TenantId == message.ReceiverId);

            if (tenant == null)
            {
                // If the LandlordId doesn't exist, display an error message
                ModelState.AddModelError("ReceiverId", "The specified Tenant ID does not exist.");
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

            return RedirectToAction("ReceiveMessages", "Managers"); 
        }


        // 1. View Appointments - List all appointments
        public ActionResult ViewAppointments()
        {
            // Get the logged-in manager's ID from the session or User.Identity
            int managerId = int.Parse(User.Identity.Name);
            var manager = db.Managers.FirstOrDefault(m => m.ManagerId == managerId);

            if (manager == null)
            {
                return HttpNotFound("Manager not found");
            }

            var appointments = db.Appointments
                .Where(a => a.ManagerId == manager.ManagerId)
                .Include(a => a.Tenant)
                .Include(a => a.Apartment)
                .Include(a => a.Status)
                .ToList();

            // If no appointments are found, display a message
            if (appointments.Count == 0)
            {
                ViewBag.Message = "No appointments yet.";
            }
            return View(appointments);
        }

        // Appointment Details - View and Update appointment status (Confirmed or Canceled)
        public ActionResult AppointmentDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }

            int managerId = int.Parse(User.Identity.Name);

            if (appointment.ManagerId != managerId)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
            }

            // Populate ViewBag with statuses
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "Description", appointment.StatusId);

            return View(appointment);
        }

        //method for updating the appointment status
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AppointmentDetails(int id, string status)
        {
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }

            // Get the logged-in manager's ID from User.Identity
            int managerId = int.Parse(User.Identity.Name);

            // Check if the appointment belongs to the logged-in manager
            if (appointment.ManagerId != managerId)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
            }

            // Update status based on action (Confirm or Cancel)
            if (status == "Confirm")
            {
                appointment.StatusId = db.Statuses.First(s => s.Description == "Confirmed").StatusId;
            }
            else if (status == "Cancel")
            {
                appointment.StatusId = db.Statuses.First(s => s.Description == "Canceled").StatusId;
            }

            db.SaveChanges();
            return RedirectToAction("ViewAppointments");
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
