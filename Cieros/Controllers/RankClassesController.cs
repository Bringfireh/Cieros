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
    public class RankClassesController : Controller
    {
        private MyModel db = new MyModel();

        // GET: RankClasses
        public ActionResult Index()
        {
            return View(db.RankClasses.ToList());
        }

        // GET: RankClasses/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RankClass rankClass = db.RankClasses.Find(id);
            if (rankClass == null)
            {
                return HttpNotFound();
            }
            return View(rankClass);
        }

        // GET: RankClasses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RankClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StdClass,NextClass")] RankClass rankClass)
        {
            if (ModelState.IsValid)
            {
                db.RankClasses.Add(rankClass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rankClass);
        }

        // GET: RankClasses/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RankClass rankClass = db.RankClasses.Find(id);
            if (rankClass == null)
            {
                return HttpNotFound();
            }
            return View(rankClass);
        }

        // POST: RankClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StdClass,NextClass")] RankClass rankClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rankClass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rankClass);
        }

        // GET: RankClasses/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RankClass rankClass = db.RankClasses.Find(id);
            if (rankClass == null)
            {
                return HttpNotFound();
            }
            return View(rankClass);
        }

        // POST: RankClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            RankClass rankClass = db.RankClasses.Find(id);
            db.RankClasses.Remove(rankClass);
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
