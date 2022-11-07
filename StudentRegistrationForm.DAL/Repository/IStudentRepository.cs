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
        bool FindDuplicateInfo(Student student);
        int InsertStudentInfo(Student student, int sessionUserId);
        void InsertStudentResult(List<SubjectResult> result, int studentId);
        bool CheckEnrolment(int sessionUserId);
    }
}