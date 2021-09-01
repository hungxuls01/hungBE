using DataAccessLayer.DAL;
using Entity.Student;
using Entity.User;
using NLog;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Service.Controllers
{
    public class StudentsController : ApiController
    {
        private static StudentDAL _StudentDAL;
        private static UsersDAL _UsersDAL;
        private static Logger _logger;
        public StudentsController()
        {
            _StudentDAL = new StudentDAL();
            _UsersDAL = new UsersDAL();
            _logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        public ResponeResult GetPaging(SearchingStudent obj)
        {

            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                var result = _StudentDAL.SelectPaging(obj, obj.PageIndex, obj.PageSize);
                var data = new DataTableBase<ListStudent>(result.Item2, result.Item1, obj.PageIndex + 1).Init();
                response.Status = ConstUtil.Success;
                response.Message = "Thành công !";
                response.Data = data;
                return response;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                response.Status = ConstUtil.Error;
                response.Message = "Có lỗi sảy ra !";
                return response;
            }
        }

        [HttpGet]
        public ResponeResult GetById(int id)
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                var data = _StudentDAL.GetById(id);
                if (data != null)
                {
                    response.Status = ConstUtil.Success;
                    response.Message = "Thành công !";
                    response.Data = data;
                    return response;
                }
                response.Status = ConstUtil.Error;
                response.Message = "Không tìm thấy dữ liệu!";
                return response;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                response.Status = ConstUtil.Error;
                response.Message = "Có lỗi sảy ra !";
                return response;
            }
        }

        [HttpGet]
        public ResponeResult GetCombobox()
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                var data = _StudentDAL.GetCombobox();
                if (data != null)
                {
                    response.Status = ConstUtil.Success;
                    response.Message = "Thành công !";
                    response.Data = data;
                    return response;
                }
                response.Status = ConstUtil.Error;
                response.Message = "Không tìm thấy dữ liệu!";
                return response;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                response.Status = ConstUtil.Error;
                response.Message = "Có lỗi sảy ra !";
                return response;
            }
        }

        [HttpGet]
        public ResponeResult GetByEmail(string id, int Campusid)
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                var data = _StudentDAL.GetByEmail(id);
                if (data == null)
                {
                    response.Status = ConstUtil.Error;
                    response.Message = "Tài khoản không tồn tại !";
                    return response;
                }
                else
                {
                    if (data.Campusid != Campusid)
                    {
                        response.Status = ConstUtil.Error;
                        response.Message = "Tài khoản không thuộc Campus này!";
                        return response;
                    }
                    if (data != null)
                    {
                        response.Status = ConstUtil.Success;
                        response.Message = "Thành công !";
                        response.Data = data;
                        return response;
                    }
                    response.Status = ConstUtil.Error;
                    response.Message = "Tài khoản không tồn tại !";
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                response.Status = ConstUtil.Error;
                response.Message = "Có lỗi sảy ra !";
                return response;
            }
        }


        [HttpGet]
        public ResponeResult CheckEmail(string id)
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                var data = _StudentDAL.CheckEmail(id);
                if (data != null)
                {
                    response.Status = ConstUtil.Success;
                    response.Message = "Thành công !";
                    response.Data = data;
                    return response;
                }
                response.Status = ConstUtil.Error;
                response.Message = "Không tìm thấy dữ liệu!";
                return response;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                response.Status = ConstUtil.Error;
                response.Message = "Có lỗi sảy ra !";
                return response;
            }
        }

        [HttpPost]
        public ResponeResult InsertOrUpdate(CreateStudent objAdd)
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {

                // Get Account 
                var Account = ConvertUtil.RemoveUnicode(objAdd.LastName.ToLower()) + ConvertUtil.RemoveUnicode(objAdd.FirstName.ToLower()).Substring(0, 1) + ConvertUtil.RemoveUnicode(objAdd.MiddleName.ToLower()).Substring(0, 1) + objAdd.RollNumber.ToLower();
                objAdd.Account = Account;

                if (objAdd.id == 0)
                {
                    if (objAdd.RollNumber != "")
                    {
                        var StudentGetByRollNumber = _StudentDAL.GetByRollNumber(objAdd.RollNumber);
                        if (StudentGetByRollNumber != null)
                        {
                            response.Status = ConstUtil.Error;
                            response.Message = "RollNumber already exist !";
                            return response;
                        }
                    }
                    if (objAdd.MobilePhone != "")
                    {
                        var StudentGetByMobilePhone = _StudentDAL.GetByMobilePhone(objAdd.MobilePhone);
                        if (StudentGetByMobilePhone != null)
                        {
                            response.Status = ConstUtil.Error;
                            response.Message = "MobilePhone already exist !";
                            return response;
                        }
                    }
                    if (objAdd.IDCard != "")
                    {
                        var StudentGetByIDCard = _StudentDAL.GetByIDCard(objAdd.IDCard);
                        if (StudentGetByIDCard != null)
                        {
                            response.Status = ConstUtil.Error;
                            response.Message = "IDCard already exist !";
                            return response;
                        }
                    }
                    if (objAdd.Email != "")
                    {
                        var StudentGetByEmail = _UsersDAL.CheckEmail(objAdd.Email);
                        if (StudentGetByEmail != null)
                        {
                            response.Status = ConstUtil.Error;
                            response.Message = "Email already exist !";
                            return response;
                        }
                    }
                    objAdd.Campusid = 1;
                    objAdd.Departmentid = 1;
                    var user = new CreateUser();
                    user.Departmentid = (int)objAdd.Departmentid;
                    user.Campusid = (int)objAdd.Campusid;
                    user.email = objAdd.Email;
                    user.isActive = true;
                    user.roleid = 1;
                    var userid = _UsersDAL.InsertUser(user);
                    objAdd.userid = userid;
                    _StudentDAL.InsertStudent(objAdd);
                }
                else
                {
                    var student = _StudentDAL.GetById(objAdd.id);
                    if (objAdd.RollNumber != "" && objAdd.RollNumber != student.RollNumber)
                    {
                        var StudentGetByRollNumber = _StudentDAL.GetByRollNumber(objAdd.RollNumber);
                        if (StudentGetByRollNumber != null)
                        {
                            response.Status = ConstUtil.Error;
                            response.Message = "RollNumber already exist !";
                            return response;
                        }
                        objAdd.OldRollNumber = student.RollNumber;
                    }
                    if (objAdd.Specializedid > 0 && objAdd.Specializedid != student.Specializedid)
                    {
                        objAdd.SpecializedOldid = student.Specializedid;
                    }
                    if (objAdd.MobilePhone != "" && objAdd.MobilePhone != student.MobilePhone)
                    {
                        var StudentGetByMobilePhone = _StudentDAL.GetByMobilePhone(objAdd.MobilePhone);
                        if (StudentGetByMobilePhone != null)
                        {
                            response.Status = ConstUtil.Error;
                            response.Message = "MobilePhone already exist !";
                            return response;
                        }
                    }
                    if (objAdd.IDCard != "" && objAdd.IDCard != student.IDCard)
                    {
                        var StudentGetByIDCard = _StudentDAL.GetByIDCard(objAdd.IDCard);
                        if (StudentGetByIDCard != null)
                        {
                            response.Status = ConstUtil.Error;
                            response.Message = "IDCard already exist !";
                            return response;
                        }
                    }
                    if (objAdd.Email != "" && objAdd.Email != student.Email)
                    {
                        var StudentGetByEmail = _UsersDAL.CheckEmail(objAdd.Email);
                        if (StudentGetByEmail != null)
                        {
                            response.Status = ConstUtil.Error;
                            response.Message = "Email already exist !";
                            return response;
                        }
                        var user = new CreateUser();
                        user.id = (int)objAdd.userid;
                        user.Departmentid = (int)objAdd.Departmentid;
                        user.Campusid = (int)objAdd.Campusid;
                        user.email = objAdd.Email;
                        user.isActive = true;
                        user.roleid = 1;
                        _UsersDAL.UpdateUser(user);
                    }
                    _StudentDAL.UpdateStudent(objAdd);
                }
                response.Status = ConstUtil.Success;
                response.Message = "Thành công !";
                return response;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                response.Status = ConstUtil.Error;
                response.Message = "Có lỗi sảy ra !";
                return response;
            }
        }

        [HttpGet]
        public ResponeResult Delete(int id)
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                _StudentDAL.Delete(id);
                response.Status = ConstUtil.Success;
                response.Message = "Thành công !";
                return response;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                response.Status = ConstUtil.Error;
                response.Message = "Có lỗi sảy ra !";
                return response;
            }
        }

        public ResponeResult StudentImport()
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                _StudentDAL.ImportStudentByExcel();
                //_StudentDAL.UpdateStudentByExcel();
                response.Status = ConstUtil.Success;
                response.Message = "Thành công !";
                return response;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                response.Status = ConstUtil.Error;
                response.Message = "Có lỗi sảy ra !";
                return response;
            }
        }

        public ResponeResult StudentUpdateImport()
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                //_StudentDAL.ImportStudentByExcel();
                _StudentDAL.UpdateStudentByExcel();
                response.Status = ConstUtil.Success;
                response.Message = "Thành công !";
                return response;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                response.Status = ConstUtil.Error;
                response.Message = "Có lỗi sảy ra !";
                return response;
            }
        }
    }
}
