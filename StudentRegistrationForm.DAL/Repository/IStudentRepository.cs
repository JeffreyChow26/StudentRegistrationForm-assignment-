using Repository.Models;
using Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.Repository
{
    public interface IStudentRepository
    {
        List<StudentInfoViewModel> GetAllStudentInfo();
        StudentSummary GetStudentSummary(int sessionUserId);
        bool FindDuplicateInfo(Student student);
        bool CheckEnrolment(int sessionUserId);
        void InsertStudent(Student student, int sessionUserId);
    }
}