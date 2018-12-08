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
    public class CreativPeoplesController : Controller
    {
        private PofoDbEntities db = new PofoDbEntities();

        // GET: Manage/CreativPeoples
        public ActionResult Index()
        {
            var creativPeople = db.CreativPeople.Include(c => c.Languages);
            return View(creativPeople.ToList());
        }

        // GET: Manage/CreativPeoples/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreativPeople creativPeople = db.CreativPeople.Find(id);
            if (creativPeople == null)
            {
                return HttpNotFound();
            }
            return View(creativPeople);
        }

        // GET: Manage/CreativPeoples/Create
        public ActionResult Create()
        {
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName");
            return View();
        }

        // POST: Manage/CreativPeoples/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Position,Photo,Facebook,Twitter,Google,Instagram,LangId")] CreativPeople creativPeople, HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {

                string filename = DateTime.Now.ToString("yyMMddHHmmss") + Photo.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                Photo.SaveAs(path);
                creativPeople.Photo = filename;
                db.CreativPeople.Add(creativPeople);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", creativPeople.LangId);
            return View(creativPeople);
        }

        // GET: Manage/CreativPeoples/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreativPeople creativPeople = db.CreativPeople.Find(id);
            if (creativPeople == null)
            {
                return HttpNotFound();
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", creativPeople.LangId);
            return View(creativPeople);
        }

        // POST: Manage/CreativPeoples/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Position,Photo,Facebook,Twitter,Google,Instagram,LangId")] CreativPeople creativPeople,HttpPostedFileBase Photo)
        {
            if (Photo != null)
            {
                string filename = DateTime.Now.ToString("yyMMddHHmmss") + Photo.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                Photo.SaveAs(path);
                creativPeople.Photo = filename;
                CreativPeople cp = db.CreativPeople.Find(creativPeople.Id);
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads"), creativPeople.Photo));
                db.Entry(creativPeople).State = EntityState.Deleted;
            }
            if (ModelState.IsValid)
            {
                db.Entry(creativPeople).State = EntityState.Modified;
                if (Photo == null)
                {
                    db.Entry(creativPeople).Property(p => p.Photo).IsModified = false;
                }
              
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", creativPeople.LangId);
            return View(creativPeople);
        }

        // GET: Manage/CreativPeoples/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreativPeople creativPeople = db.CreativPeople.Find(id);
            if (creativPeople == null)
            {
                return HttpNotFound();
            }
            return View(creativPeople);
        }

        // POST: Manage/CreativPeoples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CreativPeople creativPeople = db.CreativPeople.Find(id);
            db.CreativPeople.Remove(creativPeople);
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
