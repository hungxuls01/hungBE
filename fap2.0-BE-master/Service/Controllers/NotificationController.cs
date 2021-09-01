using DataAccessLayer.DAL;
using Entity.InboxRead;
using Entity.Notification;
using NLog;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Service.Controllers
{
    public class NotificationController : ApiController
    {
        private static NotificationDAL _NotificationDAL;
        private static SmsDAL _SmsDAL;
        private static InboxReadDAL _InboxReadDAL;
        private static StudentDAL _StudentDAL;
        private static UsersDAL _UsersDAL;
        private static Logger _logger;
        
        public NotificationController()
        {
            _NotificationDAL = new NotificationDAL();
            _SmsDAL = new SmsDAL();
            _InboxReadDAL = new InboxReadDAL();
            _StudentDAL = new StudentDAL();
            _UsersDAL = new UsersDAL();
            _logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        public ResponeResult GetPaging(SearchingNotification obj)
        {

            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                var result = _NotificationDAL.SelectPaging(obj, obj.PageIndex, obj.PageSize);
                var data = new DataTableBase<ListNotification>(result.Item2, result.Item1, obj.PageIndex + 1).Init();
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

        [HttpPost]
        public ResponeResult GetPagingByStudentid(SearchingNotification obj)
        {

            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                var result = _NotificationDAL.SelectPagingByStudentid(obj, obj.PageIndex, obj.PageSize);
                var data = new DataTableBase<ListNotification>(result.Item2, result.Item1, obj.PageIndex + 1).Init();
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

        [HttpPost]
        public ResponeResult Insert(CreateNotification objAdd)
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                objAdd.createDate = DateTime.Now;
                objAdd.campusid = 1;
                var user = _UsersDAL.GetById(objAdd.userid);
                objAdd.campusid = user.Campusid;
                if (objAdd.type == 3)
                {
                    objAdd.receiverlist = objAdd.receiver;
                    var data = _SmsDAL.Insert(objAdd);
                    var listMobileError = "";
                    if (data.Count > 0)
                    {
                       
                        foreach (var item in data)
                        {
                            listMobileError += item + ";";
                        };
                    }
                    response.Status = ConstUtil.Success;
                    response.Data = listMobileError;
                    response.Message = "Thành công !";
                    return response;
                }
                string listattachment = "";
                string listEmailSend = "";
                var listreceiver = "";
                if (objAdd.files != "")
                {
                    string[] fileList = objAdd.files.Split(';');
                    foreach (var itemfile in fileList)
                    {
                        if (itemfile != "")
                        {
                            var file = HttpContext.Current.Server.MapPath("~" + itemfile);
                            listattachment = listattachment + file + ";";
                        }
                    }
                }
                if (objAdd.isAll == 1)
                {
                    listreceiver = "ALL";
                    var listEmailAll = _StudentDAL.GetStudent();
                    foreach (var itemEmailAll in listEmailAll)
                    {
                        listEmailSend = listEmailSend + itemEmailAll.text + ";";
                    }
                }
                else
                {
                    string[] authorsList = objAdd.receiver.Split(';');
                    foreach (var item in authorsList)
                    {
                        if (item != "")
                        {
                            string[] isGr = item.Split(':');
                            if (isGr[1] == "0")
                            {
                                listreceiver = listreceiver + isGr[0].Split('@')[0] + ";";
                                listEmailSend = listEmailSend + isGr[0] + ";";
                            }
                            else
                            {
                                listreceiver = listreceiver + isGr[0] + ";";
                                var listEmailGroup = _StudentDAL.GetByGroupCode(isGr[0]);
                                foreach (var itemEmailGr in listEmailGroup)
                                {
                                    listEmailSend = listEmailSend + itemEmailGr.text + ";";
                                }
                            }

                        }
                    }
                }

                objAdd.receiver = listreceiver;
                objAdd.receiverlist = listEmailSend;

                var id = _NotificationDAL.InsertNotification(objAdd);

                string[] emailSend = listEmailSend.Split(';');
                foreach (var itemEmail in emailSend)
                {
                    if (itemEmail != "")
                    {
                        var userReceiver = _UsersDAL.GetByEmail(itemEmail);
                        var inboxRead = new CreateInboxRead();
                        inboxRead.notificationid = id;
                        inboxRead.userid = userReceiver.id;
                        _InboxReadDAL.Insert(inboxRead);
                        if (objAdd.type == 0)
                        {
                            _NotificationDAL.SendMailNet("QL Sinh Vien", itemEmail, objAdd.title, objAdd.contents, listattachment);
                        }
                    }
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
        public ResponeResult GetById(int id)
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                var data = _NotificationDAL.GetById(id);
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
        public ResponeResult CountNotificationNotRead(int id)
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                var data = _NotificationDAL.CountNotificationNotRead(id);
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
        public ResponeResult GetRelated(int categoryid, int id)
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                var data = _NotificationDAL.Related(categoryid, id);
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
        public ResponeResult Delete(int id)
        {
            var response = new ResponeResult(ConstUtil.Error, "Có lỗi xảy ra!", null);
            try
            {
                _NotificationDAL.Delete(id);
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
