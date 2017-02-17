using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Models.ViewModel
{
    public class UserModuleModel
    {
        public int UMTID { get; set; }
        public Nullable<int> UID { get; set; }
        public Nullable<int> MID { get; set; }
        public bool IsChecked { get; set; }
        public Nullable<int> AssignedBy { get; set; }
        public Nullable<System.DateTime> AssignedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public ModuleModel ModuleDetail { get; set; }
    }
}
