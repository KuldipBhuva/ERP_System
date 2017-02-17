using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Models.ViewModel
{
    public class AccountingYearModel
    {
        public int AYID { get; set; }
        public string Year { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
