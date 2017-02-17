using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ERP_Models;
using ERP_Models.ViewModel;

namespace ERP_Services
{
    public class TaxService
    {
        ERPEntities Dbcontext = new ERPEntities();
        public List<TaxModel> getTax()
        {
            try
            {
                Mapper.CreateMap<TaxMaster, TaxModel>();
                List<TaxMaster> objCityMaster = Dbcontext.TaxMasters.ToList();
                List<TaxModel> objCityItem = Mapper.Map<List<TaxModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TaxModel> getSTax()
        {
            try
            {
                Mapper.CreateMap<TaxTran, TaxModel>();
                List<TaxTran> objCityMaster = Dbcontext.TaxTrans.ToList();
                List<TaxModel> objCityItem = Mapper.Map<List<TaxModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertTax(TaxModel model)
        {
            try
            {
                Mapper.CreateMap<TaxModel, TaxMaster>();
                TaxMaster objUser = Mapper.Map<TaxMaster>(model);
                Dbcontext.TaxMasters.Add(objUser);
                return Dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int InsertSTax(TaxModel model)
        {
            try
            {
                Mapper.CreateMap<TaxModel, TaxTran>();
                TaxTran objUser = Mapper.Map<TaxTran>(model);
                Dbcontext.TaxTrans.Add(objUser);
                return Dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public TaxModel getByID(int id)
        {
            try
            {
                Mapper.CreateMap<TaxMaster, TaxModel>();
                TaxMaster objCityMaster = Dbcontext.TaxMasters.Where(m => m.TID == id).SingleOrDefault();
                TaxModel objCityItem = Mapper.Map<TaxModel>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TaxModel getByIDSTax(int id)
        {
            try
            {
                Mapper.CreateMap<TaxTran, TaxModel>();
                TaxTran objCityMaster = Dbcontext.TaxTrans.Where(m => m.TTID == id).SingleOrDefault();
                TaxModel objCityItem = Mapper.Map<TaxModel>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Update(TaxModel model)
        {
            Mapper.CreateMap<TaxModel, TaxMaster>();
            TaxMaster objUser = Dbcontext.TaxMasters.SingleOrDefault(m => m.TID == model.TID);
            objUser = Mapper.Map(model, objUser);
            return Dbcontext.SaveChanges();
        }
        public int UpdateSTax(TaxModel model)
        {
            Mapper.CreateMap<TaxModel, TaxTran>();
            TaxTran objUser = Dbcontext.TaxTrans.SingleOrDefault(m => m.TTID == model.TTID);
            objUser = Mapper.Map(model, objUser);
            return Dbcontext.SaveChanges();
        }
        public List<TaxModel> getActiveTax()
        {
            try
            {
                Mapper.CreateMap<TaxMaster, TaxModel>();
                List<TaxMaster> objCityMaster = Dbcontext.TaxMasters.Where(m=>m.Status==true).ToList();
                List<TaxModel> objCityItem = Mapper.Map<List<TaxModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
