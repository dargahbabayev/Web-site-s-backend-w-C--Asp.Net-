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
    
    public partial class Titles
    {
        public int Id { get; set; }
        public string RedTitle { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Nullable<int> LangId { get; set; }
        public Nullable<int> SecId { get; set; }
    
        public virtual Languages Languages { get; set; }
        public virtual Sections Sections { get; set; }
    }
}
