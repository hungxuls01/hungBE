using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.User
{
    public class CreateUser
    {
        public int id { get; set; }
        public string email { get; set; }
        public int Departmentid { get; set; }
        public int Campusid { get; set; }
        public bool isActive { get; set; }
        public int roleid { get; set; }
    }
}