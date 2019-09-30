using Cieros.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cieros.Controllers
{
    public class SessionPeriodController : Controller
    {
        private MyModel db = new MyModel();
        
        // GET: SessionPeriod
        public ActionResult Index()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult Index(string t="")
        {
            var students = db.Students.ToList();
            foreach(var item in students)
            {
                string ci = item.StdClass;
                var rc = db.RankClasses.Find(ci);
                if (rc != null)
                {
                    string nci = rc.NextClass;
                    item.StdClass = nci;
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            ViewBag.Msg = "End of Session Successfully runned.";
            return View();
        }
    }
}