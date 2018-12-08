using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pofo.Models;
using System.IO;

namespace Pofo.Areas.Manage.Controllers
{
    public class PhotosController : Controller
    {
        private PofoDbEntities db = new PofoDbEntities();

        // GET: Manage/Photos
        public ActionResult Index()
        {
            var photos = db.Photos.Include(p => p.Sections);
            return View(photos.ToList());
        }

        // GET: Manage/Photos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photos photos = db.Photos.Find(id);
            if (photos == null)
            {
                return HttpNotFound();
            }
            return View(photos);
        }

        // GET: Manage/Photos/Create
        public ActionResult Create()
        {
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "SectionName");
            return View();
        }

        // POST: Manage/Photos/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PhotoName,SectionId")] Photos photos, HttpPostedFileBase PhotoName)
        {
            if (ModelState.IsValid)
            {
                string filename = DateTime.Now.ToString("yyMMddHHmmss") + PhotoName.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                PhotoName.SaveAs(path);
                photos.PhotoName = filename;
                db.Photos.Add(photos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SectionId = new SelectList(db.Sections, "Id", "SectionName", photos.SectionId);
            return View(photos);
        }

        // GET: Manage/Photos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photos photos = db.Photos.Find(id);
            if (photos == null)
            {
                return HttpNotFound();
            }
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "SectionName", photos.SectionId);
            return View(photos);
        }

        // POST: Manage/Photos/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PhotoName,SectionId")] Photos photos, HttpPostedFileBase PhotoName)
        {

            if (PhotoName != null)
            {
                string filename = DateTime.Now.ToString("yyMMddHHmmss") + PhotoName.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                PhotoName.SaveAs(path);
                photos.PhotoName = filename;
                Photos ph = db.Photos.Find(photos.Id);
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads"), ph.PhotoName));
                db.Entry(photos).State = EntityState.Detached;
            }
            if (ModelState.IsValid)
            {
                db.Entry(photos).State = EntityState.Modified;
                if (PhotoName == null)
                {
                    db.Entry(photos).Property(p => p.PhotoName).IsModified = false;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SectionId = new SelectList(db.Sections, "Id", "SectionName", photos.SectionId);
            return View(photos);
        }

        // GET: Manage/Photos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photos photos = db.Photos.Find(id);
            if (photos == null)
            {
                return HttpNotFound();
            }
            return View(photos);
        }

        // POST: Manage/Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Photos photos = db.Photos.Find(id);
            db.Photos.Remove(photos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
