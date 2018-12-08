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
    public class DepsCardsController : Controller
    {
        private PofoDbEntities db = new PofoDbEntities();

        // GET: Manage/DepsCards
        public ActionResult Index()
        {
            var depCards = db.DepCards.Include(d => d.Departments);
            return View(depCards.ToList());
        }

        // GET: Manage/DepsCards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepCards depCards = db.DepCards.Find(id);
            if (depCards == null)
            {
                return HttpNotFound();
            }
            return View(depCards);
        }

        // GET: Manage/DepsCards/Create
        public ActionResult Create()
        {
            ViewBag.DepId = new SelectList(db.Departments, "Id", "DepName");
            return View();
        }

        // POST: Manage/DepsCards/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DepId,Title,Text")] DepCards depCards)
        {
            if (ModelState.IsValid)
            {
                db.DepCards.Add(depCards);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepId = new SelectList(db.Departments, "Id", "DepName", depCards.DepId);
            return View(depCards);
        }

        // GET: Manage/DepsCards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepCards depCards = db.DepCards.Find(id);
            if (depCards == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepId = new SelectList(db.Departments, "Id", "DepName", depCards.DepId);
            return View(depCards);
        }

        // POST: Manage/DepsCards/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DepId,Title,Text")] DepCards depCards)
        {
            if (ModelState.IsValid)
            {
                db.Entry(depCards).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepId = new SelectList(db.Departments, "Id", "DepName", depCards.DepId);
            return View(depCards);
        }

        // GET: Manage/DepsCards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepCards depCards = db.DepCards.Find(id);
            if (depCards == null)
            {
                return HttpNotFound();
            }
            return View(depCards);
        }

        // POST: Manage/DepsCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            List <DepCardPhotos> depcardPhoto = db.DepCardPhotos.Where(d => d.DepCardId == id).ToList();
            if (depcardPhoto.Count != 0)
            {
                foreach (var item in depcardPhoto)
                {
                    db.DepCardPhotos.Remove(item);
                }
                
            }
            DepCards depCards = db.DepCards.Find(id);
            db.DepCards.Remove(depCards);
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
