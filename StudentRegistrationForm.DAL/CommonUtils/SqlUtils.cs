using System.Configuration;
using System.Data.SqlClient;

namespace Repository.CommonUtils
{
    public class SqlUtils
    {
        public string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
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