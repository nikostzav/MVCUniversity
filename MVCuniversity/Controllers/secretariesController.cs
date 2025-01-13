using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCuniversity.Models;

namespace MVCuniversity.Controllers
{
    public class secretariesController : Controller
    {
        private universityEntities db = new universityEntities();

        // GET: secretaries
        public ActionResult Index()
        {
            var secretaries = db.secretaries.Include(s => s.user);
            return View(secretaries.ToList());
        }

        // GET: secretaries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            secretary secretary = db.secretaries.Find(id);
            if (secretary == null)
            {
                return HttpNotFound();
            }
            return View(secretary);
        }

        // GET: secretaries/Create
        public ActionResult Create()
        {
            ViewBag.USERS_USERNAME = new SelectList(db.users, "username", "password");
            return View();
        }

        // POST: secretaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "phonenumber,name,surename,department,USERS_USERNAME")] secretary secretary)
        {
            if (ModelState.IsValid)
            {
                user user= new user();
                user.role = "Secretary";
                user.username = secretary.USERS_USERNAME;
                user.password = secretary.phonenumber.ToString();
                db.users.Add(user);
                db.secretaries.Add(secretary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.USERS_USERNAME = new SelectList(db.users, "username", "password", secretary.USERS_USERNAME);
            return View(secretary);
        }

        // GET: secretaries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            secretary secretary = db.secretaries.Find(id);
            if (secretary == null)
            {
                return HttpNotFound();
            }
            ViewBag.USERS_USERNAME = new SelectList(db.users, "username", "password", secretary.USERS_USERNAME);
            return View(secretary);
        }

        // POST: secretaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "phonenumber,name,surename,department,USERS_USERNAME")] secretary secretary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(secretary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.USERS_USERNAME = new SelectList(db.users, "username", "password", secretary.USERS_USERNAME);
            return View(secretary);
        }

        // GET: secretaries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            secretary secretary = db.secretaries.Find(id);
            if (secretary == null)
            {
                return HttpNotFound();
            }
            return View(secretary);
        }

        // POST: secretaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            secretary secretary = db.secretaries.Find(id);
            db.secretaries.Remove(secretary);
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
