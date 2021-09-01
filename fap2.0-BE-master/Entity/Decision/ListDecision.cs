using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.Decision
{
    public class ListDecision
    {
        public long RowNum { get; set; }
        public int id { get; set; }
        public int categoryid { get; set; }
        public string categoryName { get; set; }
        public int userid { get; set; }
        public DateTime createDate { get; set; }
        public DateTime? changetime { get; set; }
        public string userEmail { get; set; }
        public string reply { get; set; }
        public int status { get; set; }
    }
}