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
    public class professorsController : Controller
    {
        private universityEntities db = new universityEntities();

        // GET: professors
        public ActionResult Index()
        {
            var professors = db.professors.Include(p => p.user);
            return View(professors.ToList());
        }

        // GET: professors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            professor professor = db.professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // GET: professors/Create
        public ActionResult Create()
        {
            ViewBag.users_username = new SelectList(db.users, "username", "password");
            return View();
        }

        // POST: professors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AFM,name,surename,department,users_username")] professor professor)
        {
            if (ModelState.IsValid)
            {
                user user = new user();
                user.role = "Professor";
                user.username = professor.users_username;
                user.password =professor.surename + professor.AFM.ToString();
                db.users.Add(user);
                db.professors.Add(professor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            

            ViewBag.users_username = new SelectList(db.users, "username", "password", professor.users_username);
            return View(professor);
        }

        // GET: professors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            professor professor = db.professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            ViewBag.users_username = new SelectList(db.users, "username", "password", professor.users_username);
            return View(professor);
        }

        // POST: professors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AFM,name,surename,department,users_username")] professor professor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(professor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.users_username = new SelectList(db.users, "username", "password", professor.users_username);
            return View(professor);
        }

        // GET: professors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            professor professor = db.professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // POST: professors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            professor professor = db.professors.Find(id);
            db.professors.Remove(professor);
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
