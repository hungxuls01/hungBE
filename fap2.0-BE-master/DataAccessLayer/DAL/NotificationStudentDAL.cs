using Entity.NotificationStudent;
using Entity.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.DAL
{
    public class NotificationStudentDAL : DbInteractionBase
    {
        public int GetAll()
        {
            var Getdata = GetDataDataTable("SELECT COUNT(*) as count FROM NotificationStudent");
            List<NumberElements> counts = ConvertData.ConvertDataTable<NumberElements>(Getdata);
            return counts.FirstOrDefault().count;
        }

        public void Insert(CreateNotificationStudent student)
        {
            var strSql = "";
            strSql += "INSERT INTO NotificationStudent(";
            strSql += "categoryid,notificationid)";
            strSql += " VALUES (";
            strSql += student.categoryid + ",";
            strSql += student.notificationid + ")";
            NonQuerySql(strSql);
        }
    }
}