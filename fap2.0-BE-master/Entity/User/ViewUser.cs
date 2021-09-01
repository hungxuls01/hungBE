using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.User
{
    public class ViewUser
    {
        public int id { get; set; }
        public int studentid { get; set; }
        public string name { get; set; }
        public string nameRole { get; set; }
        public string email { get; set; }
        public int Departmentid { get; set; }
        public int roleid { get; set; }
        public bool isAccept { get; set; }
        public bool isActive { get; set; }
        public int Campusid { get; set; }
    }
}