using Microsoft.AspNetCore.Http;
using BLL.ServiceLayer;
using BLL.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationForm.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService = new UserService(new DAL.Repository.UserRepository());
        public ActionResult Register()
        {

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Register(User user)
        {
            List<ValidationResult> result = _userService.Register(user);
            return Json(new
            {
                data = result,
                hasErrors = result.Any(),
                url = Url.Action("Login", "User")

            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Login(User user)
        {
            List<ValidationResult> result = _userService.Login(user);

            if (result.Count == 0)
            {
                this.Session["UserId"] = user.Id;
                this.Session["UserEmail"] = user.EmailAddress;
            }
            return Json(new
            {
                data = result,
                hasErrors = result.Any(),
                url = Url.Action("RegisterForm", "Student")

            }, JsonRequestBehavior.AllowGet);
        }
    }
}