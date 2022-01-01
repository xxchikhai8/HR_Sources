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
    public class TrainerController : Controller
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
            var list = db.Trainer.ToList<Trainer>();
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
            ViewBag.UserName = new SelectList(db.Account.Where(a => a.AccountType == "Trainer"), "UserName", "UserName");
            return View();
        }
        
        public ActionResult Profiles()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            string Username = Session["Username"].ToString();
            Trainer trainer = new Trainer();
            trainer = db.Trainer.Where(a => a.UserName == Username).FirstOrDefault();
            return View(trainer);
        }

        public ActionResult EditProfiles(string id)
        {
            Trainer trainer = new Trainer();
            trainer = db.Trainer.Where(a => a.UserName == id).FirstOrDefault();
            return View(trainer);
        }

        [HttpPost]
        public ActionResult EditProfiles([Bind(Include = "TrainerID,UserName,FullName,Age,Specialty,Address")] Trainer trainer)
        {
            db.Entry(trainer).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Profiles", "Trainer");
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "TrainerID,UserName,FullName,Age,Specialty,Address")] Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                var isUsernameAlreadyExists = db.Trainer.Any(x => x.UserName == trainer.UserName);
                if (isUsernameAlreadyExists)
                {
                    return View(trainer);
                }
                var isUsernameAlreadyExist = db.Trainer.Any(x => x.TrainerID == trainer.TrainerID);
                if (isUsernameAlreadyExist)
                {
                    return View(trainer);
                }
                db.Trainer.Add(trainer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserName = new SelectList(db.Account, "UserName", "UserName", trainer.UserName);
            return View(trainer);
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
            Trainer trainer = db.Trainer.Find(id);
            return View(trainer);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "TrainerID,UserName,FullName,Age,Specialty,Address")] Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trainer);
        }

        public ActionResult Delete(string id)
        {
            Trainer trainer = db.Trainer.Find(id);
            CourseDetail courseDetail = new CourseDetail();
            do
            {
                courseDetail = db.CourseDetail.Where(a => a.TrainerID == trainer.TrainerID).FirstOrDefault();
                if (courseDetail != null)
                {
                    db.CourseDetail.Remove(courseDetail);
                    db.SaveChanges();
                }
            }
            while (courseDetail != null);
            db.Trainer.Remove(trainer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
