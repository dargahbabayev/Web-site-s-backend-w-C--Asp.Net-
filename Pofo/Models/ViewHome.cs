using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


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




    }
}