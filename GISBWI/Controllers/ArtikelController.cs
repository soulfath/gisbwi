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
        // GET: /Artikel/

//        private bool IsAuthe

        [Authorize]
        public ActionResult Index()
        {
            var artikels = db.Artikels.Include(a => a.Admin).Include(a => a.JenisArtikel);
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
        // GET: /Artikel/Details/5

        public ActionResult Details(int id = 0)
        {
            Artikel artikel = db.Artikels.Find(id);
            if (artikel == null)
            {
                return HttpNotFound();
            }
            return View(artikel);
        }

        //
        // GET: /Artikel/Create

        public ActionResult Create()
        {
            ViewBag.admin_idadmin = new SelectList(db.Admins, "idAdmin", "nama");
            ViewBag.jenis_artikel_idJenisArtikel = new SelectList(db.JenisArtikels, "idJenisArtikel", "nama");
            return View();
        }

        //
        // POST: /Artikel/Create

        [HttpPost]
        public ActionResult Create(Artikel artikel)
        {
            if (ModelState.IsValid)
            {
                db.Artikels.Add(artikel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.admin_idadmin = new SelectList(db.Admins, "idAdmin", "nama", artikel.admin_idadmin);
            ViewBag.jenis_artikel_idJenisArtikel = new SelectList(db.JenisArtikels, "idJenisArtikel", "nama", artikel.jenis_artikel_idJenisArtikel);
            return View(artikel);
        }

        //
        // GET: /Artikel/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Artikel artikel = db.Artikels.Find(id);
            if (artikel == null)
            {
                return HttpNotFound();
            }
            ViewBag.admin_idadmin = new SelectList(db.Admins, "idAdmin", "nama", artikel.admin_idadmin);
            ViewBag.jenis_artikel_idJenisArtikel = new SelectList(db.JenisArtikels, "idJenisArtikel", "nama", artikel.jenis_artikel_idJenisArtikel);
            return View(artikel);
        }

        //
        // POST: /Artikel/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Artikel artikel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artikel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.admin_idadmin = new SelectList(db.Admins, "idAdmin", "nama", artikel.admin_idadmin);
            ViewBag.jenis_artikel_idJenisArtikel = new SelectList(db.JenisArtikels, "idJenisArtikel", "nama", artikel.jenis_artikel_idJenisArtikel);
            return View(artikel);
        }

        //
        // GET: /Artikel/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Artikel artikel = db.Artikels.Find(id);
            if (artikel == null)
            {
                return HttpNotFound();
            }
            return View(artikel);
        }

        //
        // POST: /Artikel/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Artikel artikel = db.Artikels.Find(id);
            db.Artikels.Remove(artikel);
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