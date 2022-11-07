using Repository.Models;
using Repository.ViewModel;
using ServiceLayer.ServiceLayer;
using System.Collections.Generic;
using System.Web.Mvc;

namespace StudentRegistrationForm.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStudentService _studentService;
        public AdminController(IStudentService studentService) => _studentService = studentService;

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