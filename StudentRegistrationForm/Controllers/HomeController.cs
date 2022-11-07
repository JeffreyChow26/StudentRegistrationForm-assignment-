using Repository.Models;
using ServiceLayer.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistrationForm.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentService _studentService = new StudentService(new Repository.Repository.StudentRepository());

        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login","User");
            }
            else
            {
                if ((int)Session["RoleId"] == (int)Role.admin)
                {
                    return RedirectToAction("Admin", "Admin");
                }
                else if ((int)Session["RoleId"] == (int)Role.user)
                {
                    return RedirectToAction("EnrolmentForm", "Student");
                }
            }
            return View();
        }

    }
}