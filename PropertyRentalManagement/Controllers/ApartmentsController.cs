using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PropertyRentalManagement.Models;

namespace PropertyRentalManagement.Controllers
{
    [Authorize]
    public class ApartmentsController : Controller
    {
        private Property_Rental_DBEntities db = new Property_Rental_DBEntities();

        // GET: Apartments
        public ActionResult Index()
        {
            // Filter apartments that are "Available"
            var apartments = db.Apartments
                                .Where(a => a.Status.Description == "Available")
                                .Include(a => a.Building)
                                .Include(a => a.Status);
            return View(apartments.ToList());
        }

        // GET: Apartments/Details/5
        public ActionResult Details(int? id)
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
            return View(apartment);
        }

        // GET: Apartments/Create
        public ActionResult Create()
        {
            // Get the currently signed-in manager's ID
            int managerId = int.Parse(User.Identity.Name);

            // Filter buildings belonging to this manager
            var buildingsForManager = db.Buildings.Where(b => b.ManagerId == managerId).ToList();

            // Check if the manager has buildings
            if (!buildingsForManager.Any())
            {
                return HttpNotFound("No buildings found for the logged-in manager.");
            }

            // Populate ViewBag with the filtered buildings and statuses
            ViewBag.BuildingCode = new SelectList(buildingsForManager, "BuildingCode", "BuildingName");
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "Description");

            return View();
            //ViewBag.BuildingCode = new SelectList(db.Buildings, "BuildingCode", "BuildingName");
            //ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "Description");
            //return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApartmentNum,BuildingCode,Rooms,Bathrooms,Description,Rent,StatusId")] Apartment apartment)
        {
            int managerId = int.Parse(User.Identity.Name);

            // Filter buildings belonging to this manager
            var buildingsForManager = db.Buildings.Where(b => b.ManagerId == managerId).ToList();

            if (ModelState.IsValid)
            {
                db.Apartments.Add(apartment);
                db.SaveChanges();
                // Redirect to the ManagersController's MyApartments action
                return RedirectToAction("Apartments", "Managers", new { managerId = managerId });
            }

            ViewBag.BuildingCode = new SelectList(db.Buildings, "BuildingCode", "BuildingName", apartment.BuildingCode);
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "Description", apartment.StatusId);
            return View(apartment);
        }

        // GET: Apartments/Edit/5
        public ActionResult Edit(int? id)
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
            // Get the logged-in manager's ID (this can vary based on your authentication method)
            int loggedInManagerId = int.Parse(User.Identity.Name); // Replace with appropriate method to get manager's ID

            // Filter buildings based on the manager's ID
            var buildingsForManager = db.Buildings.Where(b => b.ManagerId == loggedInManagerId).ToList();

            // Check if there are any buildings for this manager
            if (!buildingsForManager.Any())
            {
                return HttpNotFound("No buildings found for the logged-in manager.");
            }

            // Populate the ViewBag with the filtered list of buildings
            ViewBag.BuildingCode = new SelectList(buildingsForManager, "BuildingCode", "BuildingName", apartment.BuildingCode);
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "Description", apartment.StatusId);

            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApartmentNum,BuildingCode,Rooms,Bathrooms,Description,Rent,StatusId")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apartment).State = EntityState.Modified;
                db.SaveChanges();

                // Fetch the ManagerId through the BuildingCode
                var building = db.Buildings.FirstOrDefault(b => b.BuildingCode == apartment.BuildingCode);
                if (building == null)
                {
                    return HttpNotFound("Building not found.");
                }

                // Redirect to Apartments in ManagerController
                return RedirectToAction("Apartments", "Managers", new { managerId = building.ManagerId });
            }
            ViewBag.BuildingCode = new SelectList(db.Buildings, "BuildingCode", "BuildingName", apartment.BuildingCode);
            ViewBag.StatusId = new SelectList(db.Statuses, "StatusId", "Description", apartment.StatusId);
            return View(apartment);
        }

        // GET: Apartments/Delete/5
        public ActionResult Delete(int? id)
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
            return View(apartment);
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Apartment apartment = db.Apartments.Find(id);
            //db.Apartments.Remove(apartment);
            //db.SaveChanges();
            //return RedirectToAction("Index");

            Apartment apartment = db.Apartments.Find(id);
            if (apartment == null)
            {
                return HttpNotFound();
            }

            // Fetch the ManagerId through the BuildingCode
            var building = db.Buildings.FirstOrDefault(b => b.BuildingCode == apartment.BuildingCode);
            if (building == null)
            {
                return HttpNotFound("Building not found.");
            }

            db.Apartments.Remove(apartment);
            db.SaveChanges();

            // Redirect to Apartments in ManagerController
            return RedirectToAction("Apartments", "Managers", new { managerId = building.ManagerId });
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
