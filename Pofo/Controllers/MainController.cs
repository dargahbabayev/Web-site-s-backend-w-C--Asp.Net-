using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pofo.Models;

namespace Pofo.Controllers
{
    public class MainController : Controller
    {
        PofoDbEntities db = new PofoDbEntities();
        public ActionResult Index()
        {

            var Lang = Request.RequestContext.RouteData.Values["lang"];
            ViewBag.Settings = db.Settings.FirstOrDefault();
            ViewBag.InstaPosts = db.InstaPosts.ToList();
            ViewBag.IntroPhotos = db.Photos.Where(p => p.Sections.SectionName == "TitlePhotosPages");

            ViewHome model = new ViewHome
            {
                Sections = db.Sections.ToList(),
                HonorNumbers = db.HonorNumbers.Where(n => n.Languages.LangName == Lang.ToString()).ToList(),
                People = db.People.Where(p => p.Languages.LangName == Lang.ToString()).ToList(),
                Departments = db.Departments.ToList(),
                DepsCards = db.DepCards.ToList(),
                DepsCardsPhotos = db.DepCardPhotos.ToList(),
                MainPageSliders=db.MainPageSlider.Where(p => p.Languages.LangName == Lang.ToString()).ToList(),
            };



          

            return View(model);
        }
    }
}