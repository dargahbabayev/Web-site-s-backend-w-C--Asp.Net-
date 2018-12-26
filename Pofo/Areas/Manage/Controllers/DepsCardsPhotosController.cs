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
    public class DepsCardsPhotosController : Controller
    {
        private PofoDbEntities db = new PofoDbEntities();

        // GET: Manage/DepsCardsPhotos
        public ActionResult Index()
        {
            var depCardPhotos = db.DepCardPhotos.Include(d => d.DepCards);
            return View(depCardPhotos.ToList());
        }

        // GET: Manage/DepsCardsPhotos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepCardPhotos depCardPhotos = db.DepCardPhotos.Find(id);
            if (depCardPhotos == null)
            {
                return HttpNotFound();
            }
            return View(depCardPhotos);
        }

        // GET: Manage/DepsCardsPhotos/Create
        public ActionResult Create()
        {
            ViewBag.DepCardId = new SelectList(db.DepCards, "Id", "Name");
            return View();
        }

        // POST: Manage/DepsCardsPhotos/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PhotoName,DepCardId")] DepCardPhotos depCardPhotos, HttpPostedFileBase PhotoName)
        {
            if (ModelState.IsValid)
            {
                string filename = DateTime.Now.ToString("yyMMddHHmmss") + PhotoName.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                PhotoName.SaveAs(path);
                depCardPhotos.PhotoName = filename;
                db.DepCardPhotos.Add(depCardPhotos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepCardId = new SelectList(db.DepCards, "Id", "Name", depCardPhotos.DepCardId);
            return View(depCardPhotos);
        }

        // GET: Manage/DepsCardsPhotos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepCardPhotos depCardPhotos = db.DepCardPhotos.Find(id);
            if (depCardPhotos == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepCardId = new SelectList(db.DepCards, "Id", "Name", depCardPhotos.DepCardId);
            return View(depCardPhotos);
        }

        // POST: Manage/DepsCardsPhotos/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PhotoName,DepCardId")] DepCardPhotos depCardPhotos,HttpPostedFileBase PhotoName)
        {
            if (ModelState.IsValid)
            {
                if (PhotoName != null)
                {
                    string filename = DateTime.Now.ToString("yyMMddHHmmss") + PhotoName.FileName;
                    string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                    PhotoName.SaveAs(path);
                    depCardPhotos.PhotoName = filename;
                    DepCardPhotos dcp = db.DepCardPhotos.Find(depCardPhotos.Id);
                    System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads"), dcp.PhotoName));
                    db.Entry(dcp).State = EntityState.Detached;
                }
                db.Entry(depCardPhotos).State = EntityState.Modified;
                if (PhotoName == null)
                {
                    db.Entry(depCardPhotos).Property(p => p.PhotoName).IsModified = false;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepCardId = new SelectList(db.DepCards, "Id", "Name", depCardPhotos.DepCardId);
            return View(depCardPhotos);
        }

        // GET: Manage/DepsCardsPhotos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepCardPhotos depCardPhotos = db.DepCardPhotos.Find(id);
            if (depCardPhotos == null)
            {
                return HttpNotFound();
            }
            return View(depCardPhotos);
        }

        // POST: Manage/DepsCardsPhotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DepCardPhotos depCardPhotos = db.DepCardPhotos.Find(id);
            db.DepCardPhotos.Remove(depCardPhotos);
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
