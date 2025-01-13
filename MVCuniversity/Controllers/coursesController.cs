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
    public class coursesController : Controller
    {
        private universityEntities db = new universityEntities();

        // GET: courses
        public ActionResult Index()
        {
            var courses = db.courses.Include(c => c.course_has_student).Include(c => c.professor);
            return View(courses.ToList());
        }

        // GET: courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            course course = db.courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: courses/Create
        public ActionResult Create()
        {
            ViewBag.idCourse = new SelectList(db.course_has_student, "course_idCourse", "course_idCourse");
            ViewBag.professors_AFM = new SelectList(db.professors, "AFM", "name");
            return View();
        }

        // POST: courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCourse,courseTitle,courseSemester,professors_AFM")] course course)
        {
            if (ModelState.IsValid)
            {
                db.courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCourse = new SelectList(db.course_has_student, "course_idCourse", "course_idCourse", course.idCourse);
            ViewBag.professors_AFM = new SelectList(db.professors, "AFM", "name", course.professors_AFM);
            return View(course);
        }

        // GET: courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            course course = db.courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCourse = new SelectList(db.course_has_student, "course_idCourse", "course_idCourse", course.idCourse);
            ViewBag.professors_AFM = new SelectList(db.professors, "AFM", "name", course.professors_AFM);
            return View(course);
        }

        // POST: courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCourse,courseTitle,courseSemester,professors_AFM")] course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCourse = new SelectList(db.course_has_student, "course_idCourse", "course_idCourse", course.idCourse);
            ViewBag.professors_AFM = new SelectList(db.professors, "AFM", "name", course.professors_AFM);
            return View(course);
        }

        // GET: courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            course course = db.courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            course course = db.courses.Find(id);
            db.courses.Remove(course);
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
