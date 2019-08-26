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
    public class MessagesController : Controller
    {
        private MyModel db = new MyModel();

        // GET: Messages
        public ActionResult Index()
        {
            return View(db.Messages.OrderByDescending(m => m.DateSent).ToList());
        }

        // GET: Messages/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }
        public ActionResult SingleStaffMessage()
        {

            return View(db.Staffs.ToList());
        }

        public ActionResult SingleMsg(string id)
        {
            var phonenumbers = from s in db.Staffs
                               where s.StaffID == id
                               select new
                               {
                                   PhoneNumber = s.PhoneNumber ,
                                   Names = s.Surname + ", " + s.Othernames
                               };
            ViewBag.StaffPhone = phonenumbers.Select(s => s.PhoneNumber).FirstOrDefault();
            ViewBag.StaffNames = phonenumbers.Select(s => s.Names).FirstOrDefault();
            ViewBag.StaffID = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SingleMsg(Message message)
        {
            string StaffID = Request.Form["StaffID"];
            string RecepientName = Request.Form["RecepientName"];
            SmsService smsService = new SmsService();
            ViewBag.SendMessage = smsService.Send_SMS(message.MessageTitle + " -" + message.MessageBody, "Cieros", message.Recepient);
            if (ModelState.IsValid)
            {
                message.ID = Guid.NewGuid().ToString().Substring(0, 16);
                //message.Recepient = RecepientName + " - " + message.Recepient;
                message.MessageType = "SMS";
                message.DateSent = DateTime.Now;
                db.Messages.Add(message);
                db.SaveChanges();
            }
            var phonenumbers = from s in db.Staffs
                               where s.StaffID == StaffID
                               select new
                               {
                                   PhoneNumber = s.PhoneNumber,
                                   Names = s.Surname + ", " + s.Othernames
                               };
            ViewBag.StaffPhone = phonenumbers.Select(s => s.PhoneNumber).FirstOrDefault();
            ViewBag.StaffNames = phonenumbers.Select(s => s.Names).FirstOrDefault();
            ViewBag.StaffID = StaffID;
            return View();
        }
        // GET: Messages/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult SendAllStaff()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendAllStaff(Message message)
        {
            message.Recepient = "All Staff";
            SmsService smsService = new SmsService();

            var staffs = db.Staffs.ToList();
            foreach (var item in staffs)
            {
                var phonenumbers = from s in db.Staffs
                                   where s.StaffID == item.StaffID
                                   select new
                                   {
                                       PhoneNumber = s.PhoneNumber,
                                       Names = s.Surname + ", " + s.Othernames
                                   };
                string StaffPhone = phonenumbers.Select(s => s.PhoneNumber).FirstOrDefault();
                string StaffNames = phonenumbers.Select(s => s.Names).FirstOrDefault();
                ViewBag.SendMessage = smsService.Send_SMS(message.MessageTitle + " -" + message.MessageBody, "Cieros", StaffPhone);
            }

            if (ModelState.IsValid)
            {
                message.ID = Guid.NewGuid().ToString().Substring(0, 16);
                //message.Recepient = RecepientName + " - " + message.Recepient;
                message.MessageType = "SMS";
                message.DateSent = DateTime.Now;
                db.Messages.Add(message);
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult SendAllGuardians()
        {

            return View();
        }
        
        public ActionResult SingleGuardianMessage()
        {

            return View(db.Guardians.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendAllGuardians(Message message)
        {
            message.Recepient = "All Guardians";
            SmsService smsService = new SmsService();

            var guardians = db.Guardians.ToList();
            foreach(var item in guardians)
            {
                var phonenumbers = from s in db.Guardians
                                   where s.ID == item.ID
                                   select new
                                   {
                                       PhoneNumber = s.FatherPhoneNumber + ", " + s.MotherPhoneNumber,
                                       Names = s.FatherName + ", " + s.MotherName
                                   };
               string GuardianPhone = phonenumbers.Select(s => s.PhoneNumber).FirstOrDefault();
                string GuardianNames = phonenumbers.Select(s => s.Names).FirstOrDefault();
                ViewBag.SendMessage = smsService.Send_SMS(message.MessageTitle + " -" + message.MessageBody, "Cieros", GuardianPhone);
            }
            
            if (ModelState.IsValid)
            {
                message.ID = Guid.NewGuid().ToString().Substring(0, 16);
                //message.Recepient = RecepientName + " - " + message.Recepient;
                message.MessageType = "SMS";
                message.DateSent = DateTime.Now;
                db.Messages.Add(message);
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult SingleMessage(string ID)
        {
            var phonenumbers= from s in db.Guardians where s.ID == ID
                              select new{ PhoneNumber = s.FatherPhoneNumber + ", "+ s.MotherPhoneNumber,
                              Names = s.FatherName + ", " + s.MotherName
            };
            ViewBag.GuardianPhone = phonenumbers.Select(s => s.PhoneNumber).FirstOrDefault();
            ViewBag.GuardianNames = phonenumbers.Select(s => s.Names).FirstOrDefault();
            ViewBag.GuardianID = ID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SingleMessage(Message message)
        {
            string guardianID = Request.Form["GuardianID"];
            string RecepientName = Request.Form["RecepientName"];
            SmsService smsService = new SmsService();
            ViewBag.SendMessage = smsService.Send_SMS(message.MessageTitle + " -" + message.MessageBody, "Cieros", message.Recepient);
            if (ModelState.IsValid)
            {
                message.ID = Guid.NewGuid().ToString().Substring(0, 16);
                //message.Recepient = RecepientName + " - " + message.Recepient;
                message.MessageType = "SMS";
                message.DateSent = DateTime.Now;
                db.Messages.Add(message);
                db.SaveChanges();
            }
            
            var phonenumbers = from s in db.Guardians
                               where s.ID == guardianID
                               select new
                               {
                                   PhoneNumber = s.FatherPhoneNumber + ", " + s.MotherPhoneNumber,
                                   Names = s.FatherName + ", " + s.MotherName
                               };
            ViewBag.GuardianPhone = phonenumbers.Select(s => s.PhoneNumber).FirstOrDefault();
            ViewBag.GuardianNames = phonenumbers.Select(s => s.Names).FirstOrDefault();
            ViewBag.GuardianID = guardianID;
            return View();
        }
        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Recepient,MessageBody,DateSent,MessageType,MessageTitle")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(message);
        }

        // GET: Messages/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Recepient,MessageBody,DateSent,MessageType,MessageTitle")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(message);
        }

        // GET: Messages/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
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
