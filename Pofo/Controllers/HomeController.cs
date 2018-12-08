using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using Pofo.Models;

namespace Pofo.Controllers
{
    public class HomeController : Controller
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
                AboutPage = db.AboutPage.Where(a => a.Languages.LangName == Lang.ToString()).ToList(),
                Sections = db.Sections.ToList(),
                HonorNumbers = db.HonorNumbers.Where(n => n.Languages.LangName == Lang.ToString()).ToList(),
                Agency = db.Agency.Where(a => a.Languages.LangName == Lang.ToString()).ToList(),
                People = db.People.Where(p => p.Languages.LangName == Lang.ToString()).ToList(),
                CreativPeople = db.CreativPeople.Where(cp => cp.Languages.LangName == Lang.ToString()).ToList(),
                Departments = db.Departments.ToList(),
                DepsCards = db.DepCards.ToList(),
                DepsCardsPhotos = db.DepCardPhotos.ToList()
            };



            foreach (var item in model.AboutPage)
            {
                ViewBag.MainSlogan = item.MainSlogan;
            }

            return View(model);
        }




    }
}