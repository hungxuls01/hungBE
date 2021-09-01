using Entity;
using Entity.Notification;
using Entity.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;

namespace DataAccessLayer.DAL
{
    public class NotificationDAL : DbInteractionBase
    {
        public void SendMailNet(string SenderName, string RecieverEmail, string MailTitle, string MailContent, string listattachment)
        {
            (new Thread(new ThreadStart(
                       delegate
                       {
                           var email = GetEmail();
                           string SenderEmail = email.mail.Trim();
                           string senderEmailPass = email.pass.Trim();
                           string hostmail = "smtp.gmail.com";
                           string portmail = "587";

                           var fromAddress = new MailAddress(SenderEmail, SenderName);
                           var toAddress = new MailAddress(RecieverEmail, RecieverEmail);

                           string fromPassword = senderEmailPass;
                           string subject = MailTitle;
                           string body = "<html><body>" + MailContent + "</body></html>";

                           var smtp = new SmtpClient
                           {
                               Host = hostmail,
                               Port = int.Parse(portmail),
                               EnableSsl = true,
                               UseDefaultCredentials = false,
                               DeliveryMethod = SmtpDeliveryMethod.Network,
                               Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                           };
                           var message = new MailMessage(fromAddress, toAddress)
                           {
                               IsBodyHtml = true,
                               Subject = subject,
                               Body = body,
                           };
                           if (listattachment != "")
                           {
                               string[] attachmentItem = listattachment.Split(';');
                               foreach (var item in attachmentItem)
                               {
                                   if (item != "")
                                   {
                                       Attachment attachment;
                                       attachment = new System.Net.Mail.Attachment(item);
                                       message.Attachments.Add(attachment);
                                   }
                               }
                           }
                           smtp.Send(message);
                           UpdateEmail(email.id, email.dateRun);
                       })
                 )).Start();



        }
        public void Delete(int id)
        {
            NonQuerySql("UPDATE Notifications SET  isRemove = 1 WHERE id = " + id);
            return;
        }
        public Email GetEmail()
        {
            var strSql = "";
            strSql += "SELECT TOP 1 * FROM Emails WHERE dateRun <= '" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "' AND  count < 2000 ORDER BY dateRun";
            var Emails = GetDataDataTable(strSql);
            List<Email> ListEmail = ConvertData.ConvertDataTable<Email>(Emails);
            return ListEmail.FirstOrDefault();
        }
        public void UpdateEmail(int id, DateTime dateRun)
        {
            var strSql = "";
            if (dateRun.Year == DateTime.Now.Year && dateRun.Month == DateTime.Now.Month && dateRun.Day == DateTime.Now.Day)
            {
                strSql += "UPDATE Emails SET count = count + 1 WHERE id = " + id.ToString();
            }
            else
            {
                strSql += "UPDATE Emails SET count = 1 , dateRun = '" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "' WHERE id = " + id.ToString();
            }
            NonQuerySql(strSql);
        }
        public int InsertNotification(CreateNotification notification)
        {
            var strSql = "";
            strSql += "INSERT INTO Notifications(";
            strSql += "title,receiver,theme,contents,files,userid,createDate,categoryid,campusid,receiverlist,type)";
            strSql += " VALUES (N'";
            strSql += notification.title + "',N'";
            strSql += notification.receiver + "',N'";
            strSql += notification.theme + "',N'";
            strSql += notification.contents + "',N'";
            strSql += notification.files + "',";
            strSql += notification.userid + ",'";
            strSql += notification.createDate + "',";
            strSql += notification.categoryid + ",";
            strSql += notification.campusid + ",'";
            strSql += notification.receiverlist + "',";
            strSql += notification.type + "); SELECT CAST(scope_identity() AS int)";
            return ScalarSql(strSql);
        }

        public Tuple<int, List<ListNotification>> SelectPaging(SearchingNotification obj, int pageIndex, int pageSize)
        {
            var strSql = "";
            strSql += "SELECT ROW_NUMBER() OVER(ORDER BY no.id desc) AS RowNum,";
            strSql += " no.id,no.title,no.receiver,no.userid,no.createDate,no.type,'' as studentname, dt.name as DepartmentName, cp.name as CampusName,us.email as studentEmail";
            strSql += " INTO #Results FROM Notifications no ";
            strSql += " INNER JOIN Users us ON us.id = no.userid";
            strSql += " INNER JOIN Departments dt ON dt.id = us.Departmentid";
            strSql += " INNER JOIN Campus cp ON cp.id = no.campusid" +
           " WHERE no.isRemove = 0 ";
            if (obj.types != -1)
            {
                strSql += "AND no.type = " + obj.types;
            }
            if (obj.categoryid != -1)
            {
                strSql += "AND no.categoryid = " + obj.categoryid;
            }
            if (obj.Departmentid != -1)
            {
                strSql += "AND us.Departmentid = " + obj.Departmentid;
            }
            if (obj.Keyword != "")
            {
                strSql += "AND no.title like N'%" + obj.Keyword + "%'";
            }
            if (obj.receiver != "")
            {
                strSql += "AND no.receiverlist like '%" + obj.receiver + ";%'";
            }
            if (obj.createDate != "")
            {
                strSql += "AND YEAR(no.createDate) = YEAR(CONVERT(varchar, '" + obj.createDate + "', 101)) AND MONTH(no.createDate) = MONTH(CONVERT(varchar, '" + obj.createDate + "', 101)) AND DAY(no.createDate) = DAY(CONVERT(varchar, '" + obj.createDate + "', 101))";
            }
            strSql += " ORDER BY id desc ;";

            strSql += " SELECT COUNT(*) as count  FROM #Results;";
            strSql += " SELECT * FROM #Results   WHERE (RowNum > " + pageIndex * pageSize + ") AND (RowNum <= " + (pageIndex + 1) * pageSize + ")";
            strSql += " DROP TABLE #Results;";
            var GetNotifications = GetDataDataTablePaging(strSql);
            List<NumberElements> counts = ConvertData.ConvertDataTable<NumberElements>(GetNotifications.Tables[0]);
            List<ListNotification> Notifications = ConvertData.ConvertDataTable<ListNotification>(GetNotifications.Tables[1]);
            return Tuple.Create(counts.FirstOrDefault().count, Notifications); ;
        }

        public Tuple<int, List<ListNotification>> SelectPagingByStudentid(SearchingNotification obj, int pageIndex, int pageSize)
        {
            var strSql = "";
            strSql += "SELECT";
            strSql += " no.id,no.title,no.receiver,no.userid,no.createDate,no.type,'' as studentname, dt.name as DepartmentName, cp.name as CampusName,ir.isRead ";
            strSql += " INTO #Result FROM Notifications no ";
            strSql += " INNER JOIN Users us ON us.id = no.userid";
            strSql += " INNER JOIN Departments dt ON dt.id = us.Departmentid";
            strSql += " INNER JOIN Campus cp ON cp.id = no.campusid";
            strSql += " LEFT JOIN NotificationStudent nst ON nst.notificationid = no.id";
            strSql += " LEFT JOIN CategoryStudent cs ON nst.categoryid = cs.id";
            strSql += " INNER JOIN InboxRead ir ON ir.notificationid = no.id AND ir.userid = " + obj.userid +
           " WHERE no.isRemove = 0 AND ir.userid = " + obj.userid;
            if (obj.types != -1)
            {
                strSql += "AND no.type = " + obj.types;
            }
            if (obj.categoryid != -1)
            {
                strSql += "AND no.categoryid = " + obj.categoryid;
            }
            if (obj.categoryStudentid != -1)
            {
                strSql += "AND nst.categoryid = " + obj.categoryStudentid;
            }
            if (obj.Keyword != "")
            {
                strSql += "AND no.title like N'%" + obj.Keyword + "%'";
            }
            if (obj.receiver != "")
            {
                strSql += "AND no.receiverlist like '%" + obj.receiver + ";%'";
            }
            strSql += " GROUP BY no.id,no.title,no.receiver,no.userid,no.createDate,no.type, dt.name, cp.name,ir.isRead";
            strSql += " ORDER BY id desc ;";

            strSql += " SELECT ROW_NUMBER() OVER(ORDER BY id desc) AS RowNum,* INTO #Results FROM #Result;";
            strSql += " SELECT COUNT(*) as count  FROM #Results;";
            strSql += " SELECT * FROM #Results   WHERE (RowNum > " + pageIndex * pageSize + ") AND (RowNum <= " + (pageIndex + 1) * pageSize + ")";
            strSql += " DROP TABLE #Result;";
            strSql += " DROP TABLE #Results;";
            var GetNotifications = GetDataDataTablePaging(strSql);
            List<NumberElements> counts = ConvertData.ConvertDataTable<NumberElements>(GetNotifications.Tables[0]);
            List<ListNotification> Notifications = ConvertData.ConvertDataTable<ListNotification>(GetNotifications.Tables[1]);
            return Tuple.Create(counts.FirstOrDefault().count, Notifications);
        }

        public int CountNotificationNotRead(int studentid)
        {
            var strSql = "";
            strSql += "SELECT COUNT(*) as count";
            strSql += " FROM Notifications no ";
            strSql += " INNER JOIN Users us ON us.id = no.userid";
            strSql += " INNER JOIN Departments dt ON dt.id = us.Departmentid";
            strSql += " INNER JOIN Campus cp ON cp.id = no.campusid";
            strSql += " LEFT JOIN NotificationStudent nst ON nst.notificationid = no.id";
            strSql += " LEFT JOIN CategoryStudent cs ON nst.categoryid = cs.id";
            strSql += " INNER JOIN InboxRead ir ON ir.notificationid = no.id" +
           " WHERE no.isRemove = 0 AND no.type = 1 AND ir.isRead = 0  AND ir.userid = " + studentid;
            var GetNotifications = GetDataDataTable(strSql);
            List<NumberElements> counts = ConvertData.ConvertDataTable<NumberElements>(GetNotifications);
            return counts.FirstOrDefault().count;
        }

        public List<ListNotification> Related(int categoryid, int id)
        {
            var strSql = "";
            strSql += "SELECT TOP 5 ROW_NUMBER() OVER(ORDER BY no.id desc) AS RowNum,";
            strSql += " no.id,no.title,no.receiver,no.userid,no.createDate,no.type,'' as studentname, dt.name as DepartmentName, cp.name as CampusName";
            strSql += " FROM Notifications no ";
            strSql += " INNER JOIN Users us ON us.id = no.userid";
            strSql += " INNER JOIN Departments dt ON dt.id = us.Departmentid";
            strSql += " INNER JOIN Campus cp ON cp.id = no.campusid" +
           " WHERE no.categoryid = " + categoryid + " AND no.id <> " + id;
            strSql += " ORDER BY id desc ;";
            var GetNotifications = GetDataDataTable(strSql);
            List<ListNotification> Notifications = ConvertData.ConvertDataTable<ListNotification>(GetNotifications);
            return Notifications;
        }

        public ViewNotification GetById(int id)
        {
            var GetNotifications = GetDataDataTable("SELECT no.*,"
                 + "us.email as Email"
                + " FROM Notifications no "
                + " INNER JOIN Users us ON us.id = no.userid"
            + " WHERE no.id = " + id);
            List<ViewNotification> Notifications = ConvertData.ConvertDataTable<ViewNotification>(GetNotifications);
            return Notifications.FirstOrDefault();
        }

    }
}