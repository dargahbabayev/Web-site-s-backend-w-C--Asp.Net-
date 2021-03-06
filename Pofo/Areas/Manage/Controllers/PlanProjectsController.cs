﻿using System;
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
    public class PlanProjectsController : Controller
    {
        private PofoDbEntities db = new PofoDbEntities();

        // GET: Manage/PlanProjects
        public ActionResult Index()
        {
            var planProject = db.PlanProject.Include(p => p.Languages);
            return View(planProject.ToList());
        }

        // GET: Manage/PlanProjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanProject planProject = db.PlanProject.Find(id);
            if (planProject == null)
            {
                return HttpNotFound();
            }
            return View(planProject);
        }
        // GET: Manage/PlanProjects/Create
        public ActionResult Create()
        {
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName");
            return View();
        }

        // POST: Manage/PlanProjects/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Photo,Title,LangId,Text")] PlanProject planProject,HttpPostedFileBase Photo)
        {
            PlanProject plan = db.PlanProject.Where(p => p.LangId == planProject.LangId).FirstOrDefault();
            if (plan != null)
            {
                Session["LangError"] = "This Plan  already has created with selected Language   ";
                return RedirectToAction("index", "planprojects");
            }
            if (Photo != null)
            {
                string filename = DateTime.Now.ToString("yyMMddHHmmss") + Photo.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                Photo.SaveAs(path);
                planProject.Photo = filename;
            }
            if (ModelState.IsValid)
            {
                db.PlanProject.Add(planProject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", planProject.LangId);
            return View(planProject);
        }

     
        // GET: Manage/PlanProjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanProject planProject = db.PlanProject.Find(id);
            if (planProject == null)
            {
                return HttpNotFound();
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", planProject.LangId);
            return View(planProject);
        }

        // POST: Manage/PlanProjects/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Photo,Title,LangId,Text")] PlanProject planProject,HttpPostedFileBase Photo)
        {
            if (Photo != null)
            {

                string filename = DateTime.Now.ToString("yyMMddHHmmss") + Photo.FileName;
                string path = Path.Combine(Server.MapPath("~/Uploads"), filename);
                Photo.SaveAs(path);
                planProject.Photo = filename;
                PlanProject oldPhoto = db.PlanProject.Find(planProject.Id);
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Uploads"), oldPhoto.Photo));
                db.Entry(oldPhoto).State = EntityState.Detached;


            }

            if (ModelState.IsValid)
            {
                db.Entry(planProject).State = EntityState.Modified;
                if (Photo == null)
                {
                    db.Entry(planProject).Property(p => p.Photo).IsModified = false;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", planProject.LangId);
            return View(planProject);
        }

        // GET: Manage/PlanProjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanProject planProject = db.PlanProject.Find(id);
            if (planProject == null)
            {
                return HttpNotFound();
            }
            return View(planProject);
        }

        // POST: Manage/PlanProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlanProject planProject = db.PlanProject.Find(id);
            db.PlanProject.Remove(planProject);
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
