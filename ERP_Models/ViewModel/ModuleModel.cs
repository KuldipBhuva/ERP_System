using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Models.ViewModel
{
    public class ModuleModel
    {
        public int MID { get; set; }
        public string Module { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Sequence { get; set; }
        public Nullable<bool> Status { get; set; }
        public bool IsChecked { get; set; }
    }
}
