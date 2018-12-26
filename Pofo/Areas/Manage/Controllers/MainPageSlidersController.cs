using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pofo.Models;

namespace Pofo.Areas.Manage.Controllers
{
    public class MainPageSlidersController : Controller
    {
        private PofoDbEntities db = new PofoDbEntities();

        // GET: Manage/MainPageSliders
        public ActionResult Index()
        {
            var mainPageSlider = db.MainPageSlider.Include(m => m.Languages);
            return View(mainPageSlider.ToList());
        }

        // GET: Manage/MainPageSliders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainPageSlider mainPageSlider = db.MainPageSlider.Find(id);
            if (mainPageSlider == null)
            {
                return HttpNotFound();
            }
            return View(mainPageSlider);
        }

        // GET: Manage/MainPageSliders/Create
        public ActionResult Create()
        {
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName");
            return View();
        }

        // POST: Manage/MainPageSliders/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Text,Photo,LangId")] MainPageSlider mainPageSlider, HttpPostedFileBase Photo)
        {
            if (Photo != null)
            {

                string filename = DateTime.Now.ToString("yyMMddHHmmss") + Photo.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                Photo.SaveAs(path);
                mainPageSlider.Photo = filename;
            }
            if (ModelState.IsValid)
            {
                db.MainPageSlider.Add(mainPageSlider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", mainPageSlider.LangId);
            return View(mainPageSlider);
        }

        // GET: Manage/MainPageSliders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainPageSlider mainPageSlider = db.MainPageSlider.Find(id);
            if (mainPageSlider == null)
            {
                return HttpNotFound();
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", mainPageSlider.LangId);
            return View(mainPageSlider);
        }

        // POST: Manage/MainPageSliders/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Text,Photo,LangId")] MainPageSlider mainPageSlider, HttpPostedFileBase Photo)
        {
            if (Photo != null)
            {

                string filename = DateTime.Now.ToString("yyMMddHHmmss") + Photo.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                Photo.SaveAs(path);
                mainPageSlider.Photo = filename;
                MainPageSlider oldphoto = db.MainPageSlider.Find(mainPageSlider.Id);
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads"), oldphoto.Photo));
                db.Entry(oldphoto).State = EntityState.Detached;
            }
            if (ModelState.IsValid)
            {
                db.Entry(mainPageSlider).State = EntityState.Modified;
                if (Photo == null)
                {
                    db.Entry(mainPageSlider).Property(p => p.Photo).IsModified = false;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", mainPageSlider.LangId);
            return View(mainPageSlider);
        }

        // GET: Manage/MainPageSliders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainPageSlider mainPageSlider = db.MainPageSlider.Find(id);
            if (mainPageSlider == null)
            {
                return HttpNotFound();
            }
            return View(mainPageSlider);
        }

        // POST: Manage/MainPageSliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MainPageSlider mainPageSlider = db.MainPageSlider.Find(id);
            db.MainPageSlider.Remove(mainPageSlider);
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
