using DataAccessLayer.DAL;
using Entity.NotificationStudent;
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
    public class NotificationStudentController : ApiController
    {
        private static NotificationStudentDAL _NotificationStudentDAL;
        private static Logger _logger;
        public NotificationStudentController()
        {
            _NotificationStudentDAL = new NotificationStudentDAL();
            _logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        public ResponeResult Insert(CreateNotificationStudent objAdd)
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {

                _NotificationStudentDAL.Insert(objAdd);
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
