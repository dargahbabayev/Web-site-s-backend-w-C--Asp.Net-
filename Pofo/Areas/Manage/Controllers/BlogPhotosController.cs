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
    public class BlogPhotosController : Controller
    {
        private PofoDbEntities db = new PofoDbEntities();

        // GET: Manage/BlogPhotos
        public ActionResult Index()
        {
            var blogPhotos = db.BlogPhotos.Include(b => b.SingleBlog);
            return View(blogPhotos.ToList());
        }

        // GET: Manage/BlogPhotos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPhotos blogPhotos = db.BlogPhotos.Find(id);
            if (blogPhotos == null)
            {
                return HttpNotFound();
            }
            return View(blogPhotos);
        }

        // GET: Manage/BlogPhotos/Create
        public ActionResult Create()
        {
            ViewBag.SingleBlogId = new SelectList(db.SingleBlog, "Id", "Title");
            return View();
        }

        // POST: Manage/BlogPhotos/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PhotoName,SingleBlogId")] BlogPhotos blogPhotos,HttpPostedFileBase PhotoName)
        {
            if (PhotoName != null)
            {
                string filnename = DateTime.Now.ToString("yyMMddHHmmss") + PhotoName.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filnename);
                PhotoName.SaveAs(path);
                blogPhotos.PhotoName = filnename;
            }
            if (ModelState.IsValid)
            {
                db.BlogPhotos.Add(blogPhotos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SingleBlogId = new SelectList(db.SingleBlog, "Id", "Title", blogPhotos.SingleBlogId);
            return View(blogPhotos);
        }

        // GET: Manage/BlogPhotos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPhotos blogPhotos = db.BlogPhotos.Find(id);
            if (blogPhotos == null)
            {
                return HttpNotFound();
            }
            ViewBag.SingleBlogId = new SelectList(db.SingleBlog, "Id", "Title", blogPhotos.SingleBlogId);
            return View(blogPhotos);
        }

        // POST: Manage/BlogPhotos/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PhotoName,SingleBlogId")] BlogPhotos blogPhotos,HttpPostedFileBase PhotoName)
        {
            if (PhotoName != null)
            {

                string filename = DateTime.Now.ToString("yyMMddHHmmss") + PhotoName.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                PhotoName.SaveAs(path);
                blogPhotos.PhotoName = filename;
              BlogPhotos oldphoto = db.BlogPhotos.Find(blogPhotos.Id);
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads"), oldphoto.PhotoName));
                db.Entry(oldphoto).State = EntityState.Detached;
            }

            if (ModelState.IsValid)
            {
                db.Entry(blogPhotos).State = EntityState.Modified;
                if (PhotoName == null)
                {
                    db.Entry(blogPhotos).Property(p => p.PhotoName).IsModified = false;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SingleBlogId = new SelectList(db.SingleBlog, "Id", "Title", blogPhotos.SingleBlogId);
            return View(blogPhotos);
        }

        // GET: Manage/BlogPhotos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPhotos blogPhotos = db.BlogPhotos.Find(id);
            if (blogPhotos == null)
            {
                return HttpNotFound();
            }
            return View(blogPhotos);
        }

        // POST: Manage/BlogPhotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPhotos blogPhotos = db.BlogPhotos.Find(id);
            db.BlogPhotos.Remove(blogPhotos);
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
