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
    public class AgencyController : Controller
    {
        private PofoDbEntities db = new PofoDbEntities();

        // GET: Manage/Agency
        public ActionResult Index()
        {
            var agency = db.Agency.Include(a => a.Languages);
            return View(agency.ToList());
        }

        // GET: Manage/Agency/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agency agency = db.Agency.Find(id);
            if (agency == null)
            {
                return HttpNotFound();
            }
            return View(agency);
        }

        // GET: Manage/Agency/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agency agency = db.Agency.Find(id);
            if (agency == null)
            {
                return HttpNotFound();
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", agency.LangId);
            return View(agency);
        }

        // POST: Manage/Agency/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Photo,Title,Text,LangId")] Agency agency,HttpPostedFileBase Photo)
        {
            if (Photo != null)
            {
              
                string filename = DateTime.Now.ToString("yyMMddHHmmss") + Photo.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                Photo.SaveAs(path);
                agency.Photo = filename;
                Agency agncy = db.Agency.Find(agency.Id);
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads"), agncy.Photo));
                db.Entry(agncy).State=EntityState.Detached;
            }
           
            if (ModelState.IsValid)
            {
                db.Entry(agency).State = EntityState.Modified;
                if (Photo == null)
                {
                    db.Entry(agency).Property(p => p.Photo).IsModified = false;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", agency.LangId);
            return View(agency);
        }

        // GET: Manage/Agency/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agency agency = db.Agency.Find(id);
            if (agency == null)
            {
                return HttpNotFound();
            }
            return View(agency);
        }

        // POST: Manage/Agency/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agency agency = db.Agency.Find(id);
            db.Agency.Remove(agency);
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
