using Entity.Combobox;
using Entity.Paging;
using Entity.Student;
using Entity.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DataAccessLayer.DAL
{
    public class StudentDAL : DbInteractionBase
    {
        public void Delete(int id)
        {
            NonQuerySql("UPDATE Students SET  isRemove = 1 WHERE id = " + id + "; UPDATE Users SET isRemove = 1 WHERE id = (SELECT userid FROM Students WHERE  id = " + id + ");");
            return;
        }
        public ViewStudent GetById(int id)
        {
            var GetStudents = GetDataDataTable("SELECT st.*,"
                 + "chuyenNganh.name as QD_ChuyenNganhName, "
                 + "chinhQuy.name as QD_SV_ChinhQuyName, "
                 + "thoiHoc.name as QD_ThoiHocName, "
                 + "dieuchuyen.name as QD_DieuchuyenName, "
                 + "baoLuu.name as QD_BaoLuu_ExchangeName, "
                 + "tN.name as QD_TNName, "
                 + "rejoin.name as QD_RejoinName, "
                 + "sp.name as SpecializedName, "
                 + "spOld.name as SpecializedOldName, "
                 + "md.name as ModeName, "
                 + "cp.name as CapstoneProjectName, "
                 + "cl.name as ClassName, "
                 + "us.Campusid as Campusid, "
                 + "nts.name as NationName, "
                 + "us.Departmentid as Departmentid, "
                 + "us.Campusid as Campusid "
                + " FROM Students st "
                + " INNER JOIN Users us ON us.id = st.userid"
                + " INNER JOIN Decisions chuyenNganh ON chuyenNganh.id = st.QD_ChuyenNganh"
                + " INNER JOIN Decisions chinhQuy ON chinhQuy.id = st.QD_SV_ChinhQuy"
                + " INNER JOIN Decisions thoiHoc ON thoiHoc.id = st.QD_ThoiHoc"
                + " INNER JOIN Decisions dieuchuyen ON dieuchuyen.id = st.QD_Dieuchuyen"
                + " INNER JOIN Decisions baoLuu ON baoLuu.id = st.QD_BaoLuu_Exchange"
                + " INNER JOIN Decisions tN ON tN.id = st.QD_TN"
                + " INNER JOIN Decisions rejoin ON rejoin.id = st.QD_Rejoin"
                + " INNER JOIN Specializeds sp ON sp.id = st.Specializedid"
                + " INNER JOIN Specializeds spOld ON spOld.id = st.SpecializedOldid"
                + " INNER JOIN Modes md ON md.id = st.Modeid"
                + " INNER JOIN CapstoneProjects cp ON cp.id = st.CapstoneProjectid"
                + " INNER JOIN Class cl ON cl.id = st.Classid"
                + " INNER JOIN Nations nts ON nts.id = st.Nationid"
                + " WHERE st.id = " + id);
            List<ViewStudent> Students = ConvertData.ConvertDataTable<ViewStudent>(GetStudents);
            return Students.FirstOrDefault();
        }

        public List<ComboboxReceiver> GetCombobox()
        {
            var GetStudents = GetDataDataTable("SELECT  id as value, Email as text, 0 as isGr FROM Students WHERE isRemove = 0 UNION ALL SELECT  id as value, code as text, 1 as isGr  FROM [Group]");
            List<ComboboxReceiver> Students = ConvertData.ConvertDataTable<ComboboxReceiver>(GetStudents);
            return Students;
        }

        public List<ComboboxReceiver> GetByGroupCode(string code)
        {
            var GetStudents = GetDataDataTable("SELECT  st.id as value, st.Email as text, 0 as isGr FROM Students st INNER JOIN Student_Group sg ON st.id = sg.studentid INNER JOIN [Group] gr ON gr.id = sg.groupid WHERE gr.code = '" + code + "'");
            List<ComboboxReceiver> Students = ConvertData.ConvertDataTable<ComboboxReceiver>(GetStudents);
            return Students;
        }

        public List<ComboboxReceiver> GetAll()
        {
            var GetStudents = GetDataDataTable("SELECT  id as value, Email as text, 0 as isGr FROM Students  WHERE isRemove = 0");
            List<ComboboxReceiver> Students = ConvertData.ConvertDataTable<ComboboxReceiver>(GetStudents);
            return Students;
        }

        public List<ComboboxReceiver> GetStudent()
        {
            var GetStudents = GetDataDataTable("SELECT  st.id as value, st.Email as text, 0 as isGr FROM Students st INNER JOIN Users us ON us.id = st.userid INNER JOIN User_Role ur ON us.id = ur.userid  WHERE st.isRemove = 0 AND  ur.roleid = 1");
            List<ComboboxReceiver> Students = ConvertData.ConvertDataTable<ComboboxReceiver>(GetStudents);
            return Students;
        }

        public CreateStudent GetByRollNumber(string id)
        {
            var GetStudents = GetDataDataTable("SELECT * FROM Students WHERE RollNumber = '" + id + "'");
            List<CreateStudent> Students = ConvertData.ConvertDataTable<CreateStudent>(GetStudents);
            return Students.FirstOrDefault();
        }

        public CreateStudent GetByIDCard(string id)
        {
            var GetStudents = GetDataDataTable("SELECT * FROM Students WHERE IDCard = '" + id + "'");
            List<CreateStudent> Students = ConvertData.ConvertDataTable<CreateStudent>(GetStudents);
            return Students.FirstOrDefault();
        }

        public CreateStudent GetByUserid(int id)
        {
            var GetStudents = GetDataDataTable("SELECT * FROM Students WHERE userid = " + id);
            List<CreateStudent> Students = ConvertData.ConvertDataTable<CreateStudent>(GetStudents);
            return Students.FirstOrDefault();
        }

        public CreateStudent GetByMobilePhone(string id)
        {
            var GetStudents = GetDataDataTable("SELECT * FROM Students WHERE MobilePhone = '" + id + "'");
            List<CreateStudent> Students = ConvertData.ConvertDataTable<CreateStudent>(GetStudents);
            return Students.FirstOrDefault();
        }
        public ViewStudent GetByEmail(string id)
        {
            var GetStudents = GetDataDataTable("SELECT st.*,us.isAccept,us.Campusid,ur.roleid,us.Departmentid, rl.name as nameRole FROM Students st INNER JOIN Users us ON us.id = st.userid INNER JOIN User_Role ur ON ur.userid = us.id INNER JOIN Roles rl ON ur.roleid = rl.id  WHERE st.isRemove = 0 AND us.Email = '" + id + "'");
            List<ViewStudent> Students = ConvertData.ConvertDataTable<ViewStudent>(GetStudents);
            return Students.FirstOrDefault();
        }
        public ComboboxReceiver CheckEmail(string id)
        {
            var GetStudents = GetDataDataTable("SELECT * FROM (SELECT  id as value, Email as text, 0 as isGr FROM Students WHERE isRemove = 0 UNION ALL SELECT  id as value, code as text, 1 as isGr  FROM [Group]) master WHERE master.text = '" + id + "'");
            List<ComboboxReceiver> Students = ConvertData.ConvertDataTable<ComboboxReceiver>(GetStudents);
            return Students.FirstOrDefault();
        }
        

        public void InsertStudent(CreateStudent student)
        {
            var strSql = "";
            strSql += "INSERT INTO Students(";
            strSql += "RollNumber,OldRollNumber,LastName,MiddleName,FirstName,DateOfBirth,Gender,IDCard,DateOfIssue,PlaceOfIssue,Address,MobilePhone,Email,Image,ParentName,ParentJob,PlaceOfWork,ParentPhone,ParentAddress";
            strSql += ",ParentEmail,Major,CurrentTermNo,TermPaid,QD_ChuyenNganh,QD_SV_ChinhQuy,Date_SV_ChinhQuy,HanBayNam,LoaiTaiChinh,QD_ThoiHoc,QD_Dieuchuyen,QD_BaoLuu_Exchange,QD_TN,QD_Rejoin,TT_Den,OldCurrentTermNo,Account";
            strSql += ",Specializedid,Modeid,CapstoneProjectid,Classid,SpecializedOldid,userid,Nationid,PeriodOfStudy)";
            strSql += " VALUES (N'";
            strSql += student.RollNumber + "',N'";
            strSql += student.OldRollNumber + "',N'";
            strSql += student.LastName + "',N'";
            strSql += student.MiddleName + "',N'";
            strSql += student.FirstName + "','";
            strSql += student.DateOfBirth + "','";
            strSql += student.Gender + "','";
            strSql += student.IDCard + "','";
            strSql += student.DateOfIssue + "',N'";
            strSql += student.PlaceOfIssue + "',N'";
            strSql += student.Address + "','";
            strSql += student.MobilePhone + "','";
            strSql += student.Email + "','";
            strSql += student.Image + "',N'";
            strSql += student.ParentName + "',N'";
            strSql += student.ParentJob + "',N'";
            strSql += student.PlaceOfWork + "','";
            strSql += student.ParentPhone + "',N'";
            strSql += student.ParentAddress + "','";
            strSql += student.ParentEmail + "','";
            strSql += student.Major + "',";
            strSql += student.CurrentTermNo + ",";
            strSql += student.TermPaid + ",";
            strSql += student.QD_ChuyenNganh + ",";
            strSql += student.QD_SV_ChinhQuy + ",'";
            strSql += student.Date_SV_ChinhQuy + "','";
            strSql += student.HanBayNam + "',N'";
            strSql += student.LoaiTaiChinh + "',";
            strSql += student.QD_ThoiHoc + ",";
            strSql += student.QD_Dieuchuyen + ",";
            strSql += student.QD_BaoLuu_Exchange + ",";
            strSql += student.QD_TN + ",";
            strSql += student.QD_Rejoin + ",N'";
            strSql += student.TT_Den + "',";
            strSql += student.OldCurrentTermNo + ",'";
            strSql += student.Account + "',";
            strSql += student.Specializedid + ",";
            strSql += student.Modeid + ",";
            strSql += student.CapstoneProjectid + ",";
            strSql += student.Classid + ",";
            strSql += student.SpecializedOldid + ",";
            strSql += student.userid + ",1,'2015 - 2019')";
            NonQuerySql(strSql);
        }

        public void UpdateStudent(CreateStudent student)
        {
            var strSql = "";
            strSql += "UPDATE Students SET ";
            strSql += "RollNumber = N'" + student.RollNumber + "',";
            strSql += "OldRollNumber = N'" + student.OldRollNumber + "',";
            strSql += "LastName = N'" + student.LastName + "',";
            strSql += "MiddleName = N'" + student.MiddleName + "',";
            strSql += "FirstName = N'" + student.FirstName + "',";
            strSql += "DateOfBirth = '" + student.DateOfBirth + "',";
            strSql += "Gender = '" + student.Gender + "',";
            strSql += "IDCard = '" + student.IDCard + "',";
            strSql += "DateOfIssue = '" + student.DateOfIssue + "',";
            strSql += "PlaceOfIssue = N'" + student.PlaceOfIssue + "',";
            strSql += "Address = N'" + student.Address + "',";
            strSql += "MobilePhone = N'" + student.MobilePhone + "',";
            strSql += "Email = N'" + student.Email + "',";
            strSql += "Image = '" + student.Image + "',";
            strSql += "ParentName = N'" + student.ParentName + "',";
            strSql += "ParentJob = N'" + student.ParentJob + "',";
            strSql += "PlaceOfWork = N'" + student.PlaceOfWork + "',";
            strSql += "ParentPhone = N'" + student.ParentPhone + "',";
            strSql += "ParentAddress = N'" + student.ParentAddress + "',";
            strSql += "ParentEmail = N'" + student.ParentEmail + "',";
            strSql += "Major = N'" + student.Major + "',";
            strSql += "CurrentTermNo = " + student.CurrentTermNo + ",";
            strSql += "TermPaid = " + student.TermPaid + ",";
            strSql += "QD_ChuyenNganh = " + student.QD_ChuyenNganh + ",";
            strSql += "QD_SV_ChinhQuy = " + student.QD_SV_ChinhQuy + ",";
            strSql += "Date_SV_ChinhQuy = '" + student.Date_SV_ChinhQuy + "',";
            strSql += "HanBayNam = '" + student.HanBayNam + "',";
            strSql += "LoaiTaiChinh = N'" + student.LoaiTaiChinh + "',";
            strSql += "QD_ThoiHoc = " + student.QD_ThoiHoc + ",";
            strSql += "QD_Dieuchuyen = " + student.QD_Dieuchuyen + ",";
            strSql += "QD_BaoLuu_Exchange = " + student.QD_BaoLuu_Exchange + ",";
            strSql += "QD_TN = " + student.QD_TN + ",";
            strSql += "QD_Rejoin = " + student.QD_Rejoin + ",";
            strSql += "TT_Den = N'" + student.TT_Den + "',";
            strSql += "OldCurrentTermNo = " + student.OldCurrentTermNo + ",";
            strSql += "Account = '" + student.Account + "',";
            strSql += "Specializedid = " + student.Specializedid + ",";
            strSql += "Modeid = " + student.Modeid + ",";
            strSql += "CapstoneProjectid = " + student.CapstoneProjectid + ",";
            strSql += "Classid = " + student.Classid + ",";
            strSql += "SpecializedOldid = " + student.SpecializedOldid + " WHERE id = " + student.id;
            NonQuerySql(strSql);
        }

        public Tuple<int, List<ListStudent>> SelectPaging(SearchingStudent obj, int pageIndex, int pageSize)
        {
            var strSql = "";
            strSql += "SELECT ROW_NUMBER() OVER(ORDER BY st.id desc) AS RowNum,";
            strSql += " st.id,st.FirstName + ' ' + st.MiddleName + ' ' + st.LastName as name,st.RollNumber,st.Account,st.Email,st.MobilePhone,st.Address,st.isRemove";
            strSql += " INTO #Results FROM Students st";
            strSql += " INNER JOIN Users us ON us.id = st.userid";
            strSql += " INNER JOIN User_Role ur ON us.id = ur.userid";
            strSql += " WHERE st.isRemove = 0 AND ur.roleid = 1 AND ";
            strSql += "(st.FirstName + ' ' + st.MiddleName + ' ' + st.LastName LIKE N'%" + obj.Keyword + "%' OR st.RollNumber LIKE '%" + obj.Keyword + "%'" +
                " OR st.Account LIKE '%" + obj.Keyword + "%' " +
                " OR st.Email LIKE '%" + obj.Keyword + "%' " +
                " OR st.MobilePhone LIKE '%" + obj.Keyword + "%' " +
                " OR '" + obj.Keyword + "' = '') ";
            strSql += "ORDER BY st.id desc ;";

            strSql += " SELECT COUNT(*) as count  FROM #Results;";
            strSql += " SELECT * FROM #Results   WHERE (RowNum > " + pageIndex * pageSize + ") AND (RowNum <= " + (pageIndex + 1) * pageSize + ")";
            strSql += " DROP TABLE #Results;";
            var GetStudents = GetDataDataTablePaging(strSql);
            List<NumberElements> counts = ConvertData.ConvertDataTable<NumberElements>(GetStudents.Tables[0]);
            List<ListStudent> Students = ConvertData.ConvertDataTable<ListStudent>(GetStudents.Tables[1]);
            return Tuple.Create(counts.FirstOrDefault().count, Students); ;
        }

        public void ImportStudentByExcel()
        {


            string constr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\excel\\TestImportStudent.xlsx;Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"");
            //string constr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"");
            var Econ = new OleDbConnection(constr);
            string query = string.Format("Select * FROM [{0}]", "Sheet1$");
            OleDbCommand Ecom = new OleDbCommand(query, Econ);
            Econ.Open();

            DataSet ds = new DataSet();
            OleDbDataAdapter oda = new OleDbDataAdapter(query, Econ);
            Econ.Close();
            oda.Fill(ds);
            DataTable Exceldt = ds.Tables[0];
            Regex _regexIDCard = new Regex("[0-9]{12}");
            Regex _regexPhone = new Regex("(84|0[3|5|7|8|9])+([0-9]{8})\b");
            Regex _regexEmali = new Regex("([a-zA-Z0-9])@fpt.edu.vn");
            Regex _regexEmaliParent = new Regex("^(.+)@(\\S+)$");
            Regex _regexTerm = new Regex("[1-9]{1}");
            Regex _regexHanBayNam = new Regex("([A-Z]{2})+([0-9]{2})");
            string[] formatsDate = { "yyyy--MM-dd" };
            DateTime dateTime;

            // RollNumber, OldRollNumber, IDCard, MobilePhone, Email
            List<CreateStudent> RollNumber = GetAllStudents();
            for (int i = Exceldt.Rows.Count - 1; i >= 0; i--)
            {

                string roll = Exceldt.Rows[i]["RollNumber"].ToString();
                //var std = GetByRoll(roll);

                if (
                    exist(RollNumber, Exceldt.Rows[i]["RollNumber"].ToString())
                    || (existOldRoll(RollNumber, Exceldt.Rows[i]["OldRollNumber"].ToString()) && Exceldt.Rows[i]["OldRollNumber"] != DBNull.Value)
                        || existIDCard(RollNumber, Exceldt.Rows[i]["IDCard"].ToString())
                        || existPhone(RollNumber, Exceldt.Rows[i]["MobilePhone"].ToString())
                        || existEmail(RollNumber, Exceldt.Rows[i]["Email"].ToString())
                || Exceldt.Rows[i]["RollNumber"] == DBNull.Value
            || Exceldt.Rows[i]["RollNumber"].ToString().Length > 10
            || Exceldt.Rows[i]["OldRollNumber"].ToString().Length > 10
            || Exceldt.Rows[i]["LastName"].ToString().Length > 100
            || Exceldt.Rows[i]["LastName"] == DBNull.Value
            || Exceldt.Rows[i]["MiddleName"].ToString().Length > 100
            || Exceldt.Rows[i]["FirstName"] == DBNull.Value
            || Exceldt.Rows[i]["FirstName"].ToString().Length > 100
            || Exceldt.Rows[i]["DateOfBirth"] == DBNull.Value
            || Exceldt.Rows[i]["Gender"] == DBNull.Value
                //|| (!Exceldt.Rows[i]["Gender"].ToString().Equals("0") && !Exceldt.Rows[i]["Gender"].ToString().Equals("1"))
                || Exceldt.Rows[i]["IDCard"] == DBNull.Value
                || !_regexIDCard.IsMatch(Exceldt.Rows[i]["IDCard"].ToString())
                || Exceldt.Rows[i]["DateOfIssue"] == DBNull.Value
                || Exceldt.Rows[i]["PlaceOfIssue"] == DBNull.Value
                || Exceldt.Rows[i]["PlaceOfIssue"].ToString().Length > 500
                || Exceldt.Rows[i]["Address"].ToString().Length > 1000
            //|| !_regexPhone.IsMatch(Exceldt.Rows[i]["MobilePhone"].ToString())
            //|| Exceldt.Rows[i]["Email"] == DBNull.Value
            //|| !_regexEmali.IsMatch(Exceldt.Rows[i]["Email"].ToString())
            //|| Exceldt.Rows[i]["ParentName"].ToString().Length > 500
            //|| Exceldt.Rows[i]["ParenJob"].ToString().Length > 100
            //|| Exceldt.Rows[i]["PlaceOfWork"].ToString().Length > 1000
            //|| !_regexPhone.IsMatch(Exceldt.Rows[i]["ParentPhone"].ToString())
            //|| Exceldt.Rows[i]["ParentAddress"].ToString().Length > 1500
            //|| !_regexEmaliParent.IsMatch(Exceldt.Rows[i]["ParentEmail"].ToString())
            //|| Exceldt.Rows[i]["Major"].ToString().Length > 50
            //|| !_regexTerm.IsMatch(Exceldt.Rows[i]["CurrentTermNo"].ToString())
            //|| !_regexTerm.IsMatch(Exceldt.Rows[i]["TermPaid"].ToString())
            //|| !_regexTerm.IsMatch(Exceldt.Rows[i]["QD_ChuyenNganh"].ToString())
            //|| !_regexTerm.IsMatch(Exceldt.Rows[i]["QD_SV_ChinhQuy"].ToString())

            //|| !_regexHanBayNam.IsMatch(Exceldt.Rows[i]["HanBayNam"].ToString())
            //|| !DateTime.TryParse(Exceldt.Rows[i]["date_sv_chinhquy"].ToString(), out dateTime)

            //|| DateTime.TryParseExact(Exceldt.Rows[i]["DateOfBirth"].ToString(), formatsDate, new CultureInfo("en-US"),
            //               DateTimeStyles.None, out dateTime)

            //|| DateTime.TryParseExact(Exceldt.Rows[i]["DateOfIssue"].ToString(), formatsDate, new CultureInfo("en-US"),
            //               DateTimeStyles.None, out dateTime)


            //|| DateTime.Compare(DateTime.Parse(Exceldt.Rows[i]["dateofbirth"].ToString()), DateTime.Parse(Exceldt.Rows[i]["DateOfIssue"].ToString())) > 0
            //|| DateTime.Compare(DateTime.Parse(Exceldt.Rows[i]["dateofbirth"].ToString()), DateTime.Parse(Exceldt.Rows[i]["Date_SV_ChinhQuy"].ToString())) > 0
            )
                {
                    Exceldt.Rows[i].Delete();
                }

                else
                {
                    Exceldt.AcceptChanges();
                    var connectionString = ConfigurationManager.ConnectionStrings["ADOConnection"].ConnectionString;
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();
                    CreateStudent student = new CreateStudent();
                    student.RollNumber = Exceldt.Rows[i]["RollNumber"].ToString().Trim();
                    student.OldRollNumber = Exceldt.Rows[i]["OldRollNumber"].ToString().Trim();
                    student.FirstName = Exceldt.Rows[i]["FirstName"].ToString().Trim();
                    student.MiddleName = Exceldt.Rows[i]["MiddleName"].ToString();
                    student.LastName = Exceldt.Rows[i]["LastName"].ToString().Trim();
                    student.Gender = Exceldt.Rows[i]["Gender"].ToString().Trim().Equals("1") ? true : false;
                    student.IDCard = Exceldt.Rows[i]["IDCard"].ToString().Trim();
                    student.PlaceOfIssue = Exceldt.Rows[i]["PlaceOfIssue"].ToString().Trim();
                    student.Address = Exceldt.Rows[i]["Address"].ToString().Trim();
                    student.MobilePhone = Exceldt.Rows[i]["MobilePhone"].ToString().Trim();
                    student.Email = Exceldt.Rows[i]["Email"].ToString().Trim();
                    student.ParentName = Exceldt.Rows[i]["ParentName"].ToString().Trim();
                    student.ParentJob = Exceldt.Rows[i]["ParentJob"].ToString();
                    student.PlaceOfWork = Exceldt.Rows[i]["PlaceOfWork"].ToString();
                    student.ParentPhone = Exceldt.Rows[i]["ParentPhone"].ToString().Trim();


                    student.ParentAddress = Exceldt.Rows[i]["ParentAddress"].ToString().Trim();
                    student.ParentEmail = Exceldt.Rows[i]["ParentEmail"].ToString();
                    student.Major = Exceldt.Rows[i]["Major"].ToString();
                    //student.CurrentTermNo = (short)Convert.ChangeType(Exceldt.Rows[i]["CurrentTermNo"].ToString(), typeof(short));
                    student.CurrentTermNo = Convert.ToInt16(Exceldt.Rows[i]["CurrentTermNo"].ToString());
                    //student.TermPaid = (short)Convert.ChangeType(Exceldt.Rows[i]["TermPaid"].ToString(), typeof(short));
                    student.TermPaid = Convert.ToInt16(Exceldt.Rows[i]["TermPaid"].ToString());
                    student.QD_ChuyenNganh = Convert.ToInt16(Exceldt.Rows[i]["QD_ChuyenNganh"].ToString());
                    student.QD_SV_ChinhQuy = Convert.ToInt16(Exceldt.Rows[i]["QD_SV_ChinhQuy"].ToString());
                    student.Date_SV_ChinhQuy = DateTime.Parse(Exceldt.Rows[i]["Date_SV_ChinhQuy"].ToString());
                    student.HanBayNam = Exceldt.Rows[i]["HanBayNam"].ToString().Trim();
                    student.LoaiTaiChinh = Exceldt.Rows[i]["LoaiTaiChinh"].ToString().Trim();
                    student.QD_ThoiHoc = Convert.ToInt16(Exceldt.Rows[i]["QD_ThoiHoc"].ToString());
                    student.QD_Dieuchuyen = Convert.ToInt16(Exceldt.Rows[i]["QD_Dieuchuyen"].ToString());
                    student.QD_BaoLuu_Exchange = Convert.ToInt16(Exceldt.Rows[i]["QD_BaoLuu_Exchange"].ToString());
                    student.QD_TN = Convert.ToInt16(Exceldt.Rows[i]["QD_TN"].ToString());
                    student.TT_Den = Exceldt.Rows[i]["TT_Den"].ToString();
                    student.OldCurrentTermNo = Convert.ToInt16(Exceldt.Rows[i]["OldCurrentTermNo"].ToString());
                    student.Specializedid = Convert.ToInt16(Exceldt.Rows[i]["Specializedid"].ToString());
                    student.Modeid = Convert.ToInt16(Exceldt.Rows[i]["Modeid"].ToString());
                    student.CapstoneProjectid = Convert.ToInt16(Exceldt.Rows[i]["CapstoneProjectid"].ToString());
                    student.Classid = Convert.ToInt16(Exceldt.Rows[i]["Classid"].ToString());
                    student.SpecializedOldid = Convert.ToInt16(Exceldt.Rows[i]["SpecializedOldid"].ToString().Trim());
                    student.QD_Rejoin = Convert.ToInt16(Exceldt.Rows[i]["QD_Rejoin"].ToString());
                    student.DateOfIssue = DateTime.Parse(Exceldt.Rows[i]["DateOfIssue"].ToString().Trim());
                    student.DateOfBirth = DateTime.Parse(Exceldt.Rows[i]["DateOfBirth"].ToString().Trim());
                    string[] acc = Exceldt.Rows[i]["Email"].ToString().Split('@');
                    student.Account = acc[0];

                    student.Campusid = 1;
                    student.Departmentid = 1;
                    var user = new CreateUser();
                    user.Departmentid = (int)student.Departmentid;
                    user.Campusid = (int)student.Campusid;
                    user.email = student.Email;
                    var userid = InsertUser(user);
                    student.userid = userid;
                    try
                    {
                        InsertStudent(student);
                        trans.Commit();

                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                    }

                    con.Close();
                }
            }
        }

        public void UpdateStudentByExcel()
        {

            string constr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\excel\\TestUpdateStudent.xlsx;Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"");
            //string constr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"");
            var Econ = new OleDbConnection(constr);
            string query = string.Format("Select * FROM [{0}]", "Sheet1$");
            OleDbCommand Ecom = new OleDbCommand(query, Econ);
            Econ.Open();

            DataSet ds = new DataSet();
            OleDbDataAdapter oda = new OleDbDataAdapter(query, Econ);
            Econ.Close();
            oda.Fill(ds);
            DataTable Exceldt = ds.Tables[0];
            Regex _regexIDCard = new Regex("[0-9]{12}");
            Regex _regexPhone = new Regex("(84|0[1-9])+([0-9]{8})\b");
            Regex _regexEmali = new Regex("([a-zA-Z0-9]){9}@fpt.edu.vn");
            Regex _regexEmaliParent = new Regex("^(.+)@(\\S+)$");
            Regex _regexTerm = new Regex("[1-9]{1}");
            Regex _regexHanBayNam = new Regex("([A-Z]{2})+([0-9]{2})");
            DateTime dateTime;

            List<CreateStudent> RollNumber = GetAllRollNumber();
            for (int i = Exceldt.Rows.Count - 1; i >= 0; i--)
            {
                string roll = Exceldt.Rows[i]["RollNumber"].ToString();
                if (Exceldt.Rows[i]["RollNumber"] == DBNull.Value
                    || Exceldt.Rows[i]["RollNumber"].ToString().Equals("")
                    || Exceldt.Rows[i]["RollNumber"].ToString() == null
                    || !exist(RollNumber, Exceldt.Rows[i]["RollNumber"].ToString())
                )
                {
                    Exceldt.Rows[i].Delete();
                }

                Exceldt.AcceptChanges();
            }
            var connectionString = ConfigurationManager.ConnectionStrings["ADOConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            foreach (DataRow _dr in Exceldt.Rows)
            {
                try
                {
                    string roll = _dr["RollNumber"].ToString();
                    SqlCommand cmd = new SqlCommand("UPDATE Students SET OldRollNumber = @OldRollNumber," +
                        "Address = @Address," +
                        "MobilePhone = @MobilePhone," +
                        "Email = @Email," +
                        "ParentJob = @ParentJob," +
                        "PlaceOfWork = @PlaceOfWork," +
                        "ParentPhone = @ParentPhone," +
                        "ParentAddress = @ParentAddress," +
                        "ParentEmail = @ParentEmail," +
                        "Major = @Major," +
                        "CurrentTermNo = @CurrentTermNo," +
                        "TermPaid = @TermPaid," +
                        "QD_ChuyenNganh = @QD_ChuyenNganh," +
                        "QD_SV_ChinhQuy = @QD_SV_ChinhQuy," +
                        "Date_SV_ChinhQuy = @Date_SV_ChinhQuy," +
                        "HanBayNam = @HanBayNam," +
                        "LoaiTaiChinh = @LoaiTaiChinh," +
                        "QD_ThoiHoc = @QD_ThoiHoc," +
                        "QD_Dieuchuyen = @QD_Dieuchuyen," +
                        "QD_BaoLuu_Exchange = @QD_BaoLuu_Exchange," +
                        "QD_TN = @QD_TN," +
                        "QD_Rejoin = @QD_Rejoin," +
                        "TT_Den = @TT_Den," +
                        "OldCurrentTermNo = @OldCurrentTermNo," +
                        "Specializedid = @Specializedid," +
                        "Modeid = @Modeid," +
                        "CapstoneProjectid = @CapstoneProjectid," +
                        "Classid = @Classid," +
                        "SpecializedOldid = @SpecializedOldid," +
                        "isRemove = @isRemove " +
                        "where RollNumber = '" + roll + "'", con);
                    cmd.Transaction = trans;


                    var std = GetByRoll(roll);

                    if (std != null)
                    {
                        // OldRollNumber
                        if (_dr["OldRollNumber"].ToString().Equals("") || _dr["OldRollNumber"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("OldRollNumber", std.OldRollNumber);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("OldRollNumber", _dr["OldRollNumber"].ToString());
                        }

                        // Address
                        if (_dr["Address"].ToString().Equals("") || _dr["Address"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("Address", std.Address);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("Address", _dr["Address"].ToString());
                        }
                        // MobilePhone
                        if (_dr["MobilePhone"].ToString().Equals("") || _dr["MobilePhone"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("MobilePhone", std.MobilePhone);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("MobilePhone", _dr["MobilePhone"].ToString());
                        }
                        // Email
                        if (_dr["Email"].ToString().Equals("") || _dr["Email"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("Email", std.Email);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("Email", _dr["Email"].ToString());
                        }
                        // ParentJob
                        if (_dr["ParentJob"].ToString().Equals("") || _dr["ParentJob"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("ParentJob", std.ParentJob);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("ParentJob", _dr["ParentJob"].ToString());
                        }
                        // PlaceOfWork
                        if (_dr["PlaceOfWork"].ToString().Equals("") || _dr["PlaceOfWork"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("PlaceOfWork", std.PlaceOfWork);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("PlaceOfWork", _dr["PlaceOfWork"].ToString());
                        }
                        // ParentPhone
                        if (_dr["ParentPhone"].ToString().Equals("") || _dr["ParentPhone"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("ParentPhone", std.ParentPhone);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("ParentPhone", _dr["ParentPhone"].ToString());
                        }
                        // ParentAddress
                        if (_dr["ParentAddress"].ToString().Equals("") || _dr["ParentAddress"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("ParentAddress", std.ParentAddress);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("ParentAddress", _dr["ParentAddress"].ToString());
                        }
                        // ParentEmail
                        if (_dr["ParentEmail"].ToString().Equals("") || _dr["ParentEmail"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("ParentEmail", std.ParentEmail);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("ParentEmail", _dr["ParentEmail"].ToString());
                        }
                        // Major
                        if (_dr["Major"].ToString().Equals("") || _dr["Major"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("Major", std.Major);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("Major", _dr["Major"].ToString());
                        }
                        // CurrentTermNo
                        if (_dr["CurrentTermNo"].ToString().Equals("") || _dr["CurrentTermNo"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("CurrentTermNo", std.CurrentTermNo);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("CurrentTermNo", _dr["CurrentTermNo"].ToString());
                        }
                        // TermPaid
                        if (_dr["TermPaid"].ToString().Equals("") || _dr["TermPaid"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("TermPaid", std.TermPaid);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("TermPaid", _dr["TermPaid"].ToString());
                        }
                        // QD_ChuyenNganh
                        if (_dr["QD_ChuyenNganh"].ToString().Equals("") || _dr["QD_ChuyenNganh"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("QD_ChuyenNganh", std.QD_ChuyenNganh);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("QD_ChuyenNganh", _dr["QD_ChuyenNganh"].ToString());
                        }
                        // QD_SV_ChinhQuy
                        if (_dr["QD_SV_ChinhQuy"].ToString().Equals("") || _dr["QD_SV_ChinhQuy"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("QD_SV_ChinhQuy", std.QD_SV_ChinhQuy);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("QD_SV_ChinhQuy", _dr["QD_SV_Chinhquy"].ToString());
                        }
                        // Date_SV_ChinhQuy
                        if (_dr["Date_SV_ChinhQuy"].ToString().Equals("") || _dr["Date_SV_ChinhQuy"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("Date_SV_ChinhQuy", std.Date_SV_ChinhQuy);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("Date_SV_ChinhQuy", _dr["Date_SV_Chinhquy"].ToString());
                        }
                        // HanBayNam
                        if (_dr["HanBayNam"].ToString().Equals("") || _dr["HanBayNam"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("HanBayNam", std.HanBayNam);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("HanBayNam", _dr["HanBayNam"].ToString());
                        }
                        // LoaiTaiChinh
                        if (_dr["LoaiTaiChinh"].ToString().Equals("") || _dr["LoaiTaiChinh"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("LoaiTaiChinh", std.LoaiTaiChinh);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("LoaiTaiChinh", _dr["LoaiTaiChinh"].ToString());
                        }
                        // QD_ThoiHoc
                        if (_dr["QD_ThoiHoc"].ToString().Equals("") || _dr["QD_ThoiHoc"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("QD_ThoiHoc", std.QD_ThoiHoc);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("QD_ThoiHoc", _dr["QD_ThoiHoc"].ToString());
                        }
                        // QD_Dieuchuyen
                        if (_dr["QD_Dieuchuyen"].ToString().Equals("") || _dr["QD_Dieuchuyen"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("QD_Dieuchuyen", std.QD_Dieuchuyen);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("QD_Dieuchuyen", _dr["QD_Dieuchuyen"].ToString());
                        }
                        // QD_BaoLuu_Exchange
                        if (_dr["QD_BaoLuu_Exchange"].ToString().Equals("") || _dr["QD_BaoLuu_Exchange"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("QD_BaoLuu_Exchange", std.QD_BaoLuu_Exchange);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("QD_BaoLuu_Exchange", _dr["QD_BaoLuu_Exchange"].ToString());
                        }
                        // QD_TN
                        if (_dr["QD_TN"].ToString().Equals("") || _dr["QD_TN"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("QD_TN", std.QD_TN);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("QD_TN", _dr["QD_TN"].ToString());
                        }
                        // QD_Rejoin
                        if (_dr["QD_Rejoin"].ToString().Equals("") || _dr["QD_Rejoin"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("QD_Rejoin", std.QD_Rejoin);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("QD_Rejoin", _dr["QD_Rejoin"].ToString());
                        }
                        // TT_Den
                        if (_dr["TT_Den"].ToString().Equals("") || _dr["TT_Den"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("TT_Den", std.TT_Den);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("TT_Den", _dr["TT_Den"].ToString());
                        }
                        // OldCurrentTermNo
                        if (_dr["OldCurrentTermNo"].ToString().Equals("") || _dr["OldCurrentTermNo"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("OldCurrentTermNo", std.OldCurrentTermNo);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("OldCurrentTermNo", _dr["OldCurrentTermNo"].ToString());
                        }
                        // Specializedid
                        if (_dr["Specializedid"].ToString().Equals("") || _dr["Specializedid"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("Specializedid", std.Specializedid);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("Specializedid", _dr["Specializedid"].ToString());
                        }
                        // Modeid
                        if (_dr["Modeid"].ToString().Equals("") || _dr["Modeid"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("Modeid", std.Modeid);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("Modeid", _dr["Modeid"].ToString());
                        }
                        // CapstoneProjectid
                        if (_dr["CapstoneProjectid"].ToString().Equals("") || _dr["CapstoneProjectid"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("CapstoneProjectid", std.CapstoneProjectid);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("CapstoneProjectid", _dr["CapstoneProjectid"].ToString());
                        }
                        // Classid
                        if (_dr["Classid"].ToString().Equals("") || _dr["Classid"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("Classid", std.Classid);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("Classid", _dr["Classid"].ToString());
                        }
                        // SpecializedOldid
                        if (_dr["SpecializedOldid"].ToString().Equals("") || _dr["SpecializedOldid"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("SpecializedOldid", std.SpecializedOldid);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("SpecializedOldid", _dr["SpecializedOldid"].ToString());
                        }
                        // isRemove
                        if (_dr["isRemove"].ToString().Equals("") || _dr["isRemove"].ToString() == null)
                        {
                            cmd.Parameters.AddWithValue("isRemove", std.isRemove);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("isRemove", _dr["isRemove"].ToString());
                        }

                        //con.Open();
                        cmd.ExecuteNonQuery();
                        trans.Commit();
                    }
                    else
                    {
                        trans.Rollback();
                    }
                }
                catch (Exception ex) { }
            }
            con.Close();
        }

        public List<CreateStudent> GetAllStudents()
        {
            var GetStudents = GetDataDataTable("select RollNumber, OldRollNumber, DateOfBirth, IDCard, MobilePhone, Email, Date_SV_ChinhQuy from Students");
            List<CreateStudent> Students = ConvertData.ConvertDataTable<CreateStudent>(GetStudents);
            return Students;
        }
        public List<CreateStudent> GetAllRollNumber()
        {
            var GetStudents = GetDataDataTable("select RollNumber from Students");
            List<CreateStudent> Students = ConvertData.ConvertDataTable<CreateStudent>(GetStudents);
            return Students;
        }
        public CreateStudent GetByRoll(string roll)
        {
            var GetStudents = GetDataDataTable(" select RollNumber, OldRollNumber, Address, Email, MobilePhone, ParentJob, PlaceOfWork, ParentPhone, ParentAddress, ParentEmail, Major, CurrentTermNo, TermPaid, QD_ChuyenNganh, QD_SV_ChinhQuy, Date_SV_ChinhQuy, HanBayNam, LoaiTaiChinh, QD_ThoiHoc, QD_Dieuchuyen, QD_BaoLuu_Exchange, QD_TN, QD_Rejoin, TT_Den, isRemove, SpecializedOldid, Classid, CapstoneProjectid, Modeid, Specializedid, OldCurrentTermNo from Students where RollNumber = '" + roll + "'");
            List<CreateStudent> Students = ConvertData.ConvertDataTable<CreateStudent>(GetStudents);
            return Students.FirstOrDefault();
        }

        public bool exist(List<CreateStudent> creates, string str)
        {
            foreach (CreateStudent student in creates)
            {
                if (str.Equals(student.RollNumber))
                {
                    return true;
                }
            }
            return false;
        }

        public bool existIDCard(List<CreateStudent> creates, string str)
        {
            foreach (CreateStudent student in creates)
            {
                if (str.Equals(student.IDCard))
                {
                    return true;
                }
            }
            return false;
        }

        public bool existEmail(List<CreateStudent> creates, string str)
        {
            foreach (CreateStudent student in creates)
            {
                if (str.Equals(student.Email))
                {
                    return true;
                }
            }
            return false;
        }

        public bool existOldRoll(List<CreateStudent> creates, string str)
        {
            foreach (CreateStudent student in creates)
            {
                if (str.Equals(student.OldRollNumber))
                {
                    return true;
                }
            }
            return false;
        }

        public bool existPhone(List<CreateStudent> creates, string str)
        {
            foreach (CreateStudent student in creates)
            {
                if (str.Equals(student.MobilePhone))
                {
                    return true;
                }
            }
            return false;
        }

        public int InsertUser(CreateUser user)
        {
            var strSql = "";
            strSql += "INSERT INTO Users(email,Departmentid,Campusid) VALUES ('" + user.email + "'," + user.Departmentid + "," + user.Campusid + ");SELECT CAST(scope_identity() AS int); INSERT INTO User_Role(userid,roleid) VALUES (CAST(scope_identity() AS int),1)";
            int data = ScalarSql(strSql);
            return data;
        }
    }
}