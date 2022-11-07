using Repository.CommonUtils;
using Repository.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        public List<User> GetAllUser()
        {
            List<User> allUserLst = new List<User>();
            SqlUtils sqlUtils = new SqlUtils();
            SqlCommand sqlCommand = new SqlCommand(SqlDbCommand.SelectUserQuery, sqlUtils.sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            while(reader.Read())
            {
                User user = new User()
                {
                    EmailAddress = reader.GetString(1),
                    Password = reader.GetString(2),
                };
                allUserLst.Add(user);
            }
            return allUserLst;
        }
        public User FindUser(string email)
        {
            User existingUser = new User();
            SqlUtils sqlUtils = new SqlUtils();
            using(SqlConnection sqlConnection = sqlUtils.sqlConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(SqlDbCommand.FindUserQuery, sqlUtils.sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Email", email);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        existingUser.Id = reader.GetInt32(0);
                        existingUser.EmailAddress = reader.GetString(1);
                        existingUser.Password = reader.GetString(2);
                        existingUser.Roles = (Role)reader.GetInt32(3);
                    }
                }
            }
            sqlUtils.CloseConnection();
            return existingUser;
        }
        public void InsertUser(User user, string passwordHash)
        {
            SqlUtils sqlUtils = new SqlUtils();
            using(SqlConnection connection = sqlUtils.sqlConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(SqlDbCommand.InsertUserQuery, sqlUtils.sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Email", user.EmailAddress);
                    sqlCommand.Parameters.AddWithValue("@Password", passwordHash);
                    sqlCommand.Parameters.AddWithValue("@Role", Role.user);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            sqlUtils.CloseConnection();
        }
    }
}