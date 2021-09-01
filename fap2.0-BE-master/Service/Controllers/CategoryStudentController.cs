using DataAccessLayer.DAL;
using Entity.CategoryStudent;
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
    public class CategoryStudentController : ApiController
    {
        private static CategoryStudentDAL _CategoryStudentDAL;
        private static Logger _logger;
        public CategoryStudentController()
        {
            _CategoryStudentDAL = new CategoryStudentDAL();
            _logger = LogManager.GetCurrentClassLogger();
        }

        [HttpGet]
        public ResponeResult GetByStudentid(int id)
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                var data = _CategoryStudentDAL.GetByStudentid(id);
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
        public ResponeResult Insert(CreateCategoryStudent objAdd)
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {

                _CategoryStudentDAL.Insert(objAdd);
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
