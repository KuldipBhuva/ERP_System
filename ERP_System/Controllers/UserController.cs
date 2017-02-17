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
    public class UserController : Controller
    {
        //
        // GET: /User/
        ERPEntities Dbcontext = new ERPEntities();
        public ActionResult Index()
        {
            UserService objService = new UserService();
            UserModel objModel = new UserModel();
            List<UserModel> lstUser = new List<UserModel>();

            int uid = 0;
            int rid = 0;
            int cid = 0;
            if (Session["UID"] != null)
            {
                uid = Convert.ToInt32(Session["UID"].ToString());
                rid = Convert.ToInt32(Session["RoleID"].ToString());
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lstUser = objService.getUser(rid, cid);
            objModel.ListUser = new List<UserModel>();
            objModel.ListUser.AddRange(lstUser);

            List<CompanyModel> lstComp = new List<CompanyModel>();
            lstComp = objService.getActiveComp(rid, cid);
            objModel.ListComp = new List<CompanyModel>();
            objModel.ListComp.AddRange(lstComp);

            List<RoleModel> lstRole = new List<RoleModel>();
            lstRole = objService.getActiveRole();
            objModel.ListRole = new List<RoleModel>();
            objModel.ListRole.AddRange(lstRole);

            List<FormModel> lstForms = new List<FormModel>();
            lstForms = objService.getForms();
            objModel.ListForms = new List<FormModel>();
            objModel.ListForms.AddRange(lstForms);

            List<ModuleModel> lstModules = new List<ModuleModel>();
            lstModules = objService.getModules();
            objModel.ListModule = new List<ModuleModel>();
            objModel.ListModule.AddRange(lstModules);


            List<AccountingYearModel> lstAcYr = new List<AccountingYearModel>();
            lstAcYr = objService.getAcYr();
            objModel.ListAcYr = new List<AccountingYearModel>();
            objModel.ListAcYr.AddRange(lstAcYr);

            return View(objModel);
        }
        [HttpPost]
        public ActionResult Index(UserModel model, string cmd, UserModuleModel module, UserFormModel form)
        {
            UserService objService = new UserService();
           
            int uid = 0;
            int rid = 0;
            int cid = 0;
            int user = 0;
            if (Session["UID"] != null)
            {
                uid = Convert.ToInt32(Session["UID"].ToString());
                rid = Convert.ToInt32(Session["RoleID"].ToString());
                cid = Convert.ToInt32(Session["CompID"].ToString());
                if (cmd == "Save")
                {
                    try
                    {
                        //if (rid == 2)
                        //{
                        //    model.CompID = cid;
                        //}
                        model.Status = true;
                        model.CanLogin = true;
                        model.CreatedDate = System.DateTime.Now;
                        model.CreatedBy = uid;
                        model.UserName = model.Email;
                        model.Password = Convert.ToString(model.Mobile);
                        user = objService.Insert(model);

                        if (model.ListModule != null)
                        {
                            foreach (var m in model.ListModule)
                            {

                                module.MID = m.MID;
                                module.UID = user;
                                module.IsChecked = m.IsChecked;
                                module.AssignedBy = uid;
                                module.AssignedDate = System.DateTime.Now;
                                objService.InsertUserModule(module);
                            }
                            foreach (var f in model.ListForms)
                            {
                                form.UID = user;
                                form.FID = f.FID;
                                form.IsChecked = f.IsChecked;
                                form.AssignedDate = System.DateTime.Now;
                                form.AssignedBy = uid;
                                objService.InsertUserForm(form);
                            }
                        }
                        else
                        {
                            foreach (var m in model.ListUserModule)
                            {

                                module.MID = m.MID;
                                module.UID = user;
                                module.IsChecked = m.IsChecked;
                                module.AssignedBy = uid;
                                module.AssignedDate = System.DateTime.Now;
                                objService.InsertUserModule(module);
                            }
                            foreach (var f in model.ListUserForms)
                            {
                                form.UID = user;
                                form.FID = f.FID;
                                form.IsChecked = f.IsChecked;
                                form.AssignedDate = System.DateTime.Now;
                                form.AssignedBy = uid;
                                objService.InsertUserForm(form);
                            }
                            
                        }
                        
                        
                        TempData["AMsg"] = "Saved Successfully!";
                    }
                    catch (Exception ex)
                    {
                        TempData["AMsg"] = "Error Occured, " + ex;
                    }
                }
                else
                {
                    try
                    {
                        
                        model.UpdatedBy = uid;
                        model.UpdatedDate = System.DateTime.Now;
                        objService.Update(model);
                        if (model.reset == true)
                        {
                            UserMaster um = Dbcontext.UserMasters.Where(m => m.UID == model.UID).SingleOrDefault();
                            um.Password = Convert.ToString(model.Mobile);
                            um.UserName = model.Email;
                            Dbcontext.SaveChanges();
                        }
                        user = model.UID;
                        foreach (var m in model.ListUserModule)
                        {

                            UserModuleTran userData = Dbcontext.UserModuleTrans.Where(u => u.UID == user && u.MID == m.MID).SingleOrDefault();
                            userData.MID = m.MID;
                            userData.UID = user;
                            userData.IsChecked = m.IsChecked;
                            userData.UpdatedBy = uid;
                            userData.UpdatedDate = System.DateTime.Now;
                            Dbcontext.SaveChanges();
                        }
                        foreach (var f in model.ListUserForms)
                        {
                            UserFormTran userForm = Dbcontext.UserFormTrans.Where(u => u.UID == user && u.FID == f.FID).SingleOrDefault();
                            userForm.UID = user;
                            userForm.FID = f.FID;
                            userForm.IsChecked = f.IsChecked;
                            userForm.UpdatedDate = System.DateTime.Now;
                            userForm.UpdatedBy = uid;
                            Dbcontext.SaveChanges();
                        }
                        if (model.reset == true)
                        {
                            TempData["AMsg"] = "Password Reset and Record Updated Successfully.";
                        }
                        else
                        {
                            TempData["AMsg"] = "Updated Successfully!";
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["AMsg"] = "Error Occured, " + ex;
                    }
                }
                
            }
            else
            {
                Response.Redirect("/Login/Index");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            UserModel objModel = new UserModel();
            UserService objService = new UserService();
            List<UserModel> lstUser = new List<UserModel>();
            int uid = 0;
            int rid = 0;
            int cid = 0;
            if (Session["UID"] != null)
            {
                uid = Convert.ToInt32(Session["UID"].ToString());
                rid = Convert.ToInt32(Session["RoleID"].ToString());
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            objModel = objService.getByID(id);

            lstUser = objService.getUser(rid, cid);
            objModel.ListUser = new List<UserModel>();
            objModel.ListUser.AddRange(lstUser);

            List<CompanyModel> lstComp = new List<CompanyModel>();
            lstComp = objService.getActiveComp(rid, cid);
            objModel.ListComp = new List<CompanyModel>();
            objModel.ListComp.AddRange(lstComp);

            List<RoleModel> lstRole = new List<RoleModel>();
            lstRole = objService.getActiveRole();
            objModel.ListRole = new List<RoleModel>();
            objModel.ListRole.AddRange(lstRole);

            List<UserModuleModel> lstUserModule = new List<UserModuleModel>();
            lstUserModule = objService.getUserModules(id);
            objModel.ListUserModule = new List<UserModuleModel>();
            objModel.ListUserModule.AddRange(lstUserModule);

            List<UserFormModel> lstUserForm = new List<UserFormModel>();
            lstUserForm = objService.getUserForms(id);
            objModel.ListUserForms = new List<UserFormModel>();
            objModel.ListUserForms.AddRange(lstUserForm);

            List<AccountingYearModel> lstAcYr = new List<AccountingYearModel>();
            lstAcYr = objService.getAcYr();
            objModel.ListAcYr = new List<AccountingYearModel>();
            objModel.ListAcYr.AddRange(lstAcYr);

            ViewBag.Action = "Edit";
            return View("Index", objModel);
        }
    }
}
