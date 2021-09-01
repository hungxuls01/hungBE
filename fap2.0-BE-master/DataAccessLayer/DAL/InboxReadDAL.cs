using Entity.InboxRead;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.DAL
{
    public class InboxReadDAL : DbInteractionBase
    {
        public void Insert(CreateInboxRead inbox)
        {
            var strSql = "";
            strSql += "INSERT INTO InboxRead(";
            strSql += "notificationid,userid)";
            strSql += " VALUES (";
            strSql += inbox.notificationid + ",";
            strSql += inbox.userid + ")";
            NonQuerySql(strSql);
        }
        public void UpdateRead(CreateInboxRead inbox)
        {
            var strSql = "";
            strSql += "UPDATE InboxRead SET ";
            strSql += "isRead = 1";
            strSql += " WHERE notificationid = " + inbox.notificationid + " AND userid = " + inbox.userid;
            NonQuerySql(strSql);
        }
    }
}