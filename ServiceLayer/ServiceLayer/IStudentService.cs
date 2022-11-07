using Repository.Models;
using Repository.ViewModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ServiceLayer
{
    public interface IStudentService
    {
        bool CheckEnrolment(int sessionUerId);
        List<StudentInfoViewModel> GetAllStudentInfo();
        List<ValidationResult> InsertStudentInfo(Student student, int sessionUserId);
    }
}