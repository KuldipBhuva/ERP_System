using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Models.ViewModel
{
    public class CompanyModel
    {
        public int CompID { get; set; }
        [Required(ErrorMessage = "Company Name Required")]
        public string CompName { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public Nullable<decimal> Phone { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "City Required")]
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
        //[Required(ErrorMessage = "Parent Company Required")]
        public Nullable<int> PCompID { get; set; }
        

        public List<CompanyModel> ListComp { get; set; }
        public List<CompanyModel> ListPComp { get; set; }
        
    }
}
