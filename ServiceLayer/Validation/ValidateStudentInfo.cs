using Repository.Models;
using Repository.Repository;
using StackExchange.Profiling.Internal;
using Superpower.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
            return errorList;
        }
    }
}
