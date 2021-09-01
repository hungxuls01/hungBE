using Entity.Combobox;
using Entity.Paging;
using Entity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.DAL
{
    public class UsersDAL : DbInteractionBase
    {
        public Tuple<int, List<ListUser>> SelectPaging(SearchingUser obj, int pageIndex, int pageSize)
        {
            var strSql = "";
            strSql += "SELECT ROW_NUMBER() OVER(ORDER BY no.id desc) AS RowNum,";
            strSql += " no.id, no.email, ur.roleid, de.name as rolename, no.isActive   ";
            strSql += " INTO #Results FROM Users no ";
            strSql += " INNER JOIN User_Role ur ON ur.userid = no.id";
            strSql += " INNER JOIN Departments de ON de.id = no.Departmentid";
            strSql += " INNER JOIN Roles rl ON rl.id = ur.roleid WHERE no.isRemove = 0  AND rl.id <> 1 AND ";
            strSql += "(no.email LIKE '%" + obj.Keyword + "%'" +
               " OR '" + obj.Keyword + "' = '') ";
            if (obj.roleid != -1)
            {
                strSql += " AND no.Departmentid = " + obj.roleid;
            }
            strSql += " ORDER BY id desc ;";

            strSql += " SELECT COUNT(*) as count  FROM #Results;";
            strSql += " SELECT * FROM #Results   WHERE (RowNum > " + pageIndex * pageSize + ") AND (RowNum <= " + (pageIndex + 1) * pageSize + ")";
            strSql += " DROP TABLE #Results;";
            var GetDecisions = GetDataDataTablePaging(strSql);
            List<NumberElements> counts = ConvertData.ConvertDataTable<NumberElements>(GetDecisions.Tables[0]);
            List<ListUser> Decisions = ConvertData.ConvertDataTable<ListUser>(GetDecisions.Tables[1]);
            return Tuple.Create(counts.FirstOrDefault().count, Decisions); ;
        }

        public int InsertUser(CreateUser user)
        {
            var strSql = "";
            strSql += "INSERT INTO Users(email,Departmentid,Campusid,isActive) VALUES ('" + user.email + "'," + user.Departmentid + "," + user.Campusid + ",'" + user.isActive + "');SELECT CAST(scope_identity() AS int); INSERT INTO User_Role(userid,roleid) VALUES (CAST(scope_identity() AS int)," + user.roleid + ")";
            int data = ScalarSql(strSql);
            return data;
        }

        public void UpdateUser(CreateUser user)
        {
            var strSql = "";
            strSql += "UPDATE Users SET email = '" + user.email + "', isActive = '" + user.isActive + "',Departmentid = " + user.Departmentid + "  WHERE id = " + user.id + "; UPDATE User_Role SET roleid = " + user.roleid + " WHERE userid = " + user.id + ";";
            NonQuerySql(strSql);
        }

        public ViewUser GetByEmail(string id)
        {
            var GetUsers = GetDataDataTable("SELECT us.id,ISNULL(st.id,0) as studentid, ISNULL( st.FirstName + ' ' + st.MiddleName + ' ' + st.LastName,'') as name, rl.name as nameRole, us.email, us.Departmentid, ur.roleid, us.isAccept, us.Campusid, us.isActive FROM Users us LEFT JOIN Students st ON us.id = st.userid INNER JOIN User_Role ur ON ur.userid = us.id INNER JOIN Roles rl ON ur.roleid = rl.id  WHERE us.isRemove = 0 AND us.Email = '" + id + "'");
            List<ViewUser> Users = ConvertData.ConvertDataTable<ViewUser>(GetUsers);
            return Users.FirstOrDefault();
        }

        public ViewUser CheckEmail(string id)
        {
            var GetUsers = GetDataDataTable("SELECT st.*,ur.roleid FROM Users st INNER JOIN User_Role ur ON st.id = ur.userid WHERE st.isRemove = 0 AND st.email = '" + id + "'");
            List <ViewUser> Users = ConvertData.ConvertDataTable<ViewUser>(GetUsers);
            return Users.FirstOrDefault();
        }


        public ViewUser GetById(int id)
        {
            var GetStudents = GetDataDataTable("SELECT st.*,ur.roleid"
                + " FROM Users st "
                + " INNER JOIN User_Role ur ON st.id = ur.userid"
                + " WHERE st.id = " + id);
            List<ViewUser> Students = ConvertData.ConvertDataTable<ViewUser>(GetStudents);
            return Students.FirstOrDefault();
        }

        public List<Combobox> GetAllRole()
        {
            var Getdata = GetDataDataTable("SELECT id as value, name as text FROM Roles WHERE id <> 1");
            List<Combobox> data = ConvertData.ConvertDataTable<Combobox>(Getdata);
            return data;
        }
    }
}