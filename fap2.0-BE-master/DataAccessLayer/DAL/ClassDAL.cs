using Entity.Combobox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.DAL
{
    public class ClassDAL : DbInteractionBase
    {
        public List<Combobox> GetAll()
        {
            var Getdata = GetDataDataTable("SELECT id as value, name as text FROM Class WHERE status = 0");
            List<Combobox> data = ConvertData.ConvertDataTable<Combobox>(Getdata);
            return data;
        }
    }
}