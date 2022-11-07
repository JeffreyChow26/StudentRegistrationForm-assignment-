using Repository.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Repository.CommonUtils
{
    public static class SqlDbCommand
    {
        public static string SelectUserQuery = "select EmailAddress,Password from [Users]";

        public static string FindUserQuery = "select top 1 UserId, Email, Password, RoleId from [Users] where Email = @Email";

        public static string InsertUserQuery = "insert into [Users] (Email,Password,RoleId) values(@Email,@Password,@Role)";

        public static string SelectGradeQuery = "select Mark, Grade from [SubjectGrades]";

        public static string SelectAllSubjectQuery = "select SubjectId, SubjectName from [Subject]";

        public static string SelectExistingStudentInfo = "select Top 1 NationalIdentityNumber,EmailAddress, PhoneNumber from [Student] where NationalIdentityNumber = @NationalIdentityNumber or " +
                                                         "EmailAddress = @EmailAddress or PhoneNumber = @PhoneNumber";

        public static string InsertStudentInfoQuery = "insert into [Student] (FirstName,Surname,PhoneNumber,DateOfBirth,GuardianName,EmailAddress,NationalIdentityNumber,Address,UserId,StatusId)" +
                                                     " values(@FirstName,@Surname,@PhoneNumber,@DateOfBirth,@GuardianName,@EmailAddress,@NationalIdentityNumber, @Address, @UserId, @StatusId); SELECT SCOPE_IDENTITY();";

        public static string FindEnrolledUserQuery = "select UserId [Student] where Email = @UserId";

        public static string GetAllStudentInfoQuery = "; WITH CTE " +
                                                        "AS " +
                                                        "(SELECT stu.StudentId, stu.Firstname as FirstName, stu.Surname, stu.Address, stu.PhoneNumber, stu.DateOfBirth, stu.GuardianName, stu.NationalIdentityNumber, stu.UserId, sub.SubjectName as SubjectName, srt.Mark, stu.StatusId " +
                                                        " FROM Student as stu inner JOIN (SubjectResult as srt Inner JOIN Subject as sub on srt.SubjectId = sub.SubjectId) on stu.StudentId = srt.StudentId)" +
                                                        "SELECT DISTINCT(i1.StudentId),i1.FirstName, i1.Surname, i1.Address, i1.PhoneNumber, i1.DateOfBirth, i1.GuardianName, i1.NationalIdentityNumber, i1.UserId, " +
                                                        "STUFF(" +
                                                        "(SELECT" +
                                                        " ', ' + SubjectName" +
                                                        "FROM CTE as i2 WHERE i1.StudentId = i2.StudentId" +
                                                        "FOR XML PATH(''))" +
                                                        ",1,2, ''" +
                                                        ") as SubjectsTaken, SUM(i1.Mark) as TotalMark, i1.StatusId" +
                                                        "FROM CTE i1" +
                                                        "GROUP BY i1.StudentId, i1.FirstName, i1.Surname, i1.Address, i1.PhoneNumber, i1.DateOfBirth, i1.GuardianName, i1.NationalIdentityNumber, i1.UserId, i1.StatusId" +
                                                        "ORDER BY i1.StatusId DESC, TotalMark DESC";
    }
}
