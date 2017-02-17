using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_Models;
using ERP_Models.ViewModel;
using ERP_Services;

namespace ERP_System.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        ERPEntities Dbcontext = new ERPEntities();
        public ActionResult Index()
        {
            Session["UID"] = null;
            Session["RoleID"] = null;
            Session["CompID"] = null;
            Session["Name"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Index(UserModel model)
        {
            UserService objService = new UserService();
            UserModel objModel = new UserModel();

            objModel = objService.GetAuthUser(model.UserName, model.Password);
            if (objModel != null)
            {
                //if (objModel.Portal == true && objModel.Status == 1)
                if (objModel.Status == true && objModel.CanLogin == true)
                {
                    Session["UID"] = objModel.UID;
                    Session["RoleID"] = objModel.RoleID;
                    Session["CompID"] = objModel.CompID;
                    Session["Name"] = objModel.Title + ' ' + objModel.FirstName + ' ' + objModel.LastName;

                    //DateTime cdt = System.DateTime.Now;
                    //DateTime dt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    //model.DateTime = dt;
                    //model.IPAddress = System.Web.HttpContext.Current.Request.Params["REMOTE_ADDR"];
                    //model.UID = objModel.UID;
                    //objService.InsertLog(model);

                    Response.Redirect("/Dashboard/Index");
                    return RedirectToAction("/Dashboard/Index");
                }
                else
                {
                    TempData["AMsg"] = "You are not able to access portal.contact to admin.";
                    return View();
                }
            }
            else
            {
                TempData["AMsg"] = "Username or Password not Matched!!";
                return View();
            }

        }
        [HttpPost]
        public ActionResult ResetPwd(FormCollection All)
        {

            string email = All["txtEmail"];

            var lst = Dbcontext.UserMasters.Where(m => m.Email == email).SingleOrDefault();
            int uid = 0;

            if (lst != null)
            {
                uid = lst.UID;
                UserMaster um = Dbcontext.UserMasters.Where(m => m.UID == uid).SingleOrDefault();
                um.UserName = lst.Email;
                um.Password = Convert.ToString(lst.Mobile);
                Dbcontext.SaveChanges();
                TempData["AMsg"] = "Password has been reset, you can login with email as username and mobile as password";

                //TempData["UserMsg"] = "Password has been reset, you can login with email as username and mobile as password";

                Response.Redirect("/Login/Index");
                return Content("<script language='javascript' type='text/javascript'>alert('Password has been reset, you can login with email as username and mobile as password');</script>");
            }
            else
            {
                TempData["AMsg"] = "Email Not Matched!";
                //return Content("<script language='javascript' type='text/javascript'>alert('New Password and Confirm New Password Not Matched.');</script>");
                //TempData["EMsg"] = "Email Not Matched.";


                Response.Redirect("/Login/Index");
                return Content("<script language='javascript' type='text/javascript'>alert('Email Not Matched.');</script>");
            }
        }
    }
}
