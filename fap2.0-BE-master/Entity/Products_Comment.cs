//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _3.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Products_Comment
    {
        public int id { get; set; }
        public Nullable<int> productid { get; set; }
        public string fullname { get; set; }
        public string title { get; set; }
        public string comment { get; set; }
        public Nullable<int> memberid { get; set; }
        public Nullable<int> numberrate { get; set; }
        public Nullable<int> ParentId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsLock { get; set; }
        public string titlesearch { get; set; }
        public string dateadded { get; set; }
    }
}
