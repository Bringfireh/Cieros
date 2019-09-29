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
    public class StaffsController : Controller
    {
        private MyModel db = new MyModel();

        // GET: Staffs
        public ActionResult Index()
        {
            return View(db.Staffs.ToList());
        }

        // GET: Staffs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Staffs/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Activate(string staffId)
        {
            Staff staff = db.Staffs.Find(staffId);

            if (staff == null)
            {
                return HttpNotFound();
            }
            staff.ActiveStatus = true;
            db.Entry(staff).State = EntityState.Modified;
            db.SaveChanges();
            string urladd = "~/Staffs/Details?id=" + staffId;
            return Redirect(urladd);
        }
        public ActionResult DeActivate(string staffId)
        {
            Staff staff = db.Staffs.Find(staffId);

            if (staff == null)
            {
                return HttpNotFound();
            }
            staff.ActiveStatus = false;
            db.Entry(staff).State = EntityState.Modified;
            db.SaveChanges();
            string urladd = "~/Staffs/Details?id=" + staffId;
            return Redirect(urladd);
        }
        // POST: Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffID,Surname,Othernames,Sex,MaritalStatus,DateOfBirth,MaidenName,Country,StatOfOrigin,LocalGovt,Religion,Discipline,Qualification,DateOfAppointment,Position,BankName,AccountNumber,AccountType,PhoneNumber,EmailAddress,NextOfKinName,NextOfKinAddress,NextOfKinPhone,NextOfKinEmail,NextOfKinRelationship,BasicSalary,MonthlySalary,ActiveStatus")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                staff.ActiveStatus = true;
                db.Staffs.Add(staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staff);
        }

        // GET: Staffs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffID,Surname,Othernames,Sex,MaritalStatus,DateOfBirth,MaidenName,Country,StatOfOrigin,LocalGovt,Religion,Discipline,Qualification,DateOfAppointment,Position,BankName,AccountNumber,AccountType,PhoneNumber,EmailAddress,NextOfKinName,NextOfKinAddress,NextOfKinPhone,NextOfKinEmail,NextOfKinRelationship,BasicSalary,MonthlySalary,ActiveStatus")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                staff.ActiveStatus = true;
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Staff staff = db.Staffs.Find(id);
            db.Staffs.Remove(staff);
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
