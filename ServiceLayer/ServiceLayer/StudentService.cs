using Repository.Repository;
using Repository.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ServiceLayer.Validation;
using Repository.ViewModel;
using System.Transactions;

namespace ServiceLayer.ServiceLayer
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;
        public StudentService(IStudentRepository iStudentRepository)
        {
            this._repository = iStudentRepository;
        }
        public List<ValidationResult> InsertStudentInfo(Student student, int sessionUserId)
        {
            ValidateStudentInfo validateStudentInfo = new ValidateStudentInfo(_repository);
            List<ValidationResult> errorList = validateStudentInfo.ValidateEnrolmentForm(student);
            if (errorList.Count == 0)
            {
                _repository.InsertStudent(student, sessionUserId);              
                return errorList;
            }
            return errorList;
        }
        public List<StudentInfoViewModel> GetAllStudentInfo()
        {
            List<StudentInfoViewModel> studentEnrolmentInfoLst = _repository.GetAllStudentInfo();
            return studentEnrolmentInfoLst;
        }
        public StudentSummary SendStudentSummary(int sessionUserId)
        {
            StudentSummary studentSummary = _repository.GetStudentSummary(sessionUserId);
            return studentSummary;
        }
    }
}
