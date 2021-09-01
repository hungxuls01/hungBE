using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.Notification
{
    public class SearchingNotification : SearchBase
    {
        public int types { get; set; }
        public int categoryid { get; set; }
        public string receiver { get; set; }
        public int userid { get; set; }
        public int categoryStudentid { get; set; }
        public int Departmentid { get; set; }
        public string createDate { get; set; }
    }
}