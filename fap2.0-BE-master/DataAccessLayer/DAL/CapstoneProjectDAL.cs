using Entity.Combobox;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DataAccessLayer.DAL
{
    public class CapstoneProjectDAL : DbInteractionBase
    {
        public List<Combobox> GetAll()
        {
            var Getdata = GetDataDataTable("SELECT id as value, name as text FROM CapstoneProjects WHERE status = 0");
            List<Combobox> data = ConvertData.ConvertDataTable<Combobox>(Getdata);
            return data;
        }
        
    }
}