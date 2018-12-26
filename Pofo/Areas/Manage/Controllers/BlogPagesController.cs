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
    public class BlogPagesController : Controller
    {
        private PofoDbEntities db = new PofoDbEntities();

        // GET: Manage/BlogPages
        public ActionResult Index()
        {
            var blogPage = db.BlogPage.Include(b => b.Languages);
            return View(blogPage.ToList());
        }

        // GET: Manage/BlogPages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPage blogPage = db.BlogPage.Find(id);
            if (blogPage == null)
            {
                return HttpNotFound();
            }
            return View(blogPage);
        }

        // GET: Manage/BlogPages/Create
        public ActionResult Create()
        {
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName");
            return View();
        }

        // POST: Manage/BlogPages/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MainSlogan,LangId")] BlogPage blogPage)
        {
            if (ModelState.IsValid)
            {
                
                db.BlogPage.Add(blogPage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", blogPage.LangId);
            return View(blogPage);
        }

        // GET: Manage/BlogPages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPage blogPage = db.BlogPage.Find(id);
            if (blogPage == null)
            {
                return HttpNotFound();
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", blogPage.LangId);
            return View(blogPage);
        }

        // POST: Manage/BlogPages/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MainSlogan,LangId")] BlogPage blogPage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogPage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", blogPage.LangId);
            return View(blogPage);
        }

        // GET: Manage/BlogPages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPage blogPage = db.BlogPage.Find(id);
            if (blogPage == null)
            {
                return HttpNotFound();
            }
            return View(blogPage);
        }

        // POST: Manage/BlogPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPage blogPage = db.BlogPage.Find(id);
            db.BlogPage.Remove(blogPage);
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
