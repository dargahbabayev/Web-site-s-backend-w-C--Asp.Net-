using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pofo.Models;

namespace Pofo.Controllers
{
    public class BlogPageController : Controller
    {
        PofoDbEntities db = new PofoDbEntities();
        public ActionResult Index(Search src,int? page)
        {
           if (Session["load"]!=null)
            {
                src.Key = "";
                Session["load"] = false;
            }
           if(page==null )
            {
                page = 1;
            }
            int skip = ((int)page - 1) * 4;
            ViewBag.ActivePage = page;
            var Lang = Request.RequestContext.RouteData.Values["lang"];
            ViewBag.Settings = db.Settings.FirstOrDefault();
            ViewBag.InstaPosts = db.InstaPosts.ToList();
            ViewBag.IntroPhotos = db.Photos.Where(p => p.Sections.SectionName == "TitlePhotosPages");

            ViewBag.TotalPage = Math.Ceiling(db.SingleBlog.Count() / 4.0);

            ViewHome model = new ViewHome
            {
                BlogPage = db.BlogPage.Where(p => p.Languages.LangName == Lang.ToString()).ToList(),

            };
            model.SingleBlog = db.SingleBlog.OrderByDescending(sb => sb.Id).Skip(skip).Take(4).Where(p => p.Languages.LangName == Lang.ToString()).ToList();
            if (src.Key != null || src.Key == "")
            {
                model.SingleBlog = db.SingleBlog.OrderByDescending(sb=>sb.Id).Skip(skip).Take(4).Where(p => p.Title.ToLower().Contains(src.Key.ToLower()) && p.Languages.LangName == Lang.ToString()).ToList();
                Session["load"]=true;
            }
          
            foreach (var item in model.BlogPage)
            {
                ViewBag.MainSlogan = item.MainSlogan;
            }
            return View(model);
        }
        public ActionResult SinglePost(int singlId)
        {
            var Lang = Request.RequestContext.RouteData.Values["lang"];
            ViewBag.Settings = db.Settings.FirstOrDefault();
            ViewBag.InstaPosts = db.InstaPosts.ToList();
            ViewBag.IntroPhotos = db.Photos.Where(p => p.Sections.SectionName == "TitlePhotosPages");
            
            ViewHome model = new ViewHome
            {
                BlogPage = db.BlogPage.Where(p => p.Languages.LangName == Lang.ToString()).ToList(),
                SingleBlog = db.SingleBlog.Where(p => p.Id == singlId && p.Languages.LangName == Lang.ToString()).ToList(),
                AboutManage=db.AboutManage.Where(p => p.Languages.LangName == Lang.ToString()).ToList(),
                BloggCategories= db.BloggCategories.Where(p => p.Languages.LangName == Lang.ToString()).ToList(),
                Tags = db.Tags.Where(p => p.Languages.LangName == Lang.ToString()).ToList(),
                BlogTags =db.BlogTags.ToList(),
                InstaPosts=db.InstaPosts.ToList(),
            };

            foreach (var item in db.SingleBlog.Where(b => b.Id == singlId).ToList())
            {
                item.Count++;
                db.SaveChanges();
            }
            foreach (var item in model.BlogPage)
            {
                ViewBag.MainSlogan = item.MainSlogan;
            }
            return View(model);
        }
    }
}