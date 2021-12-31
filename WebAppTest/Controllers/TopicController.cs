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
    public class TopicController : Controller
    {
        private HRContext db = new HRContext();

        public ActionResult Index()
        {
            if (Session["AccountType"] == null)
                if (Session["AccountType"].ToString() != "Admin" || Session["AccountType"].ToString() == "Staff")
                {
                    return RedirectToAction("Index", "Home");
                }
            var list = db.Topic.ToList();
            return View(list);
        }

        public ActionResult Create()
        {
            if (Session["AccountType"] == null)
                if (Session["AccountType"].ToString() != "Admin" || Session["AccountType"].ToString() == "Staff")
                {
                    return RedirectToAction("Index", "Home");
                }
            ViewBag.CourseID = new SelectList(db.Course, "CourseID", "CourseID");
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "TopicID,TopicName,CourseID")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Topic.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Course, "CourseID", "CourseName", topic.CourseID);
            return View(topic);
        }

        public ActionResult Edit(string id)
        {
            if (Session["AccountType"] == null)
                if (Session["AccountType"].ToString() != "Admin" || Session["AccountType"].ToString() == "Staff")
                {
                    return RedirectToAction("Index", "Home");
                }
            Topic topic = db.Topic.Find(id);
            return View(topic);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "TopicID,TopicName,CourseID")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Course, "CourseID", "CourseName", topic.CourseID);
            return View(topic);
        }

        public ActionResult Delete(string id)
        {
            Topic topic = db.Topic.Find(id);
            db.Topic.Remove(topic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
