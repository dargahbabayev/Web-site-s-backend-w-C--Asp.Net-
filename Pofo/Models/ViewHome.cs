using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pofo.Models
{
    public class ViewHome
    {
        public List<Departments> Departments { get; set; }
        public List<DepCards> DepsCards { get; set; }
        public List<DepCardPhotos> DepsCardsPhotos { get; set; }
        public List<AboutPage> AboutPage { get; set; }
        public List<Languages> Langs { get; set; }
        public List<Sections> Sections { get; set; }
        public List<Photos> Photos { get; set; }
        public List<HonorNumbers> HonorNumbers { get; set; }
        public List<Agency> Agency { get; set; }
        public List<People> People { get; set; }
        public List<CreativPeople> CreativPeople { get; set; }
        public List<ServicePage> ServicePage { get; set; }
        public List<Team> Team { get; set; }
        public List<NewProject> NewProjects { get; set; }
        public List<ContactInfos> ContactInfos { get; set; }
        public List<BoxedMetroPage> BoxedMetroPage { get; set; }
        public List<AboutManage> AboutManage { get; set; }
        public List<BlogPage> BlogPage { get; set; }
        public List<Tags> Tags { get; set; }
        public List<SingleBlog> SingleBlog { get; set; }
        public List<BloggCategories> BloggCategories { get; set; }
        public List<BlogTags> BlogTags { get; set; }
        public List<InstaPosts> InstaPosts { get; set; }
        public List<MainPageSlider> MainPageSliders { get; set; }



    }
}