using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.InboxRead
{
    public class CreateInboxRead
    {
        public int id { get; set; }
        public int notificationid { get; set; }
        public int userid { get; set; }
        public bool isRead { get; set; }
    }
}