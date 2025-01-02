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
    public class MessagesController : Controller
    {
        private Property_Rental_DBEntities db = new Property_Rental_DBEntities();

        //// GET: Messages
        //public ActionResult Index()
        //{
        //    var messages = db.Messages.Include(m => m.User).Include(m => m.User1).Include(m => m.Status);
        //    return View(messages.ToList());
        //}

        //// GET: Messages/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Message message = db.Messages.Find(id);
        //    if (message == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(message);
        //}

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
