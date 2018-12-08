using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pofo.Models;

namespace Pofo.Controllers
{
  
    public class ServicesController : Controller
    {
        PofoDbEntities db = new PofoDbEntities();
        // GET: Services
        public ActionResult Index()
        {
            var Lang = Request.RequestContext.RouteData.Values["lang"];
             ViewBag.Settings = db.Settings.FirstOrDefault();
            ViewBag.InstaPosts = db.InstaPosts.ToList();
            ViewBag.IntroPhotos = db.Photos.Where(p => p.Sections.SectionName == "TitlePhotosPages");
            ViewHome model = new ViewHome
            {
                
                ServicePage =db.ServicePage.Where(s => s.Languages.LangName == Lang.ToString()).ToList(),
                Team= db.Team.Where(s => s.Languages.LangName == Lang.ToString()).ToList(),
            };

            foreach (var item in model.ServicePage)
            {
                ViewBag.MainSlogan = item.MainSlogan;
            }
            return View(model);
        }
    }
}