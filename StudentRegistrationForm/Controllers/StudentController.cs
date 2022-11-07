using ServiceLayer.ServiceLayer;
using Repository.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StudentRegistrationForm.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentService _studentService = new StudentService(new Repository.Repository.StudentRepository());
        // GET: Student
        public ActionResult EnrolmentForm()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        [HttpPost]
        public JsonResult EnrolmentForm(Student student, SubjectResult results)
        {
            int sessionUserId = (int)Session["UserId"];
            List<ValidationResult> result = _studentService.InsertStudentInfo(student, sessionUserId);
            return Json(new{ data = result,hasErrors = result.Any()},JsonRequestBehavior.AllowGet);
        }
    }
}