using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cieros.Models;

namespace Cieros.Controllers
{
    public class MonthlySalariesController : Controller
    {
        private MyModel db = new MyModel();

        // GET: MonthlySalaries
        public ActionResult Index()
        {
            string thismonth = getMonthName(DateTime.Now.AddMonths(-1).Month);
            int thisyear = DateTime.Now.Year;
            if (thismonth != "December1")
            {
                ViewBag.ThisMonth = thismonth;
                var monthlySalaries = db.MonthlySalaries.Where(m => m.Month == thismonth && m.Year == thisyear.ToString()).Include(m => m.Staff);
                ViewBag.Count = monthlySalaries.Count();
                ViewBag.Month = thismonth;
                ViewBag.Year = thisyear;
                return View(monthlySalaries.ToList());
            }
            else
            {
                thisyear = thisyear - 1;
                thismonth = "December";
                var monthlySalaries = db.MonthlySalaries.Where(m => m.Month == thismonth && m.Year == thisyear.ToString()).Include(m => m.Staff);
                ViewBag.Count = monthlySalaries.Count();
                ViewBag.Month = thismonth;
                ViewBag.Year = thisyear;
                return View(monthlySalaries.ToList());
            }
            
        }

        public string getMonthName(int month)
        {
            string name = "";
            if (month == 1)
            {
                name = "January";
            }
            else if(month == 2){
                name = "February";
            }else if (month == 3)
            {
                name = "March";
            }else if (month == 4)
            {
                name = "April";
            }else if (month == 5)
            {
                name = "May";
            }else if(month == 6)
            {
                name = "June";
            }else if (month == 7)
            {
                name = "July";
            }else if (month == 8)
            {
                name = "August";
            }else if (month == 9)
            {
                name = "September";
            }else if(month == 10)
            {
                name = "October";
            }else if (month == 11)
            {
                name = "November";
            }else if (month == 12)
            {
                name = "December";
            }
            else
            {
                name = "December1";
            }
            return name;
        }

        [HttpPost]
        public ActionResult Index(string Month, string Year)
        {
            int count = db.MonthlySalaries.Where(m => m.Month == Month && m.Year == Year).Count();
           
            if(count <= 0)
            {
                var staff = db.Staffs.ToList();
                foreach (var item in staff)
                {
                    MonthlySalary ms = new MonthlySalary();
                    ms.ID = item.StaffID + Month + Year;
                    ms.StaffID = item.StaffID;
                    ms.Month = Month;
                    ms.Year = Year;
                    ms.BasicAmount = item.MonthlySalary;
                    ms.Additions = (decimal)0.00;
                    ms.Deductions = (decimal)0.00;
                    ms.Balance = item.MonthlySalary;
                    db.MonthlySalaries.Add(ms);
                    db.SaveChanges();

                }
            }
            var monthlySalaries = db.MonthlySalaries.Where(m => m.Month==Month && m.Year==Year).Include(m => m.Staff);
            ViewBag.Count = monthlySalaries.Count();
            ViewBag.Month = Month;
            ViewBag.Year = Year;
            return View(monthlySalaries.ToList());
        }

        
        public ActionResult ResetPayroll(string Month, string Year)
        {
            var monthlySalaries = db.MonthlySalaries.Where(m => m.Month == Month && m.Year == Year).ToList();
            
                db.MonthlySalaries.RemoveRange(monthlySalaries);
                db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: MonthlySalaries/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlySalary monthlySalary = db.MonthlySalaries.Find(id);
            if (monthlySalary == null)
            {
                return HttpNotFound();
            }
            return View(monthlySalary);
        }
       
        // GET: MonthlySalaries/Create
        public ActionResult Create()
        {
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "Surname");
            
            return View();
        }

        // POST: MonthlySalaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,StaffID,Month,Year,BasicAmount,Additions,Deductions,Balance")] MonthlySalary monthlySalary)
        {
            if (ModelState.IsValid)
            {
                db.MonthlySalaries.Add(monthlySalary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "Surname", monthlySalary.StaffID);
            return View(monthlySalary);
        }

        // GET: MonthlySalaries/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlySalary monthlySalary = db.MonthlySalaries.Find(id);
           
            if (monthlySalary == null)
            {
                return HttpNotFound();
            }
            var staff = from s in db.Staffs
                        where s.StaffID == monthlySalary.StaffID
                        select new { StaffID = s.StaffID,
                            StaffName = s.Surname + " " + s.Othernames };
           
            ViewBag.StaffID = new SelectList(staff, "StaffID", "StaffName", monthlySalary.StaffID);
            return View(monthlySalary);
        }

        // POST: MonthlySalaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StaffID,Month,Year,BasicAmount,Additions,Deductions,Balance")] MonthlySalary monthlySalary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monthlySalary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "Surname", monthlySalary.StaffID);
            return View(monthlySalary);
        }

        // GET: MonthlySalaries/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthlySalary monthlySalary = db.MonthlySalaries.Find(id);
            if (monthlySalary == null)
            {
                return HttpNotFound();
            }
            return View(monthlySalary);
        }

        // POST: MonthlySalaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MonthlySalary monthlySalary = db.MonthlySalaries.Find(id);
            db.MonthlySalaries.Remove(monthlySalary);
            db.SaveChanges();
            return RedirectToAction("Index");
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
