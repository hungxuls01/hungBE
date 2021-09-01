using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entity.Combobox
{
    public class Combobox
    {
        public int value { get; set; }
        public string text { get; set; }
    }

    public class ComboboxReceiver
    {
        public int value { get; set; }
        public string text { get; set; }
        public int isGr { get; set; }
    }
}