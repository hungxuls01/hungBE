using DataAccessLayer.DAL;
using Entity.Decision;
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
    public class DecisionController : ApiController
    {
        private static NotificationDAL _NotificationDAL;
        private static DecisionDAL _DecisionDAL;
        private static StudentDAL _StudentDAL;
        private static UsersDAL _UsersDAL;
        private static Logger _logger;
        public DecisionController()
        {
            _NotificationDAL = new NotificationDAL();
            _DecisionDAL = new DecisionDAL();
            _StudentDAL = new StudentDAL();
            _UsersDAL = new UsersDAL();
            _logger = LogManager.GetCurrentClassLogger();
        }

        [HttpGet]
        public ResponeResult GetAll()
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                var data = _DecisionDAL.GetAll();
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
        public ResponeResult Insert(CreateDecision objAdd)
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                objAdd.create_date = DateTime.Now;
                _DecisionDAL.InsertDecision(objAdd);
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

        [HttpPost]
        public ResponeResult Accept(AcceptDecision obj)
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                var Decision = _DecisionDAL.GetById(obj.id);
                var user = _UsersDAL.GetById(Decision.userid);
                var student = _StudentDAL.GetByUserid(Decision.userid);
                var isEmail = true;
                if (obj.status == 4)
                {
                    var userFW = _UsersDAL.CheckEmail(obj.emailForward);
                    if (userFW.roleid == 1)
                    {
                        isEmail = false;
                        response.Status = ConstUtil.Error;
                        response.Message = "Email has no permissions!";
                        return response;
                    }
                    if (user.Departmentid != userFW.Departmentid)
                    {
                        isEmail = false;
                        response.Status = ConstUtil.Error;
                        response.Message = "Email not in the same department!";
                        return response;
                    }
                }
                _DecisionDAL.AcceptDecision(obj);
                if (obj.status == 4 && isEmail)
                {
                    _NotificationDAL.SendMailNet("QL Sinh Vien", obj.emailForward, "Forward Application", "Bạn có yêu cầu cần xử lý. <br>" +
                        "Hãy đăng nhập vào FAP xử lý thông tin <b style='color:blue'>Các loại đơn khác</b> cho sinh viên: <b>" + student.RollNumber + "</b>", "");
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

        [HttpPost]
        public ResponeResult GetPaging(SearchingDecision obj)
        {

            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                var result = _DecisionDAL.SelectPaging(obj, obj.PageIndex, obj.PageSize);
                var data = new DataTableBase<ListDecision>(result.Item2, result.Item1, obj.PageIndex + 1).Init();
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
                var data = _DecisionDAL.GetById(id);
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

