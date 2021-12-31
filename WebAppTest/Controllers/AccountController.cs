using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTest.Models;
using System.Security.Cryptography;
using System.Text;

namespace WebAppTest.Controllers
{
    public class AccountController : Controller
    {
        HRContext db = new HRContext();
        byte[] encode;
        byte[] hash;

        //The function to display the list of accounts
        public ActionResult Index()
        {
            if (Session["AdminState"] == null || Session["AdminState"].ToString() != "1")
            {
                return RedirectToAction("Index", "Home");
            }
            var list = db.Account.ToList<Account>();
            return View(list);
        }

        //Function function used to receive login request and handle login information
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "UserName,Password,AdminState,AccountType")] Account account)
        {
            Account acc = new Account();
            if (account.UserName != null && account.Password != null)
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                encode = ASCIIEncoding.Default.GetBytes(account.Password);
                hash = md5.ComputeHash(encode);
                account.Password = BitConverter.ToString(hash);
                acc = db.Account.Where(a => a.UserName == account.UserName && a.Password == account.Password).FirstOrDefault();
                if (acc != null)
                {
                    if (acc.AccountType == "Admin")
                    {
                        Admin admin = new Admin();
                        admin = db.Admin.Where(a => a.UserName == acc.UserName).FirstOrDefault();
                        if (admin != null)
                        {
                            Session["Fullname"] = admin.AdminName;
                        }
                        else
                        {
                            Session["Fullname"] = "Admin";
                        }
                    }
                    else if (acc.AccountType == "Staff")
                    {
                        Staff staff = new Staff();
                        staff = db.Staff.Where(a => a.UserName == acc.UserName).FirstOrDefault();
                        if (staff != null)
                        {
                            Session["Fullname"] = staff.StaffName;
                        }
                        else
                        {
                            Session["Fullname"] = "Staff";
                        }
                    }
                    else if (acc.AccountType == "Trainer")
                    {
                        Trainer trainer = new Trainer();
                        trainer = db.Trainer.Where(a => a.UserName == acc.UserName).FirstOrDefault();
                        if (trainer != null)
                        {
                            Session["Fullname"] = trainer.FullName;
                        }
                        else
                        {
                            Session["Fullname"] = "Trainer";
                        }
                    }
                    else if (acc.AccountType == "Trainee")
                    {
                        Trainees trainee = new Trainees();
                        trainee = db.Trainees.Where(a => a.UserName == acc.UserName).FirstOrDefault();
                        if (trainee != null)
                        {
                            Session["Fullname"] = trainee.TraineeName;
                        }
                        else
                        {
                            Session["Fullname"] = "Trainee";
                        }

                    }
                    Session["Username"] = acc.UserName;
                    Session["AccountType"] = acc.AccountType;
                    if (acc.AdminState)
                    {
                        Session["AdminState"] = "1";
                    }
                    else
                    {
                        Session["AdminState"] = "0";
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        //The function used to create a new account
        public ActionResult Create()
        {
            if (Session["AdminState"] == null || Session["AdminState"].ToString() != "1")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //Function function used to handle the request to create a new account
        [HttpPost]
        public ActionResult Create([Bind(Include = "UserName,Password,AdminState,AccountType")] Account account)
        {
            if (ModelState.IsValid)
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                encode = ASCIIEncoding.Default.GetBytes(account.Password);
                hash = md5.ComputeHash(encode);
                account.Password = BitConverter.ToString(hash);
                db.Account.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        //Function function to get account information to update
        public ActionResult Edit(string id)
        {
            if (Session["AdminState"] == null || Session["AdminState"].ToString() != "1")
            {
                return RedirectToAction("Index", "Home");
            }
            Account account = db.Account.Find(id);
            return View(account);
        }

        //Function function to get account information to update
        [HttpPost]
        public ActionResult Edit([Bind(Include = "UserName,Password,AdminState,AccountType")] Account account)
        {
            if (ModelState.IsValid)
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                encode = ASCIIEncoding.Default.GetBytes(account.Password);
                hash = md5.ComputeHash(encode);
                account.Password = BitConverter.ToString(hash);
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        //Function function to delete account information
        [HttpGet]
        public ActionResult Delete(string id)
        {
            Account account = db.Account.Find(id);
            if (account.AccountType == "Admin")
            {
                Admin admin = new Admin();
                admin = db.Admin.Where(a => a.UserName == account.UserName).FirstOrDefault();
                if (admin != null)
                {
                    db.Admin.Remove(admin);
                    db.SaveChanges();
                }
            }
            else if (account.AccountType == "Staff")
            {
                Staff staff = new Staff();
                staff = db.Staff.Where(a => a.UserName == account.UserName).FirstOrDefault();
                if (staff != null)
                {
                    db.Staff.Remove(staff);
                    db.SaveChanges();
                }
            }
            else if (account.AccountType == "Trainer")
            {
                Trainer trainer = new Trainer();
                trainer = db.Trainer.Where(a => a.UserName == account.UserName).FirstOrDefault();
                if (trainer != null)
                {
                    db.Trainer.Remove(trainer);
                    db.SaveChanges();
                }
            }
            else if (account.AccountType == "Trainee")
            {
                Trainees trainee = new Trainees();
                trainee = db.Trainees.Where(a => a.UserName == account.UserName).FirstOrDefault();
                if (trainee != null)
                {
                    db.Trainees.Remove(trainee);
                    db.SaveChanges();
                }
            }
            db.Account.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Function function to handle logout request
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}
