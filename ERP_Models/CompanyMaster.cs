//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP_Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CompanyMaster
    {
        public int CompID { get; set; }
        public string CompName { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public Nullable<decimal> Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Logo { get; set; }
        public string Sign { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> PCompID { get; set; }
    }
}
