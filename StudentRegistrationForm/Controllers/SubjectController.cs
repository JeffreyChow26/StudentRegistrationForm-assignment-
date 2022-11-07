using Repository.Models;
using ServiceLayer.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistrationForm.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectService _subjectService;
        public SubjectController(ISubjectService subjectService) => _subjectService = subjectService;
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SendSubject()
        {
            List<Subject> SubjectLst = _subjectService.DisplaySubject();
            return Json(SubjectLst);
        }
    }
}