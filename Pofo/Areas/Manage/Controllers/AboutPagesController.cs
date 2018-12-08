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
    public class AboutPagesController : Controller
    {
        private PofoDbEntities db = new PofoDbEntities();

        // GET: Manage/AboutPages
        public ActionResult Index()
        {
            var aboutPage = db.AboutPage.Include(a => a.Languages);
            return View(aboutPage.ToList());
        }

        // GET: Manage/AboutPages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutPage aboutPage = db.AboutPage.Find(id);
            if (aboutPage == null)
            {
                return HttpNotFound();
            }
            return View(aboutPage);
        }

        // GET: Manage/AboutPages/Create
        public ActionResult Create()
        {
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName");
            return View();
        }

        // POST: Manage/AboutPages/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LeftPreTitle,LeftTitle,LeftText,RightPreText,OverviewTitle,OverViewVideo,OverViewBgPic,MainSlogan,LangId")] AboutPage aboutPage, HttpPostedFileBase OverViewBgPic)
        {
            var test = db.AboutPage.Where(ap => ap.LangId == aboutPage.LangId).FirstOrDefault();
            if (test != null)
            {
                return RedirectToAction("Index");
            }
            if ( aboutPage.OverViewBgPic == null)
            {
                Session["uploadError"] = "Fill the all boxes";
                return RedirectToAction("create");
            }

            if (ModelState.IsValid)
            {
                string filename = DateTime.Now.ToString("yyMMddHHmmss") + OverViewBgPic.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                OverViewBgPic.SaveAs(path);
                aboutPage.OverViewBgPic = filename;
                db.AboutPage.Add(aboutPage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", aboutPage.LangId);
            return View(aboutPage);
        }

        // GET: Manage/AboutPages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutPage aboutPage = db.AboutPage.Find(id);
            if (aboutPage == null)
            {
                return HttpNotFound();
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", aboutPage.LangId);
            return View(aboutPage);
        }

        // POST: Manage/AboutPages/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LeftPreTitle,LeftTitle,LeftText,RightPreText,OverviewTitle,OverViewVideo,OverViewBgPic,MainSlogan,LangId")] AboutPage aboutPage,HttpPostedFileBase OverViewBgPic)
        {
            if (OverViewBgPic != null)
            {

                string filename = DateTime.Now.ToString("yyMMddHHmmss") + OverViewBgPic.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                OverViewBgPic.SaveAs(path);
                aboutPage.OverViewBgPic = filename;
                AboutPage ap = db.AboutPage.Find(aboutPage.Id);
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads"), aboutPage.OverViewBgPic));
                db.Entry(ap).State = EntityState.Detached;
            }

            if (ModelState.IsValid)
            {
                db.Entry(aboutPage).State = EntityState.Modified;
                if (OverViewBgPic == null)
                {
                    db.Entry(aboutPage).Property(p => p.OverViewBgPic).IsModified = false;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", aboutPage.LangId);
            return View(aboutPage);
        }

        // GET: Manage/AboutPages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutPage aboutPage = db.AboutPage.Find(id);
            if (aboutPage == null)
            {
                return HttpNotFound();
            }
            return View(aboutPage);
        }

        // POST: Manage/AboutPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AboutPage aboutPage = db.AboutPage.Find(id);
            db.AboutPage.Remove(aboutPage);
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
