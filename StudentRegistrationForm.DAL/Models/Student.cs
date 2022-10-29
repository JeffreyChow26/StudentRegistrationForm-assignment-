using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string GuardianName { get; set; }
        public string EmailAddress { get; set; }
        public string NationalIdentityNumber { get; set; }
        public List<Subject> SubjectsLst { get; set; }  
    }
}