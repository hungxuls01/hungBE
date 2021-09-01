using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.DecisionCategory
{
    public class ViewDecisionCategory
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int status { get; set; }
        public int Departmentid { get; set; }
    }
}