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
    public class BoxedMetroManageController : Controller
    {
        private PofoDbEntities db = new PofoDbEntities();

        // GET: Manage/BoxedMetroManage
        public ActionResult Index()
        {
            var boxedMetroPage = db.BoxedMetroPage.Include(b => b.Languages);
            return View(boxedMetroPage.ToList());
        }

        // GET: Manage/BoxedMetroManage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoxedMetroPage boxedMetroPage = db.BoxedMetroPage.Find(id);
            if (boxedMetroPage == null)
            {
                return HttpNotFound();
            }
            return View(boxedMetroPage);
        }

        // GET: Manage/BoxedMetroManage/Create
        public ActionResult Create()
        {
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName");
            return View();
        }

        // POST: Manage/BoxedMetroManage/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MainSlogan,LangId")] BoxedMetroPage boxedMetroPage)
        {
            if (ModelState.IsValid)
            {
                db.BoxedMetroPage.Add(boxedMetroPage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", boxedMetroPage.LangId);
            return View(boxedMetroPage);
        }

        // GET: Manage/BoxedMetroManage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoxedMetroPage boxedMetroPage = db.BoxedMetroPage.Find(id);
            if (boxedMetroPage == null)
            {
                return HttpNotFound();
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", boxedMetroPage.LangId);
            return View(boxedMetroPage);
        }

        // POST: Manage/BoxedMetroManage/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MainSlogan,LangId")] BoxedMetroPage boxedMetroPage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boxedMetroPage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", boxedMetroPage.LangId);
            return View(boxedMetroPage);
        }

        // GET: Manage/BoxedMetroManage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoxedMetroPage boxedMetroPage = db.BoxedMetroPage.Find(id);
            if (boxedMetroPage == null)
            {
                return HttpNotFound();
            }
            return View(boxedMetroPage);
        }

        // POST: Manage/BoxedMetroManage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BoxedMetroPage boxedMetroPage = db.BoxedMetroPage.Find(id);
            db.BoxedMetroPage.Remove(boxedMetroPage);
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
