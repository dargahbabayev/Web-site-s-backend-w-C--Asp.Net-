using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pofo.Models;

namespace Pofo.Controllers
{
    
    public class CommentsController : Controller
    {
        PofoDbEntities db = new PofoDbEntities();
        // GET: Comments
        public ActionResult CreateComment(Comments comment,int singlId)
        {
            if (comment.Name == null || comment.Email == null || comment.Content == null)
            {
                Session["uploadError"] = "Fill the all boxes";
                return RedirectToAction("SinglePost", "BlogPage", new { singlId });
            }

            if (ModelState.IsValid)
            {
                comment.Date = DateTime.Now;
                comment.SingleBlogId = singlId;
                db.Comments.Add(comment);
                db.SaveChanges();
            }
            return RedirectToAction("SinglePost", "BlogPage", new { singlId });
        }
    }
}