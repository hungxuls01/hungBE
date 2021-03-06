using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity
{
    public class SearchBase
    {

        private string _keyword;

        public string Keyword
        {
            get
            {
                return !string.IsNullOrEmpty(_keyword) ? _keyword.Trim() : _keyword;
            }
            set { _keyword = value; }
        }


        public string Status { get; set; }
        public string Order { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Orderdir { get; set; }
    }
}