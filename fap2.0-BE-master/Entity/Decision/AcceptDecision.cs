using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.Decision
{
    public class AcceptDecision
    {
        public int id { get; set; }
        public int status { get; set; }
        public string reply { get; set; }
        public string emailForward { get; set; }
    }
}