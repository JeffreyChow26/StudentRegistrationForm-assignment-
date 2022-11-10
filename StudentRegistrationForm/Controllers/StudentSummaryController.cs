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
            ClearCache();
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        } 
        public ActionResult StudentSummaryDojo()
        {
            ClearCache();
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