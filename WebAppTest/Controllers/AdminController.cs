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
        //The declaration statement creates a variable for the database call
        HRContext db = new HRContext();

        //Function function to display information of admins
        public ActionResult Index()
        {
            if (Session["AdminState"] == null || Session["AdminState"].ToString() != "1")
            {
                return RedirectToAction("Index", "Home");
            }
            var list = db.Admin.ToList<Admin>();
            return View(list);
        }

        //Function function to create new information of admin
        public ActionResult Create()
        {
            if (Session["AdminState"] == null || Session["AdminState"].ToString() != "1")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.UserName = new SelectList(db.Account.Where(a => a.AccountType == "Admin"), "UserName", "UserName");
            return View();
        }

        //The function function handles the request to create a new administrator information
        [HttpPost]
        public ActionResult Create([Bind(Include = "AdminID,UserName,AdminName,Age,Email,Specialty,Address")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admin.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserName = new SelectList(db.Account, "UserName", "UserName", admin.UserName );
            return View(admin);
        }

        //Function function to get administrator information to update information
        public ActionResult Edit(string id)
        {
            if (Session["AdminState"] == null || Session["AdminState"].ToString() != "1")
            {
                return RedirectToAction("Index", "Home");
            }
            Admin admin = db.Admin.Find(id);
            return View(admin);
        }

        //The function function handles the administrator's information update request
        [HttpPost]
        public ActionResult Edit([Bind(Include = "AdminID,UserName,AdminName,Age,Email,Specialty,Address")] Admin admin)
        {
            db.Entry(admin).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //The function function handles the request to delete administrator information
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
