using PropertyRentalManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PropertyRentalManagement.Controllers
{
   // [Authorize]
    public class HomeController : Controller
    {
        private Property_Rental_DBEntities db = new Property_Rental_DBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            // Fetch buildings from the database (this will display buildings for all users, including guests)
            var buildings = db.Buildings.ToList(); 
            ViewBag.Buildings = buildings; 
            ViewBag.Message = "List of available buildings."; 

            return View();
        }

    }
}