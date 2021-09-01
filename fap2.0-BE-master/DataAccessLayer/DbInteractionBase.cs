using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;

namespace DataAccessLayer
{
    public class DbInteractionBase
    {

        /// <summary>
        ///     Key để kết nối đến database qua file webconfig
        /// </summary> 
        public readonly string ConnectString = ConfigurationManager.ConnectionStrings["ADOConnection"].ConnectionString;
        public DataTable GetDataDataTable(string SQL)
        {
            using (SqlConnection conn = new SqlConnection(ConnectString))
            {

                conn.Open();
                DataTable dttb = GetDataDataTable(conn, SQL);
                conn.Close();
                return dttb;
            }
        }

        public static DataTable GetDataDataTable(SqlConnection conn, string SQL)
        {
            DataTable dttb = new DataTable();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandTimeout = 0;
            cmd.CommandText = SQL;
            cmd.Connection = conn;
            SqlDataAdapter SQlAdap = new SqlDataAdapter(cmd);
            SQlAdap.Fill(dttb);
            return dttb;
        }

        public DataSet GetDataDataTablePaging(string SQL)
        {
            using (SqlConnection conn = new SqlConnection(ConnectString))
            {

                conn.Open();
                DataSet dttb = GetDataDataTablePaging(conn, SQL);
                conn.Close();
                return dttb;
            }
        }
        public static DataSet GetDataDataTablePaging(SqlConnection conn, string SQL)
        {
            DataSet dttb = new DataSet();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandTimeout = 0;
            cmd.CommandText = SQL;
            cmd.Connection = conn;
            SqlDataAdapter SQlAdap = new SqlDataAdapter(cmd);
            SQlAdap.Fill(dttb);
            return dttb;
        }
        public void NonQuerySql(string SQL)
        {
            SqlConnection sqlConn = new SqlConnection(ConnectString);
            sqlConn.Open();
            SqlCommand sqlCommand = new SqlCommand(SQL);
            sqlCommand.CommandTimeout = 0;
            sqlCommand.Connection = sqlConn;

            sqlCommand.CommandText = SQL;
            sqlCommand.ExecuteNonQuery();
            sqlConn.Close();
        }
        public int ScalarSql (string SQL)
        {
            int newID;
            SqlConnection sqlConn = new SqlConnection(ConnectString);
            sqlConn.Open();
            SqlCommand sqlCommand = new SqlCommand(SQL);
            sqlCommand.CommandTimeout = 0;
            sqlCommand.Connection = sqlConn;

            sqlCommand.CommandText = SQL;
            newID =  (int)sqlCommand.ExecuteScalar();
            sqlConn.Close();
            return newID;
        }
    }
}
