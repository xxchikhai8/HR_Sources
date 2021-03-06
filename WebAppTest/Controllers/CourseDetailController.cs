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
    public class CourseDetailController : Controller
    {
        private HRContext db = new HRContext();

        public ActionResult Index()
        {
            if (Session["AccountType"] == null )
            {
                return RedirectToAction("Index", "Home");
            }
            var courseDetail = db.CourseDetail.Include(c => c.Course).Include(c => c.Trainees).Include(c => c.Trainer);
            return View(courseDetail.ToList());
        }

        //Course Trainee Roll
        public ActionResult TraineeRoll()
        {
            string Username = Session["Username"].ToString();
            var acc = db.Trainees.Where(a => a.UserName == Username).FirstOrDefault();
            var courseDetail = db.CourseDetail.Where(a => a.TraineeID == acc.TraineeID);
            return View(courseDetail);
        }

        //Course Trainer Assigned
        public ActionResult TrainerRoll()
        {

            return View();
        }

        //All trainees join the course
        public ActionResult CourseRollTrainee()
        {
            return View();
        }

        //All Trainees in the Course that Trainer Assigned
        public ActionResult TraineeRollTrainer()
        {
            return View();
        }

        public ActionResult Create()
        {
            if (Session["AccountType"] == null)
            {
                if (Session["AccountType"].ToString() != "Admin" || Session["AccountType"].ToString() != "Staff")
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.CourseID = new SelectList(db.Course, "CourseID", "CourseName");
            ViewBag.TraineeID = new SelectList(db.Trainees, "TraineeID", "TraineeName");
            ViewBag.TrainerID = new SelectList(db.Trainer, "TrainerID", "FullName");
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "CourseID,TraineeID,TrainerID")] CourseDetail courseDetail)
        {
            if (ModelState.IsValid)
            {
                db.CourseDetail.Add(courseDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Course, "CourseID", "CourseName");
            ViewBag.TraineeID = new SelectList(db.Trainees, "TraineeID", "TraineeName");
            ViewBag.TrainerID = new SelectList(db.Trainer, "TrainerID", "TrainerName");
            return View(courseDetail);
        }

        public ActionResult Edit(string id, string id2, string id3)
        {
            if (Session["AccountType"] == null)
            {
                if (Session["AccountType"].ToString() != "Admin" || Session["AccountType"].ToString() != "Staff")
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            CourseDetail courseDetail = db.CourseDetail.Find(id, id2, id3);
            ViewBag.CourseID = new SelectList(db.Course, "CourseID", "CourseName", courseDetail.CourseID);
            ViewBag.TraineeID = new SelectList(db.Trainees, "TraineeID", "TraineeName", courseDetail.TraineeID);
            ViewBag.TrainerID = new SelectList(db.Trainer, "TrainerID", "FullName", courseDetail.TrainerID);
            return View(courseDetail);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "CourseID,TraineeID,TrainerID")] CourseDetail courseDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Course, "CourseID", "CourseName", courseDetail.CourseID);
            ViewBag.TraineeID = new SelectList(db.Trainees, "TraineeID", "TraineeName", courseDetail.TraineeID);
            ViewBag.TrainerID = new SelectList(db.Trainer, "TrainerID", "FullName", courseDetail.TrainerID);
            return View(courseDetail);
        }

        public ActionResult Delete(string id, string id2, string id3)
        {
            if (Session["AccountType"] == null)
            {
                if (Session["AccountType"].ToString() != "Admin" || Session["AccountType"].ToString() != "Staff")
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            CourseDetail courseDetail = db.CourseDetail.Find(id, id2, id3);
            db.CourseDetail.Remove(courseDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
