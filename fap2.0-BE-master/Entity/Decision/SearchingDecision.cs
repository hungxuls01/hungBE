using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.Decision
{
    public class SearchingDecision : SearchBase
    {
        public int categoryid { get; set; }
        public int Departmentid { get; set; }
        public int userid { get; set; }
        public int roleid { get; set; }
    }
}