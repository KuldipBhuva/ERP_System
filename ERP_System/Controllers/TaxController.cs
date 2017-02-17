using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_Models.ViewModel;
using ERP_Services;

namespace ERP_System.Controllers
{
    public class TaxController : Controller
    {
        //
        // GET: /Tax/

        public ActionResult Index()
        {
            TaxModel objModel = new TaxModel();

            TaxService objService = new TaxService();

            List<TaxModel> objList = new List<TaxModel>();
            objList = objService.getTax();
            objModel.ListTax = new List<TaxModel>();
            objModel.ListTax.AddRange(objList);

            List<TaxModel> objListSTax = new List<TaxModel>();
            objListSTax = objService.getSTax();
            objModel.ListSTax = new List<TaxModel>();
            objModel.ListSTax.AddRange(objListSTax);

            List<TaxModel> objListATax = new List<TaxModel>();
            objListATax = objService.getActiveTax();
            objModel.ListActiveTax = new List<TaxModel>();
            objModel.ListActiveTax.AddRange(objListATax);
            return View(objModel);
        }
        [HttpPost]
        public ActionResult Index(TaxModel model, string cmd)
        {
            TaxService objService = new TaxService();
            int uid = 0;
            if (Session["UID"] != null)
            {
                uid = Convert.ToInt32(Session["UID"].ToString());
                if (cmd == "Save Tax")
                {
                    model.Status = true;
                    model.CreatedBy = uid;
                    model.CreatedDate = System.DateTime.Now;
                    objService.InsertTax(model);
                    TempData["AMsg"] = "Main Tax Saved Successfully!";
                }
                else
                {
                    model.Name = model.SName;
                    model.Percentage = model.SPercentage;
                    model.Description = model.SDescription;

                    model.Status = true;
                    model.CreatedBy = uid;
                    model.CreatedDate = System.DateTime.Now;
                    objService.InsertSTax(model);
                    TempData["AMsg"] = "Sub Tax Saved Successfully!";
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            TaxModel objModel = new TaxModel();

            TaxService objService = new TaxService();
            objModel = objService.getByID(id);

            return View(objModel);
        }
        [HttpPost]
        public ActionResult Edit(TaxModel model)
        {
            TaxService objService = new TaxService();
            int uid = 0;
            if (Session["UID"] != null)
            {
                uid = Convert.ToInt32(Session["UID"].ToString());
                model.UpdatedBy = uid;
                model.UpdatedDate = System.DateTime.Now;
                objService.Update(model);
                TempData["AMsg"] = "Main Tax Updated Successfully!";
            }
            return RedirectToAction("Index");
        }
        public ActionResult EditSTax(int id)
        {
            TaxModel objModel = new TaxModel();
            TaxService objService = new TaxService();
            objModel = objService.getByIDSTax(id);

            List<TaxModel> objListATax = new List<TaxModel>();
            objListATax = objService.getActiveTax();
            objModel.ListActiveTax = new List<TaxModel>();
            objModel.ListActiveTax.AddRange(objListATax);

            return View(objModel);
        }
        [HttpPost]
        public ActionResult EditSTax(TaxModel model)
        {
            TaxService objService = new TaxService();
            int uid = 0;
            if (Session["UID"] != null)
            {
                uid = Convert.ToInt32(Session["UID"].ToString());
                model.UpdatedBy = uid;
                model.UpdatedDate = System.DateTime.Now;
                objService.UpdateSTax(model);
                TempData["AMsg"] = "Sub Tax Updated Successfully!";
            }
            return RedirectToAction("Index");
            
        }
    }
}
