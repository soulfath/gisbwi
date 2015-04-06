using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using GISBWI.ADODotNet;
using GISBWI.Models;

namespace GISBWI.Controllers
{
    public class ArtikelController : Controller
    {
        private GISBWIEntities db = new GISBWIEntities();

        //
        // GET: /artikel/

//        private bool IsAuthe

        [Authorize]
        public ActionResult Index()
        {
            var artikels = db.artikels.Include(a => a.admin).Include(a => a.jenis_artikel);
            return View(artikels.ToList());
        }

        // upload
        public void UploadNow(HttpPostedFileWrapper upload)
        {
            if (upload != null)
            {
                string imageName = upload.FileName;
                string path = Path.Combine(Server.MapPath("~/Images/Uploads"),imageName);
                upload.SaveAs(path);
            }
        }

        public ActionResult uploadPartial()
        {
            var appData = Server.MapPath("~/Images/uploads");
            var images = Directory.GetFiles(appData).Select(x => new ImagesViewModel
            {
                url = Url.Content("/images/uploads/" + Path.GetFileName(x))
            });
            return View(images);
        }
        //
        // GET: /artikel/Details/5

        public ActionResult Details(int id = 0)
        {
            artikel artikel = db.artikels.Find(id);
            if (artikel == null)
            {
                return HttpNotFound();
            }
            return View(artikel);
        }

        //
        // GET: /artikel/Create

        public ActionResult Create()
        {
            ViewBag.admin_idadmin = new SelectList(db.admins, "idAdmin", "nama");
            ViewBag.jenis_artikel_idjenis_artikel = new SelectList(db.jenis_artikel, "idjenis_artikel", "nama");
            return View();
        }

        //
        // POST: /artikel/Create

        [HttpPost]
        public ActionResult Create(artikel artikel)
        {
            if (ModelState.IsValid)
            {
                db.artikels.Add(artikel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.admin_idadmin = new SelectList(db.admins, "idAdmin", "nama", artikel.admin_idadmin);
            ViewBag.jenis_artikel_idjenis_artikel = new SelectList(db.jenis_artikel, "idjenis_artikel", "nama", artikel.jenis_artikel_idjenis_artikel);
            return View(artikel);
        }

        //
        // GET: /artikel/Edit/5

        public ActionResult Edit(int id = 0)
        {
            artikel artikel = db.artikels.Find(id);
            if (artikel == null)
            {
                return HttpNotFound();
            }
            ViewBag.admin_idadmin = new SelectList(db.admins, "idAdmin", "nama", artikel.admin_idadmin);
            ViewBag.jenis_artikel_idjenis_artikel = new SelectList(db.jenis_artikel, "idjenis_artikel", "nama", artikel.jenis_artikel_idjenis_artikel);
            return View(artikel);
        }

        //
        // POST: /artikel/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(artikel artikel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artikel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.admin_idadmin = new SelectList(db.admins, "idAdmin", "nama", artikel.admin_idadmin);
            ViewBag.jenis_artikel_idjenis_artikel = new SelectList(db.jenis_artikel, "idjenis_artikel", "nama", artikel.jenis_artikel_idjenis_artikel);
            return View(artikel);
        }

        //
        // GET: /artikel/Delete/5

        public ActionResult Delete(int id = 0)
        {
            artikel artikel = db.artikels.Find(id);
            if (artikel == null)
            {
                return HttpNotFound();
            }
            return View(artikel);
        }

        //
        // POST: /artikel/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            artikel artikel = db.artikels.Find(id);
            db.artikels.Remove(artikel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}