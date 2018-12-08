using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pofo.Models;

namespace Pofo.Areas.Manage.Controllers
{
    public class SectionsController : Controller
    {
        private PofoDbEntities db = new PofoDbEntities();

        // GET: Manage/Sections
        public ActionResult Index()
        {
            return View(db.Sections.ToList());
        }

        // GET: Manage/Sections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sections sections = db.Sections.Find(id);
            if (sections == null)
            {
                return HttpNotFound();
            }
            return View(sections);
        }

        // GET: Manage/Sections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manage/Sections/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SectionName")] Sections sections)
        {
            if (ModelState.IsValid)
            {
                db.Sections.Add(sections);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sections);
        }

        // GET: Manage/Sections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sections sections = db.Sections.Find(id);
            if (sections == null)
            {
                return HttpNotFound();
            }
            return View(sections);
        }

        // POST: Manage/Sections/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SectionName")] Sections sections)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sections).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sections);
        }

        // GET: Manage/Sections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sections sections = db.Sections.Find(id);
            if (sections == null)
            {
                return HttpNotFound();
            }
            return View(sections);
        }

        // POST: Manage/Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sections sections = db.Sections.Find(id);
            db.Sections.Remove(sections);
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
