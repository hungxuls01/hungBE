using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.Notification
{
    public class ViewNotification
    {
        public int id { get; set; }
        public string title { get; set; }
        public string receiver { get; set; }
        public string theme { get; set; }
        public string contents { get; set; }
        public string files { get; set; }
        public int userid { get; set; }
        public DateTime createDate { get; set; }
        public int type { get; set; }
        public bool isRemove { get; set; }
        public bool isAll { get; set; }
        public int categoryid { get; set; }
        public int campusid { get; set; }
        public string Email { get; set; }
    }
}