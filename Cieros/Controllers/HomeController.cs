using Cieros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cieros.Controllers
{
    public class HomeController : Controller
    {
        private MyModel db = new MyModel();
        public ActionResult Index()
        {
            
            return RedirectToAction("Login", "Account");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult DashBoard()
        {
            if (User.Identity.IsAuthenticated)
            {
                Institution institution = db.Institutions.SingleOrDefault();
                Session["institutionName"] = institution.Name;
                Session["totalStudents"] = db.Students.Count();
                Session["totalStaffs"] = db.Staffs.Count();
                Session["totalMessages"] = db.Messages.Count();
                Session["totalGuardians"] = db.Guardians.Count();
            }
                return View();
        }
    }
}