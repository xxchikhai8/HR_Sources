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
    public class StaffController : Controller
    {
        HRContext db = new HRContext();

        public ActionResult Index()
        {
            if (Session["AdminState"] == null || Session["AdminState"].ToString() != "1")
            {
                return RedirectToAction("Index", "Home");
            }
            var list = db.Staff.ToList<Staff>();
            return View(list);
        }

        public ActionResult Create()
        {
            if (Session["AdminState"] == null || Session["AdminState"].ToString() != "1")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.UserName = new SelectList(db.Account.Where(a => a.AccountType == "Staff"), "UserName", "UserName");
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "StaffID,UserName,StaffName,Age,Email,Address")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Staff.Add(staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserName = new SelectList(db.Account, "UserName", "UserName", staff.UserName);
            return View(staff);
        }

        public ActionResult Edit(string id)
        {
            if (Session["AdminState"] == null || Session["AdminState"].ToString() != "1")
            {
                return RedirectToAction("Index", "Home");
            }
            Staff staff = db.Staff.Find(id);
            return View(staff);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "StaffID,UserName,StaffName,Age,Email,Address")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staff);
        }

        public ActionResult Delete(string id)
        {
            Staff staff = db.Staff.Find(id);
            db.Staff.Remove(staff);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
