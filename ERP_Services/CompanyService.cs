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
    public class CompanyService
    {
        ERPEntities Dbcontext = new ERPEntities();
        public List<CompanyModel> getComp(int rid,int cid)
        {
            try
            {
                Mapper.CreateMap<CompanyMaster, CompanyModel>();
                List<CompanyMaster> objCityMaster = Dbcontext.CompanyMasters.Where(m => m.PCompID == (rid == 1 ? m.PCompID : cid) || m.CompID == (rid == 1 ? m.CompID : cid)).ToList();
                List<CompanyModel> objCityItem = Mapper.Map<List<CompanyModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CompanyModel> getPComp()
        {
            try
            {
                Mapper.CreateMap<CompanyMaster, CompanyModel>();
                List<CompanyMaster> objCityMaster = Dbcontext.CompanyMasters.Where(m=>m.Status==true && m.PCompID==null).ToList();
                List<CompanyModel> objCityItem = Mapper.Map<List<CompanyModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
        public int InsertComp(CompanyModel model)
        {
            try
            {
                Mapper.CreateMap<CompanyModel, CompanyMaster>();
                CompanyMaster objUser = Mapper.Map<CompanyMaster>(model);
                Dbcontext.CompanyMasters.Add(objUser);
                return Dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public CompanyModel getByID(int id)
        {
            try
            {
                Mapper.CreateMap<CompanyMaster, CompanyModel>();
                CompanyMaster objCityMaster = Dbcontext.CompanyMasters.Where(m => m.CompID == id).SingleOrDefault();
                CompanyModel objCityItem = Mapper.Map<CompanyModel>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Update(CompanyModel model)
        {
            Mapper.CreateMap<CompanyModel, CompanyMaster>();
            CompanyMaster objUser = Dbcontext.CompanyMasters.SingleOrDefault(m => m.CompID == model.CompID);
            objUser = Mapper.Map(model, objUser);
            return Dbcontext.SaveChanges();
        }

        public bool DeleteByID(int id)
        {
            try
            {
                Mapper.CreateMap<CompanyMaster, CompanyModel>();
                CompanyMaster objCityMaster = Dbcontext.CompanyMasters.Where(m => m.CompID == id).SingleOrDefault();
                Dbcontext.CompanyMasters.Remove(objCityMaster);
                Dbcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
