using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using Pofo.Models;

namespace Pofo.Controllers
{
    public class ContactUsController : Controller
    {
        PofoDbEntities db = new PofoDbEntities();

        // GET: ContactUs
        public ActionResult Index()
        {
            var Lang = Request.RequestContext.RouteData.Values["lang"];
            ViewBag.Settings = db.Settings.FirstOrDefault();
            ViewBag.InstaPosts = db.InstaPosts.ToList();
            ViewBag.IntroPhotos = db.Photos.Where(p => p.Sections.SectionName == "TitlePhotosPages");
         

            ViewHome model = new ViewHome
            {
                ServicePage = db.ServicePage.Where(a => a.Languages.LangName == Lang.ToString()).ToList(),
                PlanProject = db.PlanProject.Where(a => a.Languages.LangName == Lang.ToString()).ToList(),

                ContactInfos=db.ContactInfos.ToList(),

            };
           
           

            foreach (var item in model.ServicePage)
            {
                ViewBag.MainSlogan = item.MainSlogan;
            }

            return View(model);
        }


      


    }
}