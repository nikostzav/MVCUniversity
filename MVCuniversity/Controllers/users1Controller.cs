using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MVCuniversity.Models;

namespace MVCuniversity.Controllers
{
    public class users1Controller : Controller
    {
        private universityEntities db = new universityEntities();
        public static string username;
        public static int regNumb = -1;
        public static int regNumbProf = -1;
        // GET: users1
        public ActionResult Index()
        {
            return View(db.users.ToList());
        }

        // GET: users1/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: users1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: users1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "username,password,role")] user user)
        {
            if (ModelState.IsValid)
            {
                if (db.users.Any(x=>x.username == user.username))
                {
                    username = user.username;
                    var professor= db.users.Where(x=>x.role.Equals("Professor") && x.username == user.username).FirstOrDefault();
                    var student= db.users.Where(x => x.role.Equals("Student") && x.username == user.username).FirstOrDefault();
                    var admin = db.users.Where(x => x.role.Equals("Admin") && x.username == user.username).FirstOrDefault();
                    if (professor!=null) {
                        var st2 = db.professors.Where(y => y.users_username.Equals(username));
                        regNumbProf = st2.First().AFM;
                        return RedirectToAction("Index","course_has_student");

                    }
                    else if (student!=null)
                    {
                        var st = db.students.Where(y => y.USERS_USERNAME.Equals(username));
                        regNumb = st.First().registrationNumber;
                        return RedirectToAction("Index","course_has_student");
                    }
                    else if (admin!=null)
                    {
                        return RedirectToAction("Index","users1");
                    }
                    
                    else { }        
                    
                }
                else
                {
                    return RedirectToAction("Edit");
                }
                {

                }
                //db.users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: users1/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: users1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "username,password,role")] user user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: users1/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: users1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
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
