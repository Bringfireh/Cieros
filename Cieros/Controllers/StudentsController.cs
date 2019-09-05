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
    public class StudentsController : Controller
    {
        private MyModel db = new MyModel();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Guardian);
            ViewBag.Count = students.Count();
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        public ActionResult AddGuardian(string id)
        {
            ViewBag.GuardianID = id;
            var guardian = db.Guardians.ToList();
            ViewBag.Count = guardian.Count();
            return View(guardian);
        }
        public ActionResult NewGuardian(string stdId, string guardianid)
        {
            var student = db.Students.Find(stdId);
            student.GuardianID = guardianid;
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
            string urladd = "~/Students/Details/" + stdId;

            return Redirect(urladd);
        }
        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.GuardianID = new SelectList(db.Guardians, "ID", "FatherName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Surname,Othernames,Sex,DateOfBirth,StdClass,Label,AdmissionNumber,GuardianID")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.ID = Guid.NewGuid().ToString().Substring(0, 16);

                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GuardianID = new SelectList(db.Guardians, "ID", "FatherName", student.GuardianID);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.GuardianID = new SelectList(db.Guardians, "ID", "FatherName", student.GuardianID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Surname,Othernames,Sex,DateOfBirth,StdClass,Label,AdmissionNumber,GuardianID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GuardianID = new SelectList(db.Guardians, "ID", "FatherName", student.GuardianID);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
