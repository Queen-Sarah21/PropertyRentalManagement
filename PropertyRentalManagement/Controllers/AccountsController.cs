using PropertyRentalManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PropertyRentalManagement.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View(new SignupViewModel());
        }

        [HttpPost]
        public ActionResult Signup(SignupViewModel model)
        {
            using (Property_Rental_DBEntities context = new Property_Rental_DBEntities())
            {
                // Ensure that model state is valid before proceeding
                if (!ModelState.IsValid)
                {
                    return View(model); 
                }

                if (context.Users.Any(u => u.UserId == model.User.UserId))
                {
                    ModelState.AddModelError("User.UserId", "UserId already exists.");
                    return View(model); 
                }

                if (context.Tenants.Any(t => t.Email == model.Tenant.Email))
                {
                    ModelState.AddModelError("Tenant.Email", "Email is already taken.");
                    return View(model); 
                }

                // Check if the TenantId already exists
                if (context.Tenants.Any(t => t.TenantId == model.Tenant.TenantId))
                {
                    ModelState.AddModelError("Tenant.TenantId", "Tenant ID already exists.");
                    return View(model); 
                }

                // Check if the Phone already exists
                if (context.Tenants.Any(t => t.Phone == model.Tenant.Phone))
                {
                    ModelState.AddModelError("Tenant.Phone", "Phone number is already registered.");
                    return View(model); 
                }

                context.Users.Add(model.User);
                context.SaveChanges();

                
                model.Tenant.User = model.User;  // Link the tenant with the user

                // Create the Tenant record
                context.Tenants.Add(model.Tenant);
                context.SaveChanges();

                FormsAuthentication.SetAuthCookie(model.User.UserId.ToString(), false);

                // Redirect based on user type (assuming Tenant)
                HttpCookie userTypeCookie = new HttpCookie("UserType", "Tenant")
                {
                    Expires = DateTime.Now.AddMinutes(30) // Set to expire in 30 minutes
                };
                Response.Cookies.Add(userTypeCookie);
                return RedirectToAction("MyDetails", "Tenants");
            }
        }



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model)
        {
            using (Property_Rental_DBEntities context = new Property_Rental_DBEntities())
            {
                bool isValidUser = context.Users.Any(user => user.UserId == model.UserId && user.Password == model.Password);
                if (isValidUser)
                {
                    FormsAuthentication.SetAuthCookie(model.UserId.ToString(), false);
                    HttpCookie userTypeCookie = new HttpCookie("UserType")
                    {
                        Expires = DateTime.Now.AddMinutes(30) // Set to expire in 30 minutes
                    };

                    // Check if the user is a Tenant
                    var tenant = context.Tenants.FirstOrDefault(t => t.TenantId == model.UserId);
                    if (tenant != null)
                    {
                        // Set the TenantId in the session
                        Session["TenantId"] = tenant.TenantId;
                        // Set the user type cookie
                        userTypeCookie = new HttpCookie("UserType", "Tenant");
                        Response.Cookies.Add(userTypeCookie);
                        return RedirectToAction("MyDetails", "Tenants");
                    }

                    // Check if the user is a Landlord
                    if (context.Landlords.Any(l => l.LandlordId == model.UserId))
                    {
                        userTypeCookie = new HttpCookie("UserType", "Landlord");
                        Response.Cookies.Add(userTypeCookie);
                        return RedirectToAction("Index", "Landlords");
                    }

                    // Check if the user is a Manager
                    if (context.Managers.Any(m => m.ManagerId == model.UserId))
                    {
                        userTypeCookie = new HttpCookie("UserType", "Manager");
                        Response.Cookies.Add(userTypeCookie);
                        return RedirectToAction("MyDetails", "Managers");
                    }

                    ModelState.AddModelError("", "User is not a landlord or manager or tenant!");
                    return View();
                }
                ModelState.AddModelError("", "Invalid username or password!");
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            // Remove UserType Cookie
            if (Request.Cookies["UserType"] != null)
            {
                var userTypeCookie = new HttpCookie("UserType")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(userTypeCookie);
            }

            return RedirectToAction("Login");
          
        }
    }
}