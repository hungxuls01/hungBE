using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.Notification
{
    public class ListNotification
    {
        public long RowNum { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string receiver { get; set; }
        public int userid { get; set; }
        public DateTime createDate { get; set; }
        public int type { get; set; }
        public string studentname { get; set; }
        public string DepartmentName { get; set; }
        public string CampusName { get; set; }
        public string studentEmail { get; set; }
        public bool isRead { get; set; }
    }
}