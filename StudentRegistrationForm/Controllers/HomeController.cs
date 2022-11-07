using Repository.Models;
using ServiceLayer.ServiceLayer;
using System.ComponentModel.DataAnnotations;
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
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                if ((int)Session["RoleId"] == (int)Role.admin)
                {
                    return RedirectToAction("Admin", "Admin");
                }
                else if ((int)Session["RoleId"] == (int)Role.user && (bool)Session["isEnroled"] == null)
                {
                    return RedirectToAction("EnrolmentForm", "Student");
                }
                else if ((int)Session["RoleId"] == (int)Role.user && (bool)Session["isEnroled"] == true)
                {
                    return RedirectToAction("StudentSummary", "StudentSummary");
                }
            }
            return RedirectToAction("Login", "User");
        }

    }
}