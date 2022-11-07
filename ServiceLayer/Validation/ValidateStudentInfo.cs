using Repository.Models;
using Repository.Repository;
using StackExchange.Profiling.Internal;
using Superpower.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;

namespace ServiceLayer.Validation
{
    public class ValidateStudentInfo
    {
        private readonly IStudentRepository _repository;
        public ValidateStudentInfo(IStudentRepository iStudentRepository)
        {
            this._repository = iStudentRepository;
        }
        public List<ValidationResult> ValidateEnrolmentForm(Student student)
        {
            List<ValidationResult> errorList = new List<ValidationResult>();
            bool isDuplicated = _repository.FindDuplicateInfo(student);
            if (isDuplicated == true)
            {
                errorList.Add(new ValidationResult("NID / Emaill Address / Phone Number already exist."));
                return errorList;
            }
            else
            {
                if(IsStudentInfoEmpty(student) == false)
                {
                    errorList.Add(new ValidationResult("Please fill the enrolment form before submitting."));
                }
                if(isSubjectDuplicated(student) == true)
                {
                    errorList.Add(new ValidationResult("Cannot choose the same subject multiple time."));
                }
            }
            return errorList;
        }
        public bool IsStudentInfoEmpty(Student student)
        {
            if (Object.ReferenceEquals(student, null))
                return true;
            return student.GetType().GetProperties()
                .Any(x => IsNullOrEmpty(x.GetValue(student)));
        }
        private static bool IsNullOrEmpty(object value)
        {
            if (Object.ReferenceEquals(value, null))
                return true;

            var type = value.GetType();
            return type.IsValueType
                && Object.Equals(value, Activator.CreateInstance(type));
        }
        private bool isSubjectDuplicated(Student student)
        {
            var resultLst = student.Result;
            foreach(var subjectOne in resultLst)
            {
                foreach(var subjectTwo in resultLst)
                {
                    if(subjectOne.SubjectId == subjectTwo.SubjectId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
