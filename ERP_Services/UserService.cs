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
    public class UserService
    {
        ERPEntities Dbcontext = new ERPEntities();
        public List<UserModel> getUser(int rid, int cid)
        {
            try
            {
                //Mapper.CreateMap<UserMaster, UserModel>();
                //List<UserMaster> objCityMaster = Dbcontext.UserMasters.Where(m => m.CompID == (rid == 1 ? m.CompID : cid)).ToList();
                //List<UserModel> objCityItem = Mapper.Map<List<UserModel>>(objCityMaster);
                //return objCityItem;

                var data = (from um in Dbcontext.UserMasters
                            join cm in Dbcontext.CompanyMasters on um.CompID equals cm.CompID
                            join rm in Dbcontext.RoleMasters on um.RoleID equals rm.RID
                            where um.CompID == (rid == 1 ? um.CompID : cid)
                            select new UserModel()
                            {
                                UID = um.UID,
                                CompID = um.CompID,
                                RoleID = um.RoleID,
                                Title = um.Title,
                                FirstName = um.FirstName,
                                LastName = um.LastName,
                                UserName = um.UserName,
                                Password = um.Password,
                                Mobile = um.Mobile,
                                Email = um.Email,
                                Address = um.Address,
                                City = um.City,
                                PostCode = um.PostCode,
                                Country = um.Country,
                                Status = um.Status,
                                CanLogin = um.CanLogin,
                                CreatedBy = um.CreatedBy,
                                CreatedDate = um.CreatedDate,
                                UpdatedBy = um.UpdatedBy,
                                UpdatedDate = um.UpdatedDate,
                                CompDetails = new CompanyModel()
                                {
                                    CompName = cm.CompName
                                },
                                RoleDetails = new RoleModel()
                                {
                                    Role=rm.Role
                                }
                            }).ToList();
                return data;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
        public List<AccountingYearModel> getAcYr()
        {
            try
            {
                Mapper.CreateMap<AccountingYear, AccountingYearModel>();
                List<AccountingYear> objCityMaster = Dbcontext.AccountingYears.Where(m => m.Status == true).ToList();
                List<AccountingYearModel> objCityItem = Mapper.Map<List<AccountingYearModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CompanyModel> getActiveComp(int rid, int cid)
        {
            try
            {
                Mapper.CreateMap<CompanyMaster, CompanyModel>();
                List<CompanyMaster> objCityMaster = Dbcontext.CompanyMasters.Where(m => m.Status == true && m.PCompID == (rid == 1 ? m.PCompID : cid) || m.CompID == (rid == 1 ? m.CompID : cid)).ToList();
                List<CompanyModel> objCityItem = Mapper.Map<List<CompanyModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<RoleModel> getActiveRole()
        {
            try
            {
                Mapper.CreateMap<RoleMaster, RoleModel>();
                List<RoleMaster> objCityMaster = Dbcontext.RoleMasters.Where(m => m.Status == true && m.RID != 1).ToList();
                List<RoleModel> objCityItem = Mapper.Map<List<RoleModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Insert(UserModel model)
        {
            try
            {
                Mapper.CreateMap<UserModel, UserMaster>();
                UserMaster objUser = Mapper.Map<UserMaster>(model);
                Dbcontext.UserMasters.Add(objUser);
                Dbcontext.SaveChanges();

                int uid = Dbcontext.UserMasters.Max(m => m.UID);
                return uid;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public UserModel getByID(int id)
        {
            try
            {
                Mapper.CreateMap<UserMaster, UserModel>();
                UserMaster objCityMaster = Dbcontext.UserMasters.Where(m => m.UID == id).SingleOrDefault();
                UserModel objCityItem = Mapper.Map<UserModel>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Update(UserModel model)
        {
            Mapper.CreateMap<UserModel, UserMaster>();
            UserMaster objUser = Dbcontext.UserMasters.SingleOrDefault(m => m.UID == model.UID);
            objUser = Mapper.Map(model, objUser);
            return Dbcontext.SaveChanges();
        }

        public bool DeleteByID(int id)
        {
            try
            {
                Mapper.CreateMap<UserMaster, UserModel>();
                UserMaster objCityMaster = Dbcontext.UserMasters.Where(m => m.UID == id).SingleOrDefault();
                Dbcontext.UserMasters.Remove(objCityMaster);
                Dbcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public UserModel GetAuthUser(string username, string mobile)
        {
            Mapper.CreateMap<UserMaster, UserModel>();
            UserMaster objComp = Dbcontext.UserMasters.SingleOrDefault(m => m.UserName == username && m.Password == mobile);
            UserModel objCItem = Mapper.Map<UserModel>(objComp);
            return objCItem;
        }
        public List<FormModel> getForms()
        {
            try
            {
                //Mapper.CreateMap<FormMaster, FormModel>();
                //List<FormMaster> objCityMaster = Dbcontext.FormMasters.Where(m => m.Status == true).OrderBy(m => m.Sequence).ToList();
                //List<FormModel> objCityItem = Mapper.Map<List<FormModel>>(objCityMaster);
                //return objCityItem;

                var data = (from fm in Dbcontext.FormMasters
                            join mm in Dbcontext.ModuleMasters on fm.MID equals mm.MID
                            where fm.Status==true && mm.Status==true
                            select new FormModel()
                            {
                                FID = fm.FID,
                                MID = fm.MID,
                                FormName = fm.FormName,
                                URL = fm.URL,
                                Description = fm.Description,
                                Sequence = fm.Sequence,
                                Status = fm.Status,
                                ModuleDetails = new ModuleModel()
                                {
                                   MID=mm.MID,
                                   Module=mm.Module,
                                   Description=mm.Description,
                                   Icon=mm.Icon,
                                   Sequence=mm.Sequence,
                                   Status=mm.Status
                                }
                            }).OrderBy(m => m.Sequence).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ModuleModel> getModules()
        {
            try
            {
                Mapper.CreateMap<ModuleMaster, ModuleModel>();
                List<ModuleMaster> objCityMaster = Dbcontext.ModuleMasters.Where(m => m.Status == true).OrderBy(m => m.Sequence).ToList();
                List<ModuleModel> objCityItem = Mapper.Map<List<ModuleModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<UserFormModel> getUserForms(int uid)
        {
            try
            {
                //Mapper.CreateMap<UserFormTran, UserFormModel>();
                //List<UserFormTran> objCityMaster = Dbcontext.UserFormTrans.Where(m => m.UID == uid).ToList();
                //List<UserFormModel> objCityItem = Mapper.Map<List<UserFormModel>>(objCityMaster);
                //return objCityItem;

                var data = (from uf in Dbcontext.UserFormTrans
                            join fm in Dbcontext.FormMasters on uf.FID equals fm.FID
                            where uf.UID==uid
                            select new UserFormModel()
                            {
                                FID = fm.FID,
                                UFTID=uf.UFTID,
                                UID=uf.UID,
                                IsChecked=uf.IsChecked,
                                AssignedBy=uf.AssignedBy,
                                AssignedDate=uf.AssignedDate,
                                UpdatedBy=uf.UpdatedBy,
                                UpdatedDate=uf.UpdatedDate,
                                FormDetails = new FormModel()
                                {
                                    MID = fm.MID,
                                    FID=fm.FID,
                                    Description = fm.Description,
                                    FormName=fm.FormName,
                                    URL=fm.URL,
                                    Sequence=fm.Sequence,
                                    Status = fm.Status
                                }
                            }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<UserModuleModel> getUserModules(int uid)
        {
            try
            {
                //Mapper.CreateMap<UserModuleTran, UserModuleModel>();
                //List<UserModuleTran> objCityMaster = Dbcontext.UserModuleTrans.Where(m => m.UID== uid).ToList();
                //List<UserModuleModel> objCityItem = Mapper.Map<List<UserModuleModel>>(objCityMaster);
                //return objCityItem;

                var data = (from um in Dbcontext.UserModuleTrans
                            join mm in Dbcontext.ModuleMasters on um.MID equals mm.MID
                            where um.UID==uid
                            select new UserModuleModel()
                            {
                                UMTID = um.UMTID,
                                UID = um.UID,
                                MID = um.MID,
                                IsChecked = um.IsChecked,
                                AssignedBy = um.AssignedBy,
                                AssignedDate = um.AssignedDate,
                                UpdatedBy = um.UpdatedBy,
                                UpdatedDate = um.UpdatedDate,
                                ModuleDetail = new ModuleModel()
                                {
                                    MID=mm.MID,
                                    Module=mm.Module,
                                    Description=mm.Description,
                                    Icon=mm.Icon,
                                    Sequence=mm.Sequence,
                                    Status=mm.Status
                                }
                            }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<UserModuleModel> getUserModules4Display(int uid)
        {
            try
            {
                //Mapper.CreateMap<UserModuleTran, UserModuleModel>();
                //List<UserModuleTran> objCityMaster = Dbcontext.UserModuleTrans.Where(m => m.UID== uid).ToList();
                //List<UserModuleModel> objCityItem = Mapper.Map<List<UserModuleModel>>(objCityMaster);
                //return objCityItem;

                var data = (from um in Dbcontext.UserModuleTrans
                            join mm in Dbcontext.ModuleMasters on um.MID equals mm.MID
                            where um.UID == uid && um.IsChecked == true
                            select new UserModuleModel()
                            {
                                UMTID = um.UMTID,
                                UID = um.UID,
                                MID = um.MID,
                                IsChecked = um.IsChecked,
                                AssignedBy = um.AssignedBy,
                                AssignedDate = um.AssignedDate,
                                UpdatedBy = um.UpdatedBy,
                                UpdatedDate = um.UpdatedDate,
                                ModuleDetail = new ModuleModel()
                                {
                                    MID = mm.MID,
                                    Module = mm.Module,
                                    Description = mm.Description,
                                    Icon = mm.Icon,
                                    Sequence = mm.Sequence,
                                    Status = mm.Status
                                }
                            }).OrderBy(m => m.ModuleDetail.Sequence).ToList();
                return data;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //    var data = (from mm in Dbcontext.ModuleMasters
            //                join um in Dbcontext.UserModuleTrans on mm.MID equals um.MID
            //                where um.UID == uid && um.IsChecked == true
            //                select new UserModuleModel()
            //                {
            //                    ModuleDetail = new ModuleModel()
            //                    {
            //                        MID = mm.MID,
            //                        Module = mm.Module,
            //                        Description = mm.Description,
            //                        Icon = mm.Icon,
            //                        Sequence = mm.Sequence,
            //                        Status = mm.Status
            //                    }
            //                }).OrderBy(m=>m.ModuleDetail.Sequence).ToList();
            //    return data;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        public List<UserFormModel> getUserFormsByModule(int uid,int? mid)
        {
            try
            {
                //Mapper.CreateMap<UserFormTran, UserFormModel>();
                //List<UserFormTran> objCityMaster = Dbcontext.UserFormTrans.Where(m => m.UID == uid).ToList();
                //List<UserFormModel> objCityItem = Mapper.Map<List<UserFormModel>>(objCityMaster);
                //return objCityItem;

                var data = (from uf in Dbcontext.UserFormTrans
                            join fm in Dbcontext.FormMasters on uf.FID equals fm.FID
                            where uf.UID == uid && fm.MID == mid && uf.IsChecked == true
                            select new UserFormModel()
                            {
                                FID = fm.FID,
                                UFTID = uf.UFTID,
                                UID = uf.UID,
                                IsChecked = uf.IsChecked,
                                AssignedBy = uf.AssignedBy,
                                AssignedDate = uf.AssignedDate,
                                UpdatedBy = uf.UpdatedBy,
                                UpdatedDate = uf.UpdatedDate,
                                FormDetails = new FormModel()
                                {
                                    MID = fm.MID,
                                    FID = fm.FID,
                                    Description = fm.Description,
                                    FormName = fm.FormName,
                                    URL = fm.URL,
                                    Sequence = fm.Sequence,
                                    Status = fm.Status
                                }
                            }).OrderBy(m=>m.FormDetails.Sequence).ToList();
                return data;
                //var data = (from fm in Dbcontext.FormMasters
                //            join uf in Dbcontext.UserFormTrans on fm.FID equals uf.FID
                //            where uf.UID == uid && uf.IsChecked == true
                //            select new UserFormModel()
                //            {
                //                FormDetails = new FormModel()
                //                                {
                //                                    MID = fm.MID,
                //                                    FID = fm.FID,
                //                                    Description = fm.Description,
                //                                    FormName = fm.FormName,
                //                                    URL = fm.URL,
                //                                    Sequence = fm.Sequence,
                //                                    Status = fm.Status
                //                                }
                //            }).OrderBy(m=>m.FormDetails.Sequence).ToList();
                //return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertUserModule(UserModuleModel model)
        {
            try
            {
                Mapper.CreateMap<UserModuleModel, UserModuleTran>();
                UserModuleTran objUser = Mapper.Map<UserModuleTran>(model);
                Dbcontext.UserModuleTrans.Add(objUser);
                return Dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int InsertUserForm(UserFormModel model)
        {
            try
            {
                Mapper.CreateMap<UserFormModel, UserFormTran>();
                UserFormTran objUser = Mapper.Map<UserFormTran>(model);
                Dbcontext.UserFormTrans.Add(objUser);
                return Dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
