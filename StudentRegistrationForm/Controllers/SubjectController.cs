﻿using DAL.Models;
using BLL.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistrationForm.Controllers
{
    public class SubjectController : Controller
    {
        private readonly SubjectService _subjectService = new SubjectService(new DAL.Repository.SubjectRepository());
        // GET: Subject
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