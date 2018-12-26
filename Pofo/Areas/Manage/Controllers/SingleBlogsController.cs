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
    public class SingleBlogsController : Controller
    {
        private PofoDbEntities db = new PofoDbEntities();

        // GET: Manage/SingleBlogs
        public ActionResult Index()
        {
            var singleBlog = db.SingleBlog.Include(s => s.BloggCategories).Include(s => s.Languages);
            return View(singleBlog.ToList());
        }

        // GET: Manage/SingleBlogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SingleBlog singleBlog = db.SingleBlog.Find(id);
            if (singleBlog == null)
            {
                return HttpNotFound();
            }
            return View(singleBlog);
        }

        // GET: Manage/SingleBlogs/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.BloggCategories, "Id", "CategoryName");
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName");
            return View();
        }

        // POST: Manage/SingleBlogs/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Title,CategoryId,Text,Date,IntroPhoto,BlockVoteText,BlockVoteAutHor,Count,Facebook,Twitter,Instagram,CenterPhoto,LeftPhoto,LangId,AfterCenterPhotoText,LeftPhotoText")] SingleBlog singleBlog, HttpPostedFileBase IntroPhoto, HttpPostedFileBase CenterPhoto, HttpPostedFileBase LeftPhoto  )
        {
            if (LeftPhoto != null)
            {
                string filename = DateTime.Now.ToString("yyMMddHHmmss") + LeftPhoto.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                LeftPhoto.SaveAs(path);
                singleBlog.LeftPhoto = filename;
            }
            if (CenterPhoto != null)
            {
                string filename = DateTime.Now.ToString("yyMMddHHmmss") + CenterPhoto.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                CenterPhoto.SaveAs(path);
                singleBlog.CenterPhoto = filename;
            }
            if (IntroPhoto != null)
            {
                string filename = DateTime.Now.ToString("yyMMddHHmmss") + IntroPhoto.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                IntroPhoto.SaveAs(path);
                singleBlog.IntroPhoto = filename;
            }
            if (ModelState.IsValid)
            {
                singleBlog.Count = 0;
                singleBlog.Date = DateTime.Now;
                db.SingleBlog.Add(singleBlog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.BloggCategories, "Id", "CategoryName", singleBlog.CategoryId);
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", singleBlog.LangId);
            return View(singleBlog);
        }

        // GET: Manage/SingleBlogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SingleBlog singleBlog = db.SingleBlog.Find(id);
            if (singleBlog == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.BloggCategories, "Id", "CategoryName", singleBlog.CategoryId);
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", singleBlog.LangId);
            return View(singleBlog);
        }

        // POST: Manage/SingleBlogs/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Title,CategoryId,Text,Date,IntroPhoto,BlockVoteText,BlockVoteAutHor,Count,Facebook,Twitter,Instagram,CenterPhoto,LeftPhoto,LangId,AfterCenterPhotoText,LeftPhotoText")] SingleBlog singleBlog, HttpPostedFileBase IntroPhoto, HttpPostedFileBase CenterPhoto, HttpPostedFileBase LeftPhoto)
        {
            if (IntroPhoto != null)
            {

                string filename = DateTime.Now.ToString("yyMMddHHmmss") + IntroPhoto.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                IntroPhoto.SaveAs(path);
                singleBlog.IntroPhoto = filename;
                SingleBlog snglBlg = db.SingleBlog.Find(singleBlog.Id);
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads"), snglBlg.IntroPhoto));
                db.Entry(snglBlg).State = EntityState.Detached;
            }
            if (CenterPhoto != null)
            {

                string filename = DateTime.Now.ToString("yyMMddHHmmss") + CenterPhoto.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                CenterPhoto.SaveAs(path);
                singleBlog.CenterPhoto = filename;
                SingleBlog snglBlg = db.SingleBlog.Find(singleBlog.Id);
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads"), snglBlg.CenterPhoto));
                db.Entry(snglBlg).State = EntityState.Detached;
            }
            if (LeftPhoto != null)
            {

                string filename = DateTime.Now.ToString("yyMMddHHmmss") + LeftPhoto.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                LeftPhoto.SaveAs(path);
                singleBlog.LeftPhoto = filename;
                SingleBlog snglBlg = db.SingleBlog.Find(singleBlog.Id);
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads"), snglBlg.LeftPhoto));
                db.Entry(snglBlg).State = EntityState.Detached;
            }




            if (ModelState.IsValid)
            {
                db.Entry(singleBlog).State = EntityState.Modified;
                if (IntroPhoto == null)
                {
                    db.Entry(singleBlog).Property(p => p.IntroPhoto).IsModified = false;
                }
                if (CenterPhoto == null)
                {
                    db.Entry(singleBlog).Property(p => p.CenterPhoto).IsModified = false;
                }
                if (LeftPhoto == null)
                {
                    db.Entry(singleBlog).Property(p => p.LeftPhoto).IsModified = false;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.BloggCategories, "Id", "CategoryName", singleBlog.CategoryId);
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", singleBlog.LangId);
            return View(singleBlog);
        }

        // GET: Manage/SingleBlogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SingleBlog singleBlog = db.SingleBlog.Find(id);
            if (singleBlog == null)
            {
                return HttpNotFound();
            }
            return View(singleBlog);
        }

        // POST: Manage/SingleBlogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SingleBlog singleBlog = db.SingleBlog.Find(id);
            foreach (var item in singleBlog.Comments.ToList())
            {
                db.Comments.Remove(item);
            }
            db.SingleBlog.Remove(singleBlog);
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
