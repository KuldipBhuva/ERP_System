using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Models.ViewModel
{
    public class UserModel
    {
        public bool reset { get; set; }
        public int UID { get; set; }
        [Required(ErrorMessage = "Company Required")]
        public Nullable<int> CompID { get; set; }
        public Nullable<int> SCompID { get; set; }
        [Required(ErrorMessage = "Role Required")]
        public Nullable<int> RoleID { get; set; }
        [Required(ErrorMessage = "Title Required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "First Name Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name Required")]
        public string LastName { get; set; }
        
        public string UserName { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Mobile Required")]
        public Nullable<decimal> Mobile { get; set; }
        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "City Required")]
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public Nullable<bool> Status { get; set; }
        public bool CanLogin { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> AccID { get; set; }

        public List<UserModel> ListUser { get; set; }
        public List<CompanyModel> ListComp { get; set; }
        public List<RoleModel> ListRole { get; set; }
        public List<FormModel> ListForms { get; set; }
        public List<ModuleModel> ListModule { get; set; }
        public List<UserModuleModel> ListUserModule { get; set; }
        public List<UserFormModel> ListUserForms { get; set; }
        public CompanyModel CompDetails { get; set; }
        public RoleModel RoleDetails { get; set; }
        public List<AccountingYearModel> ListAcYr { get; set; }
    }
}
