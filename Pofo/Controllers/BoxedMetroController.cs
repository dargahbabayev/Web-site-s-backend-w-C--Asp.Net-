using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pofo.Models;

namespace Pofo.Controllers
{
    public class BoxedMetroController : Controller
    {
        // GET: BoxedMetro
        PofoDbEntities db = new PofoDbEntities();
        public ActionResult Index()
        {
            var Lang = Request.RequestContext.RouteData.Values["lang"];
            ViewBag.Settings = db.Settings.FirstOrDefault();
            ViewBag.InstaPosts = db.InstaPosts.ToList();
            ViewBag.IntroPhotos = db.Photos.Where(p => p.Sections.SectionName == "TitlePhotosPages");
            ViewHome model = new ViewHome
            {
                BoxedMetroPage = db.BoxedMetroPage.Where(cp => cp.Languages.LangName == Lang.ToString()).ToList(),
                Departments = db.Departments.ToList(),
                DepsCards = db.DepCards.ToList(),
                DepsCardsPhotos = db.DepCardPhotos.ToList()
            };

            foreach (var item in model.BoxedMetroPage)
            {
                ViewBag.MainSlogan = item.MainSlogan;
            }

            return View(model);
        }
    }
}