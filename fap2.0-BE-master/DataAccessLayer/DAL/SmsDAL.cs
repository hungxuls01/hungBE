using Entity.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace DataAccessLayer.DAL
{
    public class SmsDAL : DbInteractionBase
    {
        private string accountid = "ACb23ba5f6630cffdc4002ce8f94c0ec67";
        private string authToken = "171c8f6968b74162fd8b4cf95ce940a3";
        private string sender = "+12093539311";

        public List<string> Insert(CreateNotification notification)
        {
            List<string> _PhoneError = new List<string>();
            TwilioClient.Init(accountid, authToken);
            //var from = "+12093539311";
            string[] phone = notification.receiver.Split(',');
            var receiver = "";
            foreach (string ch in phone)
            {
                string mobileTo = "";
                if (ch.Trim().Substring(0, 1) == "0")
                {
                    mobileTo = "+84" + ch.Trim().Substring(1, ch.Length - 1) ;
                }
                else
                {
                    mobileTo = ch.Trim();
                }
                if (Regex.Match(mobileTo, @"^([\+]?84)?[1-9][0-9]{8}$").Success)
                {
                    var message = MessageResource.Create(to: mobileTo, from: sender, body: notification.contents);
                    receiver += mobileTo + ";";
                }
                else
                {
                    _PhoneError.Add(ch);
                };
            }

            var strSql = "";
            strSql += "INSERT INTO Notifications(";
            strSql += "title,receiver,theme,contents,files,userid,createDate,categoryid,campusid,receiverlist,type)";
            strSql += " VALUES (N'";
            strSql += notification.title + "',N'";
            strSql += receiver + "',N'";
            strSql += notification.theme + "',N'";
            strSql += notification.contents + "',N'";
            strSql += notification.files + "',";
            strSql += notification.userid + ",'";
            strSql += notification.createDate + "',";
            strSql += notification.categoryid + ",";
            strSql += notification.campusid + ",'";
            strSql += receiver + "',";
            strSql += notification.type + ")";
            NonQuerySql(strSql);
            return _PhoneError;
        }

    }
}