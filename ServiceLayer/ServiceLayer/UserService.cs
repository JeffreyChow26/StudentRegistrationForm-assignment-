using DAL.Models;
using DAL.Repository;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Diagnostics;
using BLL.Validation;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace BLL.ServiceLayer
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository iUserRepository)
        {
            this._repository = iUserRepository;
        }

        public List<ValidationResult> Register(User user)
        {
            ValidateInfo validateInfo = new ValidateInfo(_repository);
            List<ValidationResult> errorList = validateInfo.ValidateRegister(user);

            if(errorList.Count == 0)
            {
                string HashPassword = Crypto.HashPassword(user.Password);
                _repository.InsertUser(user, HashPassword);
            }

            return errorList;

        }

        public List<ValidationResult> Login(User user)
        {
            ValidateInfo validateInfo = new ValidateInfo(_repository);
            List<ValidationResult> errorList = validateInfo.ValidateLogin(user);
            return errorList;
        }

    }
}