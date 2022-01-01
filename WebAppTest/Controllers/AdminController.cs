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
    public class AdminController : Controller
    {
        HRContext db = new HRContext();

        public ActionResult Index()
        {
            if (Session["AdminState"] == null || Session["AdminState"].ToString() != "1")
            {
                return RedirectToAction("Index", "Home");
            }
            var list = db.Admin.ToList<Admin>();
            return View(list);
        }

        public ActionResult Create()
        {
            if (Session["AdminState"] == null || Session["AdminState"].ToString() != "1")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.UserName = new SelectList(db.Account.Where(a => a.AccountType == "Admin"), "UserName", "UserName");
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "AdminID,UserName,AdminName,Age,Email,Specialty,Address")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                var isUsernameAlreadyExists = db.Admin.Any(x => x.UserName == admin.UserName);
                if (isUsernameAlreadyExists)
                {
                    return View(admin);
                }
                var isUsernameAlreadyExist = db.Admin.Any(x => x.AdminID == admin.AdminID);
                if (isUsernameAlreadyExist)
                {
                    return View(admin);
                }
                db.Admin.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserName = new SelectList(db.Account, "UserName", "UserName", admin.UserName );
            return View(admin);
        }

        public ActionResult Edit(string id)
        {
            if (Session["AdminState"] == null || Session["AdminState"].ToString() != "1")
            {
                return RedirectToAction("Index", "Home");
            }
            Admin admin = db.Admin.Find(id);
            return View(admin);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "AdminID,UserName,AdminName,Age,Email,Specialty,Address")] Admin admin)
        {
            db.Entry(admin).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            Admin admin = db.Admin.Find(id);
            db.Admin.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
