using Repository.Models;
using ServiceLayer.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistrationForm.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeService _gradeService;
        public GradeController(IGradeService gradeService) => _gradeService = gradeService;  
        // GET: Grade
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SendGrade()
        {
            List<Grade> GradesLst = _gradeService.DisplayGrades();
            return Json(GradesLst);
        }

    }
}