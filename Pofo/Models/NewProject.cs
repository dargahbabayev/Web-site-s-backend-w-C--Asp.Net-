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
    
    public partial class NewProject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Photo { get; set; }
        public Nullable<int> LangId { get; set; }
        public Nullable<int> DepId { get; set; }
    
        public virtual Departments Departments { get; set; }
        public virtual Languages Languages { get; set; }
    }
}
