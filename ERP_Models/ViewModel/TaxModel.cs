using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Models.ViewModel
{
    public class TaxModel
    {
        [Required(ErrorMessage = "Tax Type Required")]
        public int TID { get; set; }
        [Required(ErrorMessage = "Tax Type Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Percentage(%) Required")]
        public Nullable<decimal> Percentage { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public int TTID { get; set; }
        [Required(ErrorMessage = "Tax Type Required")]
        public string SName { get; set; }
        [Required(ErrorMessage = "Percentage(%) Required")]
        public Nullable<decimal> SPercentage { get; set; }
        public string SDescription { get; set; }

        public List<TaxModel> ListTax { get; set; }
        public List<TaxModel> ListActiveTax { get; set; }
        public List<TaxModel> ListSTax { get; set; }
        public TaxTranModel StaxDetails { get; set; }
    }
}
