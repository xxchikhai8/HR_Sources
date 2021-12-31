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
    public class TraineesController : Controller
    {
        HRContext db = new HRContext();

        public ActionResult Index()
        {
            if (Session["AccountType"] == null)
                if (Session["AccountType"].ToString() != "Admin" || Session["AccountType"].ToString() == "Staff")
                {
                    return RedirectToAction("Index", "Home");
                }
            var list = db.Trainees.ToList<Trainees>();
            return View(list);
        }

        public ActionResult Create()
        {
            if (Session["AccountType"] == null)
                if (Session["AccountType"].ToString() != "Admin" || Session["AccountType"].ToString() == "Staff")
                {
                    return RedirectToAction("Index", "Home");
                }
            ViewBag.UserName = new SelectList(db.Account.Where(a => a.AccountType == "Trainee"), "UserName", "UserName");
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "TraineeID,UserName,TraineeName,Age,DofB,Email,Edu")] Trainees trainees)
        {
            if (ModelState.IsValid)
            {
                db.Trainees.Add(trainees);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserName = new SelectList(db.Account, "UserName", "UserName", trainees.UserName);
            return View(trainees);
        }

        public ActionResult Edit(string id)
        {
            if (Session["AccountType"] == null)
                if (Session["AccountType"].ToString() != "Admin" || Session["AccountType"].ToString() == "Staff")
                {
                    return RedirectToAction("Index", "Home");
                }
            Trainees trainees = db.Trainees.Find(id);
            return View(trainees);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "TraineeID,UserName,TraineeName,Age,DofB,Email,Edu")] Trainees trainees)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainees).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserName = new SelectList(db.Account, "UserName", "UserName");
            return View(trainees);
        }

        public ActionResult Delete(string id)
        {
            Trainees trainees = db.Trainees.Find(id);
            CourseDetail courseDetail = new CourseDetail();
            do
            {
                courseDetail = db.CourseDetail.Where(a => a.TraineeID == trainees.TraineeID).FirstOrDefault();
                if (courseDetail != null)
                {
                    db.CourseDetail.Remove(courseDetail);
                    db.SaveChanges();
                }
            }
            while (courseDetail != null);
            db.Trainees.Remove(trainees);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
