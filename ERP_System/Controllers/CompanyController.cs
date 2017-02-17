using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_Models;
using ERP_Models.ViewModel;
using ERP_Services;

namespace ERP_System.Controllers
{
    public class CompanyController : Controller
    {
        //
        // GET: /Company/
        ERPEntities db = new ERPEntities();
        public ActionResult Index()
        {
            CompanyModel objModel = new CompanyModel();
            CompanyService objService = new CompanyService();
            List<CompanyModel> lstComp = new List<CompanyModel>();

            int uid = 0;
            int rid = 0;
            int cid = 0;
            if (Session["UID"] != null)
            {
                uid = Convert.ToInt32(Session["UID"].ToString());
                rid = Convert.ToInt32(Session["RoleID"].ToString());
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }

            lstComp = objService.getComp(rid, cid);
            objModel.ListComp = new List<CompanyModel>();
            objModel.ListComp.AddRange(lstComp);

            List<CompanyModel> lstPComp = new List<CompanyModel>();
            lstPComp = objService.getPComp();
            objModel.ListPComp = new List<CompanyModel>();
            objModel.ListPComp.AddRange(lstPComp);
           

            return View(objModel);
        }
        [HttpPost]
        public ActionResult Index(CompanyModel model, string cmd)
        {
            CompanyService objService = new CompanyService();
            HttpPostedFileBase logo = Request.Files["fileLogo"];
            HttpPostedFileBase sign = Request.Files["fileSign"];
            int uid = 0;
            if (Session["UID"] != null)
            {
                uid = Convert.ToInt32(Session["UID"].ToString());
                if (logo != null)
                {
                    var logoPhoto = Path.GetFileName(logo.FileName);

                    if (logoPhoto != "")
                    {
                        var path = Path.Combine(Server.MapPath("/PhotoUpload/Logo"), logoPhoto);
                        logo.SaveAs(path);
                        model.Logo = logoPhoto;
                    }
                }
                if (sign != null)
                {
                    var signFile = Path.GetFileName(sign.FileName);
                    if (signFile != "")
                    {
                        var path = Path.Combine(Server.MapPath("/PhotoUpload/Sign"), signFile);
                        sign.SaveAs(path);
                        model.Sign = signFile;
                    }
                }
                if (cmd == "Save")
                {
                    try
                    {
                        int rid = 0;
                        int cid = 0;
                        if (Session["RoleID"] != null)
                        {
                            rid = Convert.ToInt32(Session["RoleID"].ToString());
                            cid = Convert.ToInt32(Session["CompID"].ToString());
                        }
                        if (rid == 2)
                        {
                            model.PCompID = cid;
                        }
                        model.Status = true;
                        model.CreatedBy = uid;
                        model.CreatedDate = System.DateTime.Now;
                        objService.InsertComp(model);
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
                        TempData["AMsg"] = "Updated Successfully!";
                    }
                    catch (Exception ex)
                    {
                        TempData["AMsg"] = "Error Occured," + ex;
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
            CompanyModel objModel = new CompanyModel();
            CompanyService objService = new CompanyService();
            List<CompanyModel> lstComp = new List<CompanyModel>();
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


            lstComp = objService.getComp(rid, cid);
            objModel.ListComp = new List<CompanyModel>();
            objModel.ListComp.AddRange(lstComp);

            List<CompanyModel> lstPComp = new List<CompanyModel>();
            lstPComp = objService.getPComp();
            objModel.ListPComp = new List<CompanyModel>();
            objModel.ListPComp.AddRange(lstPComp);

            //List<AccountingYearModel> lstAcYr = new List<AccountingYearModel>();
            //lstAcYr = objService.getAcYr();
            //objModel.ListAcYr = new List<AccountingYearModel>();
            //objModel.ListAcYr.AddRange(lstAcYr);

            ViewBag.Action = "Edit";
            return View("Index", objModel);
        }
        //[HttpPost]
        //public ActionResult Edit(CompanyModel model)
        //{
        //    CompanyService objService = new CompanyService();
        //    model.UpdatedBy = 1;
        //    model.UpdatedDate = System.DateTime.Now;
        //    objService.Update(model);
        //    return RedirectToAction("Index");
        //}
    }
}
