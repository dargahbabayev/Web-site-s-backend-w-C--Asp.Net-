//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class ServicePage
    {
        public int Id { get; set; }
        public string MainSlogan { get; set; }
        public string OverviewTitle { get; set; }
        public string OverviewVideo { get; set; }
        public string OverViewBgPic { get; set; }
        public Nullable<int> LangId { get; set; }
    
        public virtual Languages Languages { get; set; }
    }
}
