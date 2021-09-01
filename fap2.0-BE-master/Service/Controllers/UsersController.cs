using DataAccessLayer.DAL;
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
    public class UsersController : ApiController
    {
        private static UsersDAL _UsersDAL;
        private static Logger _logger;
        public UsersController()
        {
            _UsersDAL = new UsersDAL();
            _logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        public ResponeResult GetPaging(SearchingUser obj)
        {

            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                var result = _UsersDAL.SelectPaging(obj, obj.PageIndex, obj.PageSize);
                var data = new DataTableBase<ListUser>(result.Item2, result.Item1, obj.PageIndex + 1).Init();
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
                var data = _UsersDAL.GetById(id);
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
                var data = _UsersDAL.GetByEmail(id);
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
                    if (!data.isActive)
                    {
                        response.Status = ConstUtil.Error;
                        response.Message = "Tài khoản đã bị khóa!";
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

        [HttpPost]
        public ResponeResult InsertOrUpdate(CreateUser objAdd)
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {

                if (objAdd.id == 0)
                {
                    if (objAdd.email != "")
                    {
                        var StudentGetByEmail = _UsersDAL.CheckEmail(objAdd.email);
                        if (StudentGetByEmail != null)
                        {
                            response.Status = ConstUtil.Error;
                            response.Message = "Email already exist !";
                            return response;
                        }
                    }
                    _UsersDAL.InsertUser(objAdd);
                }
                else
                {
                    var student = _UsersDAL.GetById(objAdd.id);
                    if (objAdd.email != "" && objAdd.email != student.email)
                    {
                        var StudentGetByEmail = _UsersDAL.CheckEmail(objAdd.email);
                        if (StudentGetByEmail != null)
                        {
                            response.Status = ConstUtil.Error;
                            response.Message = "Email already exist !";
                            return response;
                        }
                    }
                    _UsersDAL.UpdateUser(objAdd);
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
        public ResponeResult GetAllRole()
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                var data = _UsersDAL.GetAllRole();
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
    }
}
