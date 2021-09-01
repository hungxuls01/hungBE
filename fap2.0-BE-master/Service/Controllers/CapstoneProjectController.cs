using Service.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccessLayer.DAL;

namespace Service.Controllers
{
    public class CapstoneProjectController : ApiController
    {
        private static CapstoneProjectDAL _CapstoneProjectDal;
        private static Logger _logger;
        public CapstoneProjectController()
        {
            _CapstoneProjectDal = new CapstoneProjectDAL();
            _logger = LogManager.GetCurrentClassLogger();
        }

        [HttpGet]
        public ResponeResult GetAll()
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                var data = _CapstoneProjectDal.GetAll();
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
