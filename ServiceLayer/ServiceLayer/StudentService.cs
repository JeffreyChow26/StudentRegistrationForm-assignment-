using DAL.Repository;
using DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ServiceLayer
{
    public class StudentService
    {
        private readonly IStudentRepository _repository;
        public StudentService(IStudentRepository iStudentRepository)
        {
            this._repository = iStudentRepository;
        }
        public bool VerifyDuplicateInfo(string nid, string emailAddress, string phoneNumber)
        {
            List<Student> extingStudentLst = _repository.FindDuplicateInfo();
            List<Student> inputInformationLst = new List<Student>();
            Student student = new Student()
            {
                NationalIdentityNumber = nid,
                EmailAddress = emailAddress,
                PhoneNumber = phoneNumber
            };
            inputInformationLst.Add(student);
            return extingStudentLst.Any(i => inputInformationLst.Contains(i));
        }
    }
}
