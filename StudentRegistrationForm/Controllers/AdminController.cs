using Repository.Models;
using Repository.ViewModel;
using ServiceLayer.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistrationForm.Controllers
{
    public class AdminController : Controller
    {
        private readonly StudentService _studentService = new StudentService(new Repository.Repository.StudentRepository());
        public ActionResult Admin()
        {
            if (Session["UserId"] == null || (int)Session["RoleId"] != (int)Role.admin)
            {
                return RedirectToAction("EnrolmentForm", "Student");
            }
            return View();
        }
        [HttpPost]
        public JsonResult GetStudentInfo()
        {
            List<StudentInfoViewModel> studentEnrolmentInfoLst = _studentService.GetAllStudentInfo();
            return Json(studentEnrolmentInfoLst);
        }
    }
}