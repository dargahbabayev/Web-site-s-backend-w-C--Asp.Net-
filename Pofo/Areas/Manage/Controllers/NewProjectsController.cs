﻿using System;
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
    public class NewProjectsController : Controller
    {
        private PofoDbEntities db = new PofoDbEntities();

        // GET: Manage/NewProjects
        public ActionResult Index()
        {
            var newProject = db.NewProject.Include(n => n.Languages);
            return View(newProject.ToList());
        }

        // GET: Manage/NewProjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewProject newProject = db.NewProject.Find(id);
            if (newProject == null)
            {
                return HttpNotFound();
            }
            return View(newProject);
        }

        // GET: Manage/NewProjects/Create
        public ActionResult Create()
        {
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName");
            return View();
        }

        // POST: Manage/NewProjects/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Text,Photo,LangId")] NewProject newProject)
        {
            if (ModelState.IsValid)
            {
                db.NewProject.Add(newProject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", newProject.LangId);
            return View(newProject);
        }

        // GET: Manage/NewProjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewProject newProject = db.NewProject.Find(id);
            if (newProject == null)
            {
                return HttpNotFound();
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", newProject.LangId);
            return View(newProject);
        }

        // POST: Manage/NewProjects/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Text,Photo,LangId")] NewProject newProject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newProject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LangId = new SelectList(db.Languages, "Id", "LangName", newProject.LangId);
            return View(newProject);
        }

        // GET: Manage/NewProjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewProject newProject = db.NewProject.Find(id);
            if (newProject == null)
            {
                return HttpNotFound();
            }
            return View(newProject);
        }

        // POST: Manage/NewProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewProject newProject = db.NewProject.Find(id);
            db.NewProject.Remove(newProject);
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
