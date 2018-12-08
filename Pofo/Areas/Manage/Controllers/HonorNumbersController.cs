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
    public class HonorNumbersController : Controller
    {
        private PofoDbEntities db = new PofoDbEntities();

        // GET: Manage/HonorNumbers
        public ActionResult Index()
        {
            var honorNumbers = db.HonorNumbers.Include(h => h.Languages);
            return View(honorNumbers.ToList());
        }

        // GET: Manage/HonorNumbers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HonorNumbers honorNumbers = db.HonorNumbers.Find(id);
            if (honorNumbers == null)
            {
                return HttpNotFound();
            }
            return View(honorNumbers);
        }

        // GET: Manage/HonorNumbers/Create
        public ActionResult Create()
        {
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName");
            return View();
        }

        // POST: Manage/HonorNumbers/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Number,LangId")] HonorNumbers honorNumbers)
        {
            if (ModelState.IsValid)
            {
                db.HonorNumbers.Add(honorNumbers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", honorNumbers.LangId);
            return View(honorNumbers);
        }

        // GET: Manage/HonorNumbers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HonorNumbers honorNumbers = db.HonorNumbers.Find(id);
            if (honorNumbers == null)
            {
                return HttpNotFound();
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", honorNumbers.LangId);
            return View(honorNumbers);
        }

        // POST: Manage/HonorNumbers/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Number,LangId")] HonorNumbers honorNumbers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(honorNumbers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", honorNumbers.LangId);
            return View(honorNumbers);
        }

        // GET: Manage/HonorNumbers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HonorNumbers honorNumbers = db.HonorNumbers.Find(id);
            if (honorNumbers == null)
            {
                return HttpNotFound();
            }
            return View(honorNumbers);
        }

        // POST: Manage/HonorNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HonorNumbers honorNumbers = db.HonorNumbers.Find(id);
            db.HonorNumbers.Remove(honorNumbers);
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
