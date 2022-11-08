using Repository.Models;
using Repository.ViewModel;
using ServiceLayer.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistrationForm.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStudentService _studentService;
        public AdminController(IStudentService studentService) => _studentService = studentService;

        public ActionResult Admin()
        {
            ClearCache();
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