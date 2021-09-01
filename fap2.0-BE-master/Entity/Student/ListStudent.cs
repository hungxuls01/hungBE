using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.Student
{
    public class ListStudent
    {
        public long RowNum { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string RollNumber { get; set; }
        public string Account { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string Address { get; set; }
        public bool isRemove { get; set; }
    }
}