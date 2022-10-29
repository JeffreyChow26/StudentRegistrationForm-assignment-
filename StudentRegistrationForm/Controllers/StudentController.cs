using BLL.ServiceLayer;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistrationForm.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService _studentService = new StudentService(new DAL.Repository.StudentRepository());
        // GET: Student
        public ActionResult RegisterForm()
        {
            return View();
        }

        [HttpPost]
        public JsonResult RegisterForm(Student student)
        {
            bool submit = _studentService.VerifyDuplicateInfo(student.NationalIdentityNumber, student.EmailAddress, student.PhoneNumber);

            if(submit == true){
                Debug.WriteLine("no duplicate info");
            }
            else{
                Debug.WriteLine("duplicate info found");
            }

            return Json(new { result = true});
        }
    }
}