﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GISBWI.ADODotNet;

namespace GISBWI.Controllers
{
    public class AdminController : Controller
    {
        private GISBWIEntities db = new GISBWIEntities();

        // CRUD ADMIN
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View(db.Admins.ToList());
        }

        //
        // GET: /Admin/Details/5

        public ActionResult Details(int id = 0)
        {
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        public ActionResult Create(Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        //
        // GET: /Admin/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        /* LOGIN */
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "FrontEnd");
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            if (IsValid(admin.username, admin.password))
            {
                FormsAuthentication.SetAuthCookie(admin.username, false);
                return RedirectToAction("Index", "BackEnd");
            }
            else
                ModelState.AddModelError("", "username atau password salah");
            return View(admin);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }



        private bool IsValid(String username, string password)
        {
            bool isvalid = false;
            using (var db = new GISBWIEntities())
            {
                var user = db.Admins.FirstOrDefault(u => u.username == username);
                if ( user != null )
                {
                    if (user.password == password)
                    {
                        isvalid = true;
                    }
                }
            }
            return isvalid;
        }
    }
}