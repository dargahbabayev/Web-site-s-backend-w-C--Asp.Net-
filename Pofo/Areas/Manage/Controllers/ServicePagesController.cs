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
    public class ServicePagesController : Controller
    {
        private PofoDbEntities db = new PofoDbEntities();

        // GET: Manage/ServicePages
        public ActionResult Index()
        {
            var servicePage = db.ServicePage.Include(s => s.Languages);
            return View(servicePage.ToList());
        }

        // GET: Manage/ServicePages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePage servicePage = db.ServicePage.Find(id);
            if (servicePage == null)
            {
                return HttpNotFound();
            }
            return View(servicePage);
        }

        // GET: Manage/ServicePages/Create
        public ActionResult Create()
        {
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName");
            return View();
        }

        // POST: Manage/ServicePages/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MainSlogan,OverviewTitle,OverviewVideo,OverViewBgPic,LangId")] ServicePage servicePage,HttpPostedFileBase OverViewBgPic)
        {
            var test = db.ServicePage.Where(ap => ap.LangId == servicePage.LangId).FirstOrDefault();
            if (test != null)
            {
                return RedirectToAction("Index");
            }
            if (servicePage.OverViewBgPic == null)
            {
                Session["uploadError"] = "Fill the all boxes";
                return RedirectToAction("create");
            }

            if (ModelState.IsValid)
            {
                string filename = DateTime.Now.ToString("yyMMddHHmmss") + OverViewBgPic.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                OverViewBgPic.SaveAs(path);
                servicePage.OverViewBgPic = filename;
                db.ServicePage.Add(servicePage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", servicePage.LangId);
            return View(servicePage);
        }

        // GET: Manage/ServicePages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePage servicePage = db.ServicePage.Find(id);
            if (servicePage == null)
            {
                return HttpNotFound();
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", servicePage.LangId);
            return View(servicePage);
        }

        // POST: Manage/ServicePages/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MainSlogan,OverviewTitle,OverviewVideo,OverViewBgPic,LangId")] ServicePage servicePage,HttpPostedFileBase OverViewBgPic)
        {
            if (OverViewBgPic != null)
            {

                string filename = DateTime.Now.ToString("yyMMddHHmmss") + OverViewBgPic.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                OverViewBgPic.SaveAs(path);
                servicePage.OverViewBgPic = filename;
                ServicePage sp = db.ServicePage.Find(servicePage.Id);
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads"), servicePage.OverViewBgPic));
                db.Entry(sp).State = EntityState.Detached;
            }
            if (ModelState.IsValid)
            {
                db.Entry(servicePage).State = EntityState.Modified;
                if (OverViewBgPic == null)
                {
                    db.Entry(servicePage).Property(p => p.OverViewBgPic).IsModified = false;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", servicePage.LangId);
            return View(servicePage);
        }

        // GET: Manage/ServicePages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicePage servicePage = db.ServicePage.Find(id);
            if (servicePage == null)
            {
                return HttpNotFound();
            }
            return View(servicePage);
        }

        // POST: Manage/ServicePages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServicePage servicePage = db.ServicePage.Find(id);
            db.ServicePage.Remove(servicePage);
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
