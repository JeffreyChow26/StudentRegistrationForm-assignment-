using DAL.CommonUtils;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL.Repository
{
    public class StudentRepository : IStudentRepository
    {
        public List<Student> GetAllStudent()
        {
            List<Student> subjectLst = new List<Student>();
            SqlUtils sqlUtils = new SqlUtils();
            string selectAllQuery = "select ,StudentId,Name,Surname,PhoneNumber,DateOfBirth,GuardianName,EmailAddress,NationalIdentityNumber from [Subject]";
            SqlCommand sqlCommand = new SqlCommand(selectAllQuery, sqlUtils.sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Student student = new Student()
                {
                    Id = reader.GetInt32(0)

                };
            }

            return subjectLst;
        }

        public List<Student> FindDuplicateInfo()
        {
            List<Student> extingStudentLst = GetAllStudent();
            return extingStudentLst;
        }

        public bool InsertInfo(Student student)
        {
            SqlUtils sqlUtils = new SqlUtils();
            string query = "insert into [Student] (Name,StudentId) values(@Name,@StudentId)";

            using (SqlConnection connection = sqlUtils.sqlConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlUtils.sqlConnection))
                {
                    //sqlCommand.Parameters.AddWithValue("@Name", student.Id);
                    sqlCommand.ExecuteNonQuery();
                }

            }
            sqlUtils.CloseConnection();
            return true;
        }
    }
}