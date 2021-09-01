using Entity.Combobox;
using Entity.Decision;
using Entity.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.DAL
{
    public class DecisionDAL : DbInteractionBase
    {
        public List<Combobox> GetAll()
        {
            var Getdata = GetDataDataTable("SELECT id as value, name as text FROM Decisions WHERE status = 0");
            List<Combobox> data = ConvertData.ConvertDataTable<Combobox>(Getdata);
            return data;
        }

        public Tuple<int, List<ListDecision>> SelectPaging(SearchingDecision obj, int pageIndex, int pageSize)
        {
            var strSql = "";
            strSql += "SELECT ROW_NUMBER() OVER(ORDER BY no.id desc) AS RowNum,";
            strSql += " no.id,no.categoryid,dc.name as categoryName, no.userid, st.email as userEmail, no.create_date as createDate,no.status,no.reply,no.changetime  ";
            strSql += " INTO #Results FROM Decisions no ";
            strSql += " INNER JOIN Users st ON st.id = no.userid";
            strSql += " INNER JOIN DecisionCategorys dc ON dc.id = no.categoryid";
            if (obj.categoryid != -1)
            {
                strSql += " WHERE no.categoryid = " + obj.categoryid;
            }
            if (obj.userid != -1)
            {
                strSql += " WHERE no.userid = " + obj.userid;
            }
            if (obj.roleid != 3)
            {
                strSql += " AND dc.Departmentid = " + obj.Departmentid;
            }
            strSql += " ORDER BY id desc ;";

            strSql += " SELECT COUNT(*) as count  FROM #Results;";
            strSql += " SELECT * FROM #Results   WHERE (RowNum > " + pageIndex * pageSize + ") AND (RowNum <= " + (pageIndex + 1) * pageSize + ")";
            strSql += " DROP TABLE #Results;";
            var GetDecisions = GetDataDataTablePaging(strSql);
            List<NumberElements> counts = ConvertData.ConvertDataTable<NumberElements>(GetDecisions.Tables[0]);
            List<ListDecision> Decisions = ConvertData.ConvertDataTable<ListDecision>(GetDecisions.Tables[1]);
            return Tuple.Create(counts.FirstOrDefault().count, Decisions); ;
        }

        public ViewDecision GetById(int id)
        {
            var GetDecisions = GetDataDataTable("SELECT no.*,no.class as classs, "
                 + "st.FirstName + ' ' + st.MiddleName + ' ' + st.LastName as studentName,us.email as userEmail, st.Email as studentEmail, st.MobilePhone as studentMobile, st.RollNumber as studentRollNumber, st.OldRollNumber as studentRollNumberOld, st.IDCard as studentIDCard,dc.name as nameCategory, "
                 + " st.DateOfBirth as studentDateOfBirth, st.Gender as studentGender,ISNULL(nts.name,'') as studentNationName, ISNULL(cl.name,'') as studentClass, ISNULL(pr.name,'') as provinceName, st.Address as studentAddress, st.PeriodOfStudy as studentPeriodOfStudy, st.Major as studentMajor, "
                 + " ISNULL(sz.name,'') as Specialized_fromName,ISNULL(szTo.name,'') as Specialized_toName,ISNULL(cp.name,'') as campus_fromName, ISNULL(cpTo.name,'') as campus_toName, "
                 + " ISNULL(szST.name,'') as studentSpecialized,ISNULL(md.name,'') as studentModes,st.id as studentid "
                + " FROM Decisions no "
                + " INNER JOIN Users us ON us.id = no.userid"
                + " INNER JOIN Students st ON us.id = st.userid"
                + " LEFT JOIN Nations nts ON nts.id = st.Nationid"
                + " LEFT JOIN Class cl ON cl.id = st.Classid"
                + " INNER JOIN DecisionCategorys dc ON dc.id = no.categoryid"
                + " LEFT JOIN Province pr ON pr.id = no.provinceid"
                + " LEFT JOIN Specializeds sz ON sz.id = no.Specialized_from"
                + " LEFT JOIN Specializeds szTo ON szTo.id = no.Specialized_to"
                + " LEFT JOIN Campus cp ON cp.id = no.campus_from"
                + " LEFT JOIN Campus cpTo ON cpTo.id = no.campus_to"
                + " LEFT JOIN Specializeds szST ON szST.id = st.Specializedid"
                + " LEFT JOIN Modes md ON md.id = st.Modeid"
                + " WHERE no.id = " + id);
            List<ViewDecision> Decisions = ConvertData.ConvertDataTable<ViewDecision>(GetDecisions);
            return Decisions.FirstOrDefault();
        }

        public void AcceptDecision(AcceptDecision obj)
        {
            if (obj.status == 1 || obj.status == 2)
            {
                var strSql = "";
                strSql += "UPDATE Decisions SET status = " + obj.status + ", reply = N'" + obj.reply + "', emailForward = '" + obj.emailForward + "', changetime = GETDATE() WHERE id = " + obj.id;
                NonQuerySql(strSql);
            }
            else
            {
                var strSql = "";
                strSql += "UPDATE Decisions SET status = " + obj.status + ", reply = N'" + obj.reply + "', emailForward = '" + obj.emailForward + "' WHERE id = " + obj.id;
                NonQuerySql(strSql);
            }

        }

        public void InsertDecision(CreateDecision decision)
        {
            var strSql = "";
            strSql += "INSERT INTO Decisions(";
            strSql += "name,note,create_date,categoryid,status,userid,class,course,subject,exam,point,attendanceDate";
            strSql += ",Slot,teacher,dayOfReflection,files,test_date,address_test,level_test,email,mobile,address,organizationName,semester,";
            strSql += "count_test,part,year_reserve,term_reserve,dear,money,accountHolder,bank,accountID,provinceid,major,semester_start,Specialized_from";
            strSql += ",campus_from,Specialized_to,campus_to,methodOfPayment,dateOfOut,dateOfBirth,IDCard,rollNumber,year_start,modes,suggestions,reply,emailForward,changetime)";
            strSql += " VALUES (N'";
            strSql += decision.name + "',N'";
            strSql += decision.note + "','";
            strSql += decision.create_date + "',";
            strSql += decision.categoryid + ",0,";
            strSql += decision.userid + ",N'";
            strSql += decision.classs + "',N'";
            strSql += decision.course + "',N'";
            strSql += decision.subject + "',N'";
            strSql += decision.exam + "',";
            strSql += decision.point + ",'";
            strSql += decision.attendanceDate.Year + "-" + decision.attendanceDate.Month + "-" + decision.attendanceDate.Day + "',N'";
            strSql += decision.Slot + "',N'";
            strSql += decision.teacher + "','";
            strSql += decision.dayOfReflection + "',N'";
            strSql += decision.files + "','";
            strSql += decision.test_date + "',N'";
            strSql += decision.address_test + "',";
            strSql += decision.level_test + ",N'";
            strSql += decision.email + "',N'";
            strSql += decision.mobile + "',N'";
            strSql += decision.address + "',N'";
            strSql += decision.organizationName + "',N'";
            strSql += decision.semester + "',";
            strSql += decision.count_test + ",N'";
            strSql += decision.part + "',";
            strSql += decision.year_reserve + ",N'";
            strSql += decision.term_reserve + "',N'";
            strSql += decision.dear + "',";
            strSql += decision.money + ",N'";
            strSql += decision.accountHolder + "',N'";
            strSql += decision.bank + "',N'";
            strSql += decision.accountID + "',";
            strSql += decision.provinceid + ",N'";
            strSql += decision.major + "',N'";
            strSql += decision.semester_start + "',";
            strSql += decision.Specialized_from + ",";
            strSql += decision.campus_from + ",";
            strSql += decision.Specialized_to + ",";
            strSql += decision.campus_to + ",N'";
            strSql += decision.methodOfPayment + "','";
            strSql += decision.dateOfOut + "','";
            strSql += decision.dateOfBirth + "',N'";
            strSql += decision.IDCard + "',N'";
            strSql += decision.rollNumber + "',";
            strSql += decision.year_start + ",N'";
            strSql += decision.modes + "',N'";
            strSql += decision.suggestions + "','','',GETDATE())";
            NonQuerySql(strSql);
        }
    }
}