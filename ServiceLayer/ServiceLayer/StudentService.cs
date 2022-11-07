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

namespace ServiceLayer.ServiceLayer
{
    public class StudentService
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
            if(errorList.Count == 0)
            {
                int studentId = _repository.InsertStudentInfo(student, sessionUserId);
                _repository.InsertStudentResult(student.Result,studentId);
                return errorList;
            }
            return errorList; 
        }
        public List<StudentInfoViewModel> GetAllStudentInfo()
        {
            List<StudentInfoViewModel> studentEnrolmentInfoLst = _repository.GetAllStudentInfo();
            return studentEnrolmentInfoLst;
        }

        public bool CheckEnrolment(int sessionUerId)
        {
            return _repository.CheckEnrolment(sessionUerId);
        }
    }
}
