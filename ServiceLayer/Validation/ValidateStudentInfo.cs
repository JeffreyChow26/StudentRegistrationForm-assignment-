using Repository.Models;
using Repository.Repository;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using Umbraco.Core.Models.Membership;

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
                if(isPhoneNumberValid(student) == false)
                {
                    errorList.Add(new ValidationResult("Please enter a valid phone number."));
                }
                CheckEmailValid(student, errorList);
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
            var duplicatesSubject = resultLst.GroupBy(s => s.SubjectId).Where(g => g.Count() > 1).Select(g => g.Key);
            if(duplicatesSubject.Count() > 0)
            {
                return true;
            }
            return false;
        }
        public void CheckEmailValid(Student student, List<ValidationResult> errorList)
        {
            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            if (!Regex.IsMatch(student.EmailAddress, pattern, RegexOptions.IgnoreCase) && !student.EmailAddress.IsNullOrWhiteSpace())
            {
                errorList.Add(new ValidationResult("Please enter a valid email address."));
            }
            else if (student.EmailAddress.IsNullOrWhiteSpace())
            {
                errorList.Add(new ValidationResult("Email Address is required"));
            }
        }
        public bool isPhoneNumberValid(Student student)
        {
            if(student.PhoneNumber.Length > 7 && student.PhoneNumber.Trim().Length > 0)
            {
                return false;
            }
            return true;
        }
    }
}
