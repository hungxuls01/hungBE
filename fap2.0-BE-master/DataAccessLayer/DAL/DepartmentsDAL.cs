using Entity.Combobox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.DAL
{
    public class DepartmentsDAL : DbInteractionBase
    {
        public List<Combobox> GetAll()
        {
            var Getdata = GetDataDataTable("SELECT id as value, name as text FROM Departments");
            List<Combobox> data = ConvertData.ConvertDataTable<Combobox>(Getdata);
            return data;
        }

    }
}