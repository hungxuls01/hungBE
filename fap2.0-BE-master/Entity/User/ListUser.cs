using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.User
{
    public class ListUser
    {
        public long RowNum { get; set; }
        public int id { get; set; }
        public string email { get; set; }
        public int roleid { get; set; }
        public string rolename { get; set; }
        public bool isActive { get; set; }
    }
}