using Repository.CommonUtils;
using Repository.Models;
using Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Repository.Repository
{
    public class StudentRepository : IStudentRepository
    {
        public bool FindDuplicateInfo(Student student)
        {
            SqlUtils sqlUtils = new SqlUtils();
            using (SqlConnection sqlConnection = sqlUtils.sqlConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(SqlDbCommand.SelectExistingStudentInfo, sqlUtils.sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@NationalIdentityNumber", student.NationalIdentityNumber);
                    sqlCommand.Parameters.AddWithValue("@EmailAddress", student.EmailAddress);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                    object row = sqlCommand.ExecuteScalar();
                    if(row == null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public List<StudentInfoViewModel> GetAllStudentInfo()
        {
            List<StudentInfoViewModel> studentEnrolmentInfoLst = new List<StudentInfoViewModel>();
            SqlUtils sqlUtils = new SqlUtils();
            SqlCommand sqlCommand = new SqlCommand(@";WITH CTE
                                                        AS
                                                        (
                                                            SELECT stu.StudentId,stu.Firstname as FirstName,stu.Surname, stu.Address, stu.PhoneNumber, stu.EmailAddress,stu.DateOfBirth, stu.GuardianName, stu.NationalIdentityNumber, stu.UserId, sub.SubjectName as SubjectName, srt.Mark, stu.StatusId
                                                            FROM Student as stu inner JOIN (SubjectResult as srt Inner JOIN Subject as sub on srt.SubjectId = sub.SubjectId) on stu.StudentId = srt.StudentId
                                                        )          
                                                        SELECT DISTINCT(i1.StudentId),i1.FirstName, i1.Surname, i1.Address, i1.PhoneNumber, i1.EmailAddress ,i1.DateOfBirth, i1.GuardianName, i1.NationalIdentityNumber, i1.UserId, STUFF(
                                                                    (SELECT
                                                                        ', ' + SubjectName
                                                                        FROM CTE i2 WHERE i1.StudentId = i2.StudentId
                                                                        FOR XML PATH(''))
                                                                    ,1,2, ''
                                                                ) as SubjectsTaken, SUM(i1.Mark) as TotalMark, i1.StatusId     
    
                                                        FROM CTE i1
                                                        GROUP BY i1.StudentId, i1.FirstName, i1.Surname, i1.Address,  i1.EmailAddress ,i1.PhoneNumber, i1.DateOfBirth, i1.GuardianName, i1.NationalIdentityNumber, i1.UserId, i1.StatusId
                                                        ORDER BY i1.StatusId DESC, TotalMark DESC
                                                    ", sqlUtils.sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                StudentInfoViewModel allStudentInformation = new StudentInfoViewModel()
                {
                    StudentId = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    Surname = reader.GetString(2),
                    Address = reader.GetString(3),
                    PhoneNumber = reader.GetString(4),
                    EmailAddress = reader.GetString(5),
                    DateOfBirth = reader.GetDateTime(6),
                    GuardianName = reader.GetString(7),
                    NationalIdentityNumber = reader.GetString(8),
                    UserId = reader.GetInt32(9),
                    SubjectTaken = reader.GetString(10),
                    TotalMark = reader.GetInt32(11),
                    StatusId = (Status)reader.GetInt32(12),
                };
                studentEnrolmentInfoLst.Add(allStudentInformation);
            }
            return studentEnrolmentInfoLst;
        }
        public int InsertStudentInfo(Student student, int sessionUserId)
        {
            int studentId = 0;
            SqlUtils sqlUtils = new SqlUtils();
            using (SqlConnection connection = sqlUtils.sqlConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(SqlDbCommand.InsertStudentInfoQuery, sqlUtils.sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@FirstName", student.Name);
                    sqlCommand.Parameters.AddWithValue("@Surname", student.Surname);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                    sqlCommand.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                    sqlCommand.Parameters.AddWithValue("@GuardianName", student.GuardianName);
                    sqlCommand.Parameters.AddWithValue("@EmailAddress", student.EmailAddress);
                    sqlCommand.Parameters.AddWithValue("@NationalIdentityNumber", student.NationalIdentityNumber);
                    sqlCommand.Parameters.AddWithValue("@Address", student.Address);
                    sqlCommand.Parameters.AddWithValue("@UserId", sessionUserId);
                    sqlCommand.Parameters.AddWithValue("@StatusId", Status.pending);
                    object studentIdObj = sqlCommand.ExecuteScalar();
                    if(studentIdObj != null)
                    {
                        studentId = int.Parse(studentIdObj.ToString());
                    }
                }
            }
            sqlUtils.CloseConnection();
            return studentId;
        }
        public void InsertStudentResult(List<SubjectResult> results, int studentId )
        {
            SqlUtils sqlUtils = new SqlUtils();
            DataTable studentTable = new DataTable();
            studentTable.Columns.Add(new DataColumn("SubjectId", typeof(Int32)));
            studentTable.Columns.Add(new DataColumn("StudentId", typeof(Int32)));
            studentTable.Columns.Add(new DataColumn("GradeId", typeof(Int32)));
            foreach (SubjectResult result in results)
            {
                DataRow row = studentTable.NewRow();
                row["SubjectId"] = result.SubjectId;
                row["GradeId"] = result.GradeId;
                row["StudentId"] = studentId;
                studentTable.Rows.Add(row);
            }
            if (studentTable.Rows.Count > 0)
            {
                using (SqlConnection connection = sqlUtils.sqlConnection)
                {
                    SqlTransaction transaction = connection.BeginTransaction();
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.FireTriggers, transaction))
                    {
                        sqlBulkCopy.DestinationTableName = "dbo.SubjectResult";
                        sqlBulkCopy.ColumnMappings.Add("SubjectId", "SubjectId");
                        sqlBulkCopy.ColumnMappings.Add("GradeId", "Mark");
                        sqlBulkCopy.ColumnMappings.Add("StudentId", "StudentId");
                        sqlBulkCopy.WriteToServer(studentTable);
                        transaction.Commit();
                        sqlUtils.CloseConnection();
                    }
                }
            }
        }
        public bool CheckEnrolment(int sessionUserId)
        {
            SqlUtils sqlUtils = new SqlUtils();
            bool isEnrolled = false;
            using (SqlConnection sqlConnection = sqlUtils.sqlConnection)
            {
                using (SqlCommand sqlCommand = new SqlCommand(SqlDbCommand.FindEnrolledUserQuery, sqlUtils.sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@UserId", sessionUserId);
                    int row = sqlCommand.ExecuteNonQuery();
                    if(row > 0)
                    {
                        return isEnrolled = true;
                    }
                }
            }
            return isEnrolled;
        }
    }
}