using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTest.Models;

namespace WebAppTest.Controllers
{
    public class CourseController : Controller
    {
        HRContext db = new HRContext();

        public ActionResult Index()
        {
            if (Session["AccountType"] == null)
            {
                if (Session["AccountType"].ToString() != "Admin" || Session["AccountType"].ToString() == "Staff")
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            var list = db.Course.ToList<Course>();
            return View(list);
        }

        public ActionResult Create()
        {
            if (Session["AccountType"] == null)
            {
                if (Session["AccountType"].ToString() != "Admin" || Session["AccountType"].ToString() == "Staff")
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "CatName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,CourseName,CourseDes,CatID")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Course.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        public ActionResult Edit(string id)
        {
            if (Session["AccountType"] == null)
            {
                if (Session["AccountType"].ToString() != "Admin" || Session["AccountType"].ToString() == "Staff")
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "CatName");
            Course course = db.Course.Find(id);
            return View(course);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "CourseID,CourseName,CourseDes,CatID")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CatID = new SelectList(db.Categories, "CatID", "CatName");
            return View(course);
        }

        public ActionResult Delete(string id)
        {
            Course course = db.Course.Find(id);
            Topic topic = new Topic();
            CourseDetail courseDetail = new CourseDetail();
            do
            {
                topic = db.Topic.Where(a => a.CourseID == course.CourseID).FirstOrDefault();
                if (topic != null)
                {
                    db.Topic.Remove(topic);
                    db.SaveChanges();
                }
            }
            while (topic != null);
            do
            {
                courseDetail = db.CourseDetail.Where(a => a.CourseID == course.CourseID).FirstOrDefault();
                if (courseDetail != null)
                {
                    db.CourseDetail.Remove(courseDetail);
                    db.SaveChanges();
                }
            }
            while (courseDetail != null);
            db.Course.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
