using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Repository
{
    public interface IStudentRepository
    {
        List<Student> GetAllStudent();
        List<Student> FindDuplicateInfo();
        bool InsertInfo(Student student);
    }
}