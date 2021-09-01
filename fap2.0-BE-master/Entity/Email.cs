using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity
{
    public class Email
    {
        public int id { get; set; }
        public string mail { get; set; }
        public string pass { get; set; }
        public int count { get; set; }
        public DateTime dateRun { get; set; }
    }
}
