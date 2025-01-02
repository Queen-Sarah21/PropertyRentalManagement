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
    public class LandlordsController : Controller
    {
        private Property_Rental_DBEntities db = new Property_Rental_DBEntities();

        // GET: Landlords
        public ActionResult Index()
        {
            var landlords = db.Landlords.Include(l => l.User);
            return View(landlords.ToList());
        }


        // Display all messages for the landlord
        public ActionResult ReceiveMessages()
        {
            int landlordId = int.Parse(User.Identity.Name); // Get the logged-in landlord's ID
            var messages = db.Messages
                             .Where(m => m.ReceiverId == landlordId)
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

            // Change message status to 'Read' (StatusId = 8)
            if (message.StatusId == 8) // 8 = Unread
            {
                message.StatusId = 7; // 7 = Read
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
            }

            return View(message);
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
