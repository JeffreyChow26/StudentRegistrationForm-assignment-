using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL.CommonUtils
{
    public class SqlUtils
    {
        public string connection = "Data Source=L-PW02X07C; Initial Catalog=StudentRegistrationForm; Integrated Security = True";
        public SqlConnection sqlConnection;

        public SqlUtils()
        {
            sqlConnection = new SqlConnection(connection);
            OpenConnection();
        }

        public void OpenConnection()
        {
            try
            {
                if(sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
                sqlConnection.Open();
            }
            catch(SqlException ex)
            {
                throw ex;
            }
        }
        public void CloseConnection()
        {
            if(sqlConnection != null && sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Dispose();
            }
        }
    }
}