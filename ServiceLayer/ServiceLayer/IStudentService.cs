using Repository.Models;
using Repository.ViewModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ServiceLayer
{
    public interface IStudentService
    {
        List<StudentInfoViewModel> GetAllStudentInfo();
        List<ValidationResult> InsertStudentInfo(Student student, int sessionUserId);
        StudentSummary SendStudentSummary(int userId);
    }
}