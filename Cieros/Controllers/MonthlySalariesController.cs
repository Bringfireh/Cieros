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
            var monthlySalaries = db.MonthlySalaries.Include(m => m.Staff);
            return View(monthlySalaries.ToList());
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
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "Surname", monthlySalary.StaffID);
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
