using ServiceLayer.ServiceLayer;
using Repository.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Repository.ViewModel;
using System.Web;
using System;

namespace StudentRegistrationForm.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService) => _studentService = studentService;
        public ActionResult EnrolmentForm()
        {
            ClearCache();
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
            return Json(new{ data = result,hasErrors = result.Any(), url = Url.Action("StudentSummary", "StudentSummary") },JsonRequestBehavior.AllowGet);
        }
        public void ClearCache()
        {
            HttpContext.Response.Cache.SetAllowResponseInBrowserHistory(false);
            HttpContext.Response.AddHeader("Cache-Control", "no-cache, no-store");
            HttpContext.Response.AddHeader("Pragma", "no-cache");
            HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetValidUntilExpires(true);
        }
    }
}