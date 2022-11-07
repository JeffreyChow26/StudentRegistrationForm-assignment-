using Repository.ViewModel;
using ServiceLayer.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistrationForm.Controllers
{   
    public class StudentSummaryController : Controller
    {
        private readonly IStudentService _studentService;
        public StudentSummaryController(IStudentService studentService) => _studentService = studentService;
        public ActionResult StudentSummary()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
  
        [HttpPost]
        public JsonResult GetStudentSummary()
        {
            int sessionUserId = (int)Session["UserId"];
            StudentSummary studentSummary = _studentService.SendStudentSummary(sessionUserId);
            return Json(studentSummary);
        }
    }
}