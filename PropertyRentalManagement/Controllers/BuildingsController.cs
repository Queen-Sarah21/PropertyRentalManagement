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
    public class BuildingsController : Controller
    {
        private Property_Rental_DBEntities db = new Property_Rental_DBEntities();

        // GET: Buildings
      
        public ActionResult Index()
        {
            var buildings = db.Buildings.Include(b => b.Landlord).Include(b => b.Manager);
            return View(buildings.ToList());
        }

        // GET: Buildings/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // GET: Buildings/Create
        public ActionResult Create()
        {
            //ViewBag.LandlordId = new SelectList(db.Landlords, "LandlordId", "FirstName");
            //ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "FirstName");
            return View();
        }


        //DOUBLE CHECK THE BUILDING ID, MANAGER, AND LANDLORD ID/ Duplicates
        // POST: Buildings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BuildingCode,LandlordId,ManagerId,BuildingName,Description,Address,City,Province,ZipCode")] Building building)
        {
            if (ModelState.IsValid)
            {
                // Check if the Building Code already exists
                if (db.Buildings.Any(b => b.BuildingCode == building.BuildingCode))
                {
                    // Add a model error if the Building Code already exists
                    ModelState.AddModelError("BuildingCode", "The Building Code already exists.");
                    return View(building);
                }

                // Check if the ManagerId exists
                var managerExists = db.Managers.Any(m => m.ManagerId == building.ManagerId);
                if (!managerExists)
                {
                    ModelState.AddModelError("ManagerId", "The Manager ID does not exist.");
                }

                // Check if the LandlordId exists
                var landlordExists = db.Landlords.Any(l => l.LandlordId == building.LandlordId);
                if (!landlordExists)
                {
                    ModelState.AddModelError("LandlordId", "The Landlord ID does not exist.");
                }

                // If either manager or landlord does not exist, return to the view
                if (!managerExists || !landlordExists)
                {
                    return View(building);
                }

                // If all validations pass, add the building
                db.Buildings.Add(building);
                db.SaveChanges();
                // Redirect to the manager's building list (make sure to replace "Manager" with the appropriate controller if it's different)
                return RedirectToAction("Buildings", "Managers", new { managerId = building.ManagerId });
                //return RedirectToAction("Index");
            }

            // If the model state is not valid, return to the view with existing data
            return View(building);
        }

        // GET: Buildings/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            ViewBag.LandlordId = new SelectList(db.Landlords, "LandlordId", "FirstName", building.LandlordId);
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "FirstName", building.ManagerId);
            return View(building);
        }

        // POST: Buildings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BuildingCode,LandlordId,ManagerId,BuildingName,Description,Address,City,Province,ZipCode")] Building building)
        {
            if (ModelState.IsValid)
            {
                db.Entry(building).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Buildings", "Managers", new { managerId = building.ManagerId });
            }
            ViewBag.LandlordId = new SelectList(db.Landlords, "LandlordId", "FirstName", building.LandlordId);
            ViewBag.ManagerId = new SelectList(db.Managers, "ManagerId", "FirstName", building.ManagerId);
            return View(building);
        }

        // GET: Buildings/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // POST: Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Building building = db.Buildings.Find(id);

            // Store the ManagerId before deletion for redirection
            var managerId = building.ManagerId;
            db.Buildings.Remove(building);
            db.SaveChanges();
            return RedirectToAction("Buildings", "Managers", new { managerId = managerId });
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
