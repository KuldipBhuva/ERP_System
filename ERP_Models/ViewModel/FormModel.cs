using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Models.ViewModel
{
    public class FormModel
    {
        public int FID { get; set; }
        public Nullable<int> MID { get; set; }
        public string FormName { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Status { get; set; }
        public string Sequence { get; set; }
        public bool IsChecked { get; set; }

        public ModuleModel ModuleDetails { get; set; }
    }
}
