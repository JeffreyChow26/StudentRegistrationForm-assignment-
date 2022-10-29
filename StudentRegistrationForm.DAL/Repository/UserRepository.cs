using DAL.CommonUtils;
using DAL.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        public List<User> GetAllUser()
        {
            List<User> allUserLst = new List<User>();
            SqlUtils sqlUtils = new SqlUtils();
            string SelectAllQuery = "select EmailAddress,Password from [Users]";
            SqlCommand sqlCommand = new SqlCommand(SelectAllQuery, sqlUtils.sqlConnection);
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
            string query = "select top 1 Id, Email, Password from [Users] where Email = @Email";
            using(SqlConnection sqlConnection = sqlUtils.sqlConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlUtils.sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Email", email);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        existingUser.Id = reader.GetInt32(0);
                        existingUser.EmailAddress = reader.GetString(1);
                        existingUser.Password = reader.GetString(2);
                    }
                }
            }
            sqlUtils.CloseConnection();
            return existingUser;
        }
        public void InsertUser(User user, string passwordHash)
        {
            SqlUtils sqlUtils = new SqlUtils();
            string query = "insert into [Users] (Email,Password,RoleId) values(@Email,@Password,@Role)";
            using(SqlConnection connection = sqlUtils.sqlConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlUtils.sqlConnection))
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