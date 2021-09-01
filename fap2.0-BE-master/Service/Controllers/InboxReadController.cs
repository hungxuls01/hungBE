using DataAccessLayer.DAL;
using Entity.InboxRead;
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
    public class InboxReadController : ApiController
    {
        private static InboxReadDAL _InboxReadDAL;
        private static Logger _logger;
        public InboxReadController()
        {
            _InboxReadDAL = new InboxReadDAL();
            _logger = LogManager.GetCurrentClassLogger();
        }
        [HttpPost]
        public ResponeResult UpdateRead(CreateInboxRead objAdd)
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {

                _InboxReadDAL.UpdateRead(objAdd);
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
