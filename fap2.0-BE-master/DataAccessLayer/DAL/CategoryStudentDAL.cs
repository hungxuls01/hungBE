using Entity.CategoryStudent;
using Entity.Combobox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.DAL
{
    public class CategoryStudentDAL : DbInteractionBase
    {
        public List<Combobox> GetByStudentid(int studentid)
        {
            var Getdata = GetDataDataTable("SELECT id as value, name as text FROM CategoryStudent WHERE isRemove = 0 AND userid = " + studentid);
            List<Combobox> data = ConvertData.ConvertDataTable<Combobox>(Getdata);
            return data;
        }

        public void Insert(CreateCategoryStudent student)
        {
            var strSql = "";
            strSql += "INSERT INTO CategoryStudent(";
            strSql += "userid,name)";
            strSql += " VALUES (";
            strSql += student.userid + ",N'";
            strSql += student.name + "')";
            NonQuerySql(strSql);
        }

    }
}