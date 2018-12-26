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
    public class BloggCategoriesController : Controller
    {
        private PofoDbEntities db = new PofoDbEntities();

        // GET: Manage/BloggCategories
        public ActionResult Index()
        {
            var bloggCategories = db.BloggCategories.Include(b => b.Languages);
            return View(bloggCategories.ToList());
        }

        // GET: Manage/BloggCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloggCategories bloggCategories = db.BloggCategories.Find(id);
            if (bloggCategories == null)
            {
                return HttpNotFound();
            }
            return View(bloggCategories);
        }

        // GET: Manage/BloggCategories/Create
        public ActionResult Create()
        {
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName");
            return View();
        }

        // POST: Manage/BloggCategories/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryName,LangId")] BloggCategories bloggCategories)
        {
            if (ModelState.IsValid)
            {
                db.BloggCategories.Add(bloggCategories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", bloggCategories.LangId);
            return View(bloggCategories);
        }

        // GET: Manage/BloggCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloggCategories bloggCategories = db.BloggCategories.Find(id);
            if (bloggCategories == null)
            {
                return HttpNotFound();
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", bloggCategories.LangId);
            return View(bloggCategories);
        }

        // POST: Manage/BloggCategories/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryName,LangId")] BloggCategories bloggCategories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bloggCategories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", bloggCategories.LangId);
            return View(bloggCategories);
        }

        // GET: Manage/BloggCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BloggCategories bloggCategories = db.BloggCategories.Find(id);
            if (bloggCategories == null)
            {
                return HttpNotFound();
            }
            return View(bloggCategories);
        }

        // POST: Manage/BloggCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BloggCategories bloggCategories = db.BloggCategories.Find(id);
            db.BloggCategories.Remove(bloggCategories);
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
