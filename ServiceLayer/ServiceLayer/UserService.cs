using Repository.Models;
using Repository.Repository;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Diagnostics;
using ServiceLayer.Validation;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Umbraco.Core.Models.Membership;
using User = Repository.Models.User;
using System;

namespace ServiceLayer.ServiceLayer
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IStudentRepository _studentRepository;
        public UserService(IUserRepository iUserRepository, IStudentRepository studentRepository)
        {
            this._repository = iUserRepository;
            _studentRepository = studentRepository;
        }
        public List<ValidationResult> Register(User user)
        {
            ValidateInfo validateInfo = new ValidateInfo(_repository);
            List<ValidationResult> errorList = validateInfo.ValidateRegister(user);
            if (errorList.Count == 0)
            {
                string HashPassword = Crypto.HashPassword(user.Password);
                _repository.InsertUser(user, HashPassword);
            }
            return errorList;
        }
        public Tuple<User, List<ValidationResult>> Login(User user)
        {
            ValidateInfo validateInfo = new ValidateInfo(_repository);
            var tuple = validateInfo.ValidateLogin(user);
            return tuple;
        }
        public int GetSessionUserId(User user)
        {
            User existingUser = _repository.FindUser(user.EmailAddress);
            int sessionUserId = existingUser.Id;
            return sessionUserId;
        }
        public bool isEnrolled(int userId)
        {
            return _studentRepository.CheckEnrolment(userId);
        }
    }
}