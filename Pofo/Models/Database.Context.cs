﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pofo.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PofoDbEntities : DbContext
    {
        public PofoDbEntities()
            : base("name=PofoDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ArticleCompany> ArticleCompany { get; set; }
        public virtual DbSet<HonorNumbers> HonorNumbers { get; set; }
        public virtual DbSet<Languages> Languages { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Photos> Photos { get; set; }
        public virtual DbSet<Agency> Agency { get; set; }
        public virtual DbSet<People> People { get; set; }
        public virtual DbSet<CreativPeople> CreativPeople { get; set; }
        public virtual DbSet<AboutPage> AboutPage { get; set; }
        public virtual DbSet<DepCardPhotos> DepCardPhotos { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<DepCards> DepCards { get; set; }
        public virtual DbSet<BuildWebSites> BuildWebSites { get; set; }
        public virtual DbSet<ServicePage> ServicePage { get; set; }
        public virtual DbSet<Provide> Provide { get; set; }
        public virtual DbSet<Titles> Titles { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<ContactUs> ContactUs { get; set; }
        public virtual DbSet<ContactInfos> ContactInfos { get; set; }
        public virtual DbSet<BoxedMetroPage> BoxedMetroPage { get; set; }
        public virtual DbSet<BloggCategories> BloggCategories { get; set; }
        public virtual DbSet<BlogPhotos> BlogPhotos { get; set; }
        public virtual DbSet<BlogPage> BlogPage { get; set; }
        public virtual DbSet<Sections> Sections { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<AboutManage> AboutManage { get; set; }
        public virtual DbSet<BlogTags> BlogTags { get; set; }
        public virtual DbSet<SingleBlog> SingleBlog { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<InstaPosts> InstaPosts { get; set; }
        public virtual DbSet<MainPageSlider> MainPageSlider { get; set; }
        public virtual DbSet<NewProject> NewProject { get; set; }
        public virtual DbSet<PlanProject> PlanProject { get; set; }
    }
}
