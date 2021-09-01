using Entity.Combobox;
using Entity.DecisionCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.DAL
{
    public class DecisionCategoryDAL : DbInteractionBase
    {
        public List<Combobox> GetAll()
        {
            var Getdata = GetDataDataTable("SELECT id as value, name as text FROM DecisionCategorys WHERE status = 0");
            List<Combobox> data = ConvertData.ConvertDataTable<Combobox>(Getdata);
            return data;
        }

        public ViewDecisionCategory GetById(int id)
        {
            var GetDecisions = GetDataDataTable("SELECT * FROM DecisionCategorys  WHERE id = " + id);
            List<ViewDecisionCategory> Decisions = ConvertData.ConvertDataTable<ViewDecisionCategory>(GetDecisions);
            return Decisions.FirstOrDefault();
        }
    }
}