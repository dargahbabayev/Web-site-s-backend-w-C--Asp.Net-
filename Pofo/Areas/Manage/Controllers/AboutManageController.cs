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
    public class AboutManageController : Controller
    {
        private PofoDbEntities db = new PofoDbEntities();

        // GET: Manage/AboutManage
        public ActionResult Index()
        {
            var aboutManage = db.AboutManage.Include(a => a.Languages);
            return View(aboutManage.ToList());
        }

        // GET: Manage/AboutManage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutManage aboutManage = db.AboutManage.Find(id);
            if (aboutManage == null)
            {
                return HttpNotFound();
            }
            return View(aboutManage);
        }


        // GET: Manage/AboutManage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutManage aboutManage = db.AboutManage.Find(id);
            if (aboutManage == null)
            {
                return HttpNotFound();
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", aboutManage.LangId);
            return View(aboutManage);
        }

        // POST: Manage/AboutManage/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Photo,Text,LangId")] AboutManage aboutManage, HttpPostedFileBase Photo)
        {
            if (Photo != null)
            {

                string filename = DateTime.Now.ToString("yyMMddHHmmss") + Photo.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                Photo.SaveAs(path);
                aboutManage.Photo = filename;
                AboutManage am = db.AboutManage.Find(aboutManage.Id);
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads"), am.Photo));
                db.Entry(am).State = EntityState.Detached;
            }
            if (ModelState.IsValid)
            {
                db.Entry(aboutManage).State = EntityState.Modified;
                if (Photo == null)
                {
                    db.Entry(aboutManage).Property(p => p.Photo).IsModified = false;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", aboutManage.LangId);
            return View(aboutManage);
        }

        // GET: Manage/AboutManage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutManage aboutManage = db.AboutManage.Find(id);
            if (aboutManage == null)
            {
                return HttpNotFound();
            }
            return View(aboutManage);
        }

        // POST: Manage/AboutManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AboutManage aboutManage = db.AboutManage.Find(id);
            db.AboutManage.Remove(aboutManage);
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
