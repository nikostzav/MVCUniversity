using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using MVCuniversity.Models;

namespace MVCuniversity.Controllers
{
    public class course_has_studentController : Controller
    {
        public static string Message;
        private universityEntities db = new universityEntities();
        string username = users1Controller.username; 
        // GET: course_has_student
        public ActionResult Index()
        {
            if (users1Controller.regNumb != -1)
            {
                Message = " ";
                List<course_has_student> list = new List<course_has_student>();
                foreach (var k in db.students)
                {
                    foreach (var l in db.course_has_student)
                    {
                        if (k.USERS_USERNAME.Equals(username) && l.students_registrationNumber == users1Controller.regNumb)
                        {
                            list.Add(l);
                        }
                    }

                }
                return View(list);
            }
            else if (users1Controller.regNumbProf != -1)
            {
                Message = "Add new Grade!";
                var course_has_student = db.course_has_student.Include(c => c.course).Include(c => c.student);
                return View(course_has_student);

            }
            else { return View(); }

            

            // return View(course_has_student.ToList());
        }

        // GET: course_has_student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            course_has_student course_has_student = db.course_has_student.Find(id);
            if (course_has_student == null)
            {
                return HttpNotFound();
            }
            return View(course_has_student);
        }

        // GET: course_has_student/Create
        public ActionResult Create()
        {
            ViewBag.courseID = new SelectList(db.courses, "idCourse", "courseTitle");
            ViewBag.students_registrationNumber = new SelectList(db.students, "registrationNumber", "name");
            return View();
        }

        // POST: course_has_student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "courseID,students_registrationNumber,grade,id")] course_has_student course_has_student)
        {
            if (ModelState.IsValid)
            {
                Random r = new Random();
                course_has_student.id = r.Next(0,10000);
                db.course_has_student.Add(course_has_student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.courseID = new SelectList(db.courses, "idCourse", "courseTitle", course_has_student.courseID);
            ViewBag.students_registrationNumber = new SelectList(db.students, "registrationNumber", "name", course_has_student.students_registrationNumber);
            return View(course_has_student);
        }

        // GET: course_has_student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            course_has_student course_has_student = db.course_has_student.Find(id);
            if (course_has_student == null)
            {
                return HttpNotFound();
            }
            ViewBag.courseID = new SelectList(db.courses, "idCourse", "courseTitle", course_has_student.courseID);
            ViewBag.students_registrationNumber = new SelectList(db.students, "registrationNumber", "name", course_has_student.students_registrationNumber);
            return View(course_has_student);
        }

        // POST: course_has_student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "courseID,students_registrationNumber,grade,id")] course_has_student course_has_student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course_has_student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.courseID = new SelectList(db.courses, "idCourse", "courseTitle", course_has_student.courseID);
            ViewBag.students_registrationNumber = new SelectList(db.students, "registrationNumber", "name", course_has_student.students_registrationNumber);
            return View(course_has_student);
        }

        // GET: course_has_student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            course_has_student course_has_student = db.course_has_student.Find(id);
            if (course_has_student == null)
            {
                return HttpNotFound();
            }
            return View(course_has_student);
        }

        // POST: course_has_student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            course_has_student course_has_student = db.course_has_student.Find(id);
            db.course_has_student.Remove(course_has_student);
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
