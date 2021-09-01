using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity
{
    public class DataTableData<T> where T : class
    {
        public int draw { get; set; }
        public long recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<T> data { get; set; }
    }
}