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
    public class BlogTagsController : Controller
    {
        private PofoDbEntities db = new PofoDbEntities();

        // GET: Manage/BlogTags
        public ActionResult Index()
        {
            var blogTags = db.BlogTags.Include(b => b.SingleBlog).Include(b => b.Tags);
            return View(blogTags.ToList());
        }

        // GET: Manage/BlogTags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogTags blogTags = db.BlogTags.Find(id);
            if (blogTags == null)
            {
                return HttpNotFound();
            }
            return View(blogTags);
        }

        // GET: Manage/BlogTags/Create
        public ActionResult Create()
        {
            ViewBag.SingleBlogId = new SelectList(db.SingleBlog, "Id", "Title");
            ViewBag.TagId = new SelectList(db.Tags, "Id", "TagName");
            return View();
        }

        // POST: Manage/BlogTags/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TagId,SingleBlogId")] BlogTags blogTags)
        {

            BlogTags oldblogtag = db.BlogTags.Where(b =>  b.SingleBlogId== blogTags.SingleBlogId && b.TagId==blogTags.TagId).FirstOrDefault();
            if (oldblogtag!=null){
               
                    Session["uploadError"] = "This Blog already have this tag";
                    return RedirectToAction("create", "blogtags");
            }
            if (ModelState.IsValid)
            {
                db.BlogTags.Add(blogTags);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SingleBlogId = new SelectList(db.SingleBlog, "Id", "Title", blogTags.SingleBlogId);
            ViewBag.TagId = new SelectList(db.Tags, "Id", "TagName", blogTags.TagId);
            return View(blogTags);
        }

        // GET: Manage/BlogTags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogTags blogTags = db.BlogTags.Find(id);
            if (blogTags == null)
            {
                return HttpNotFound();
            }
            ViewBag.SingleBlogId = new SelectList(db.SingleBlog, "Id", "Title", blogTags.SingleBlogId);
            ViewBag.TagId = new SelectList(db.Tags, "Id", "TagName", blogTags.TagId);
            return View(blogTags);
        }

        // POST: Manage/BlogTags/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TagId,SingleBlogId")] BlogTags blogTags)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogTags).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SingleBlogId = new SelectList(db.SingleBlog, "Id", "Title", blogTags.SingleBlogId);
            ViewBag.TagId = new SelectList(db.Tags, "Id", "TagName", blogTags.TagId);
            return View(blogTags);
        }

        // GET: Manage/BlogTags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogTags blogTags = db.BlogTags.Find(id);
            if (blogTags == null)
            {
                return HttpNotFound();
            }
            return View(blogTags);
        }

        // POST: Manage/BlogTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogTags blogTags = db.BlogTags.Find(id);
            db.BlogTags.Remove(blogTags);
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
