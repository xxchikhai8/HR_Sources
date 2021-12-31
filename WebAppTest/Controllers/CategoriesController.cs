using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTest.Models;

namespace WebAppTest.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        HRContext db = new HRContext();
        public ActionResult Index()
        {
            if (Session["AccountType"] == null)
                if (Session["AccountType"].ToString() != "Admin" || Session["AccountType"].ToString() == "Staff")
                {
                    return RedirectToAction("Index", "Home");
                }
            var list = db.Categories.ToList<Categories>();
            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["AccountType"] == null)
                if (Session["AccountType"].ToString() != "Admin" || Session["AccountType"].ToString() == "Staff")
                {
                    return RedirectToAction("Index", "Home");
                }
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "CatID, CatName, CatDes")] Categories categories)
        {
            db.Categories.Add(categories);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            Categories categories = db.Categories.Find(id);
            Course course = new Course();
            do
            {
                course = db.Course.Where(a => a.CatID == categories.CatID).FirstOrDefault();
                if (course != null)
                {
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
                }
            }
            while (course != null);
            db.Categories.Remove(categories);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (Session["AccountType"] == null)
                if (Session["AccountType"].ToString() != "Admin" || Session["AccountType"].ToString() == "Staff")
                {
                    return RedirectToAction("Index", "Home");
                }
            Categories categories = db.Categories.Find(id);
            return View(categories);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "CatID, CatName, CatDes")] Categories categories)
        {
            db.Entry(categories).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}