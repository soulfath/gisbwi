using System;
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
        // GET: /admin/

        public ActionResult Index()
        {
            return View(db.admins.ToList());
        }

        //
        // GET: /admin/Details/5

        public ActionResult Details(int id = 0)
        {
            admin admin = db.admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        //
        // GET: /admin/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /admin/Create

        [HttpPost]
        public ActionResult Create(admin admin)
        {
            if (ModelState.IsValid)
            {
                db.admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }

        //
        // GET: /admin/Edit/5

        public ActionResult Edit(int id = 0)
        {
            admin admin = db.admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        //
        // POST: /admin/Edit/5

        [HttpPost]
        public ActionResult Edit(admin admin)
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
        // GET: /admin/Delete/5

        public ActionResult Delete(int id = 0)
        {
            admin admin = db.admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        //
        // POST: /admin/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            admin admin = db.admins.Find(id);
            db.admins.Remove(admin);
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
        public ActionResult Login(admin admin)
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
                var user = db.admins.FirstOrDefault(u => u.username == username);
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