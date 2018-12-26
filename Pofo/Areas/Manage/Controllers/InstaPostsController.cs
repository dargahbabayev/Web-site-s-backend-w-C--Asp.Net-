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
    public class InstaPostsController : Controller
    {
        private PofoDbEntities db = new PofoDbEntities();

        // GET: Manage/InstaPosts
        public ActionResult Index()
        {
            return View(db.InstaPosts.ToList());
        }

        // GET: Manage/InstaPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstaPosts instaPosts = db.InstaPosts.Find(id);
            if (instaPosts == null)
            {
                return HttpNotFound();
            }
            return View(instaPosts);
        }

        // GET: Manage/InstaPosts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manage/InstaPosts/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Photo,Link")] InstaPosts instaPosts, HttpPostedFileBase Photo)
        {

            if (Photo != null)
            {

                string filename = DateTime.Now.ToString("yyMMddHHmmss") + Photo.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                Photo.SaveAs(path);
               instaPosts.Photo = filename;
            }
            if (ModelState.IsValid)
            {
                db.InstaPosts.Add(instaPosts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(instaPosts);
        }

        // GET: Manage/InstaPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstaPosts instaPosts = db.InstaPosts.Find(id);
            if (instaPosts == null)
            {
                return HttpNotFound();
            }
            return View(instaPosts);
        }

        // POST: Manage/InstaPosts/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Photo,Link")] InstaPosts instaPosts, HttpPostedFileBase Photo)
        {
            if (Photo != null)
            {

                string filename = DateTime.Now.ToString("yyMMddHHmmss") + Photo.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                Photo.SaveAs(path);
                instaPosts.Photo = filename;
                InstaPosts oldphoto = db.InstaPosts.Find(instaPosts.Id);
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads"), oldphoto.Photo));
                db.Entry(oldphoto).State = EntityState.Detached;
            }
            if (ModelState.IsValid)
            {
                db.Entry(instaPosts).State = EntityState.Modified;
                if (Photo == null)
                {
                    db.Entry(instaPosts).Property(p => p.Photo).IsModified = false;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(instaPosts);
        }

        // GET: Manage/InstaPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstaPosts instaPosts = db.InstaPosts.Find(id);
            if (instaPosts == null)
            {
                return HttpNotFound();
            }
            return View(instaPosts);
        }

        // POST: Manage/InstaPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InstaPosts instaPosts = db.InstaPosts.Find(id);
            db.InstaPosts.Remove(instaPosts);
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
