using DataAccessLayer.DAL;
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
    public class ModeController : ApiController
    {
        private static ModeDAL _ModeDAL;
        private static Logger _logger;
        public ModeController()
        {
            _ModeDAL = new ModeDAL();
            _logger = LogManager.GetCurrentClassLogger();
        }

        [HttpGet]
        public ResponeResult GetAll()
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                var data = _ModeDAL.GetAll();
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
