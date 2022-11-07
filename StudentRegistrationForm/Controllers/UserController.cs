using Microsoft.AspNetCore.Http;
using ServiceLayer.ServiceLayer;
using ServiceLayer.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using Microsoft.Ajax.Utilities;

namespace StudentRegistrationForm.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService = new UserService(new Repository.Repository.UserRepository());
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            if (Session["UserId"] != null)
            {
                if((int)Session["RoleId"] == (int)Role.admin)
                {
                    return RedirectToAction("Admin", "Admin");
                }
                else if((int)Session["RoleId"] == (int)Role.user)
                {
                    return RedirectToAction("EnrolmentForm", "Student");
                }
            }
            return View();
        }
        [HttpPost]
        public JsonResult Register(User user)
        {
            List<ValidationResult> result = _userService.Register(user);
            return Json(new{data = result,hasErrors = result.Any(),url = Url.Action("Login", "User")}, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Login(User user)
        { 
            var tuple = _userService.Login(user);
            List<ValidationResult> result = tuple.Item2;
            var existinUser = tuple.Item1;
            var roleId = (int)existinUser.Roles;
            var userId = (int)existinUser.Id;
            var userEmail = existinUser.EmailAddress;
            SetSession(userId, roleId, userEmail);
            return Json(new{data = result,hasErrors = result.Any(),url = Url.Action("Index", "Home")}, JsonRequestBehavior.AllowGet);
        }
        public bool SetSession(int userId, int roleId, string email)
        {
            this.Session["UserId"] = userId;
            this.Session["RoleId"] = roleId;
            this.Session["Email"] = email;
            return true;
        }
        public JsonResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return Json(new { url = Url.Action("Login", "User") }, JsonRequestBehavior.AllowGet);
        }
    }
}