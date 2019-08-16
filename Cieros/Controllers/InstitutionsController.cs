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
    public class InstitutionsController : Controller
    {
        private MyModel db = new MyModel();

        // GET: Institutions
        public ActionResult Index()
        {
            ViewBag.InstitutionName = db.Institutions.Select(ie => ie.Name).FirstOrDefault();
            return View(db.Institutions.ToList());
        }

        // GET: Institutions/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.InstitutionName = db.Institutions.Select(ie => ie.Name).FirstOrDefault();
            Institution institution = db.Institutions.Find(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        // GET: Institutions/Create
        public ActionResult Create()
        {
            ViewBag.InstitutionName = db.Institutions.Select(ie => ie.Name).FirstOrDefault();
            return View();
        }

        // POST: Institutions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,PostalAddress,ContactAddress,EmailAddress,PhoneNumber,FaxNumber,WebsiteAddress,Motto,DateCreated")] Institution institution)
        {
            if (ModelState.IsValid)
            {
                int ccount = db.Institutions.Count();
                ViewBag.InstitutionName = db.Institutions.Select(ie => ie.Name).FirstOrDefault();
                if (ccount <= 0)
                {
                    institution.ID = Guid.NewGuid().ToString().Substring(0, 16);
                    institution.DateCreated = DateTime.Now;
                    db.Institutions.Add(institution);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.InstitutionName = db.Institutions.Select(ie => ie.Name).FirstOrDefault();
                    ViewBag.Result = "Institution Already Created. Could not create Institution";
                }
            }

            return View(institution);
        }

        // GET: Institutions/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = db.Institutions.Find(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        // POST: Institutions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,PostalAddress,ContactAddress,EmailAddress,PhoneNumber,FaxNumber,WebsiteAddress,Motto,DateCreated")] Institution institution)
        {
            if (ModelState.IsValid)
            {
                //institution.ID = Guid.NewGuid().ToString().Substring(0, 16);
                ViewBag.InstitutionName = db.Institutions.Select(ie => ie.Name).FirstOrDefault();
                institution.DateCreated = DateTime.Now;
                db.Entry(institution).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(institution);
        }

        // GET: Institutions/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = db.Institutions.Find(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        // POST: Institutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Institution institution = db.Institutions.Find(id);
            db.Institutions.Remove(institution);
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
