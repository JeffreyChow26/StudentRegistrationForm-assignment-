using Repository.Models;
using Repository.Repository;
using StackExchange.Profiling.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace ServiceLayer.Validation
{
    public class ValidateInfo
    {
        private readonly IUserRepository _repository;
        public ValidateInfo(IUserRepository iUserRepository)
        {
            this._repository = iUserRepository;
        }
        public List<ValidationResult> ValidateRegister(User user)
        {
            
            List<ValidationResult> errorList = new List<ValidationResult>();

            User existingUser = _repository.FindUser(user.EmailAddress);
            if (existingUser.EmailAddress != null)
            {
                errorList.Add(new ValidationResult("User already exist."));
            }
            else
            {
                CheckEmailValid(user, errorList);
                CheckPassword(user, errorList);
            }
            return errorList;
        }


        public Tuple <User, List<ValidationResult>> ValidateLogin(User user)
        {
            List<ValidationResult> errorList = new List<ValidationResult>();
            User existingUser = _repository.FindUser(user.EmailAddress);

            var tuple = new Tuple<User, List<ValidationResult>>(existingUser, errorList);
            if (existingUser.EmailAddress == null)
            {
                errorList.Add(new ValidationResult("User does not exist."));
                return tuple;
            }
            else if (!Crypto.VerifyHashedPassword(existingUser.Password, user.Password))
            {
                errorList.Add(new ValidationResult("Password or Email Address is invalid"));
            }
            return tuple;
        }

        public void CheckEmailValid(User user, List<ValidationResult> errorList)
        {
            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            if (!Regex.IsMatch(user.EmailAddress, pattern, RegexOptions.IgnoreCase) && !user.EmailAddress.IsNullOrWhiteSpace())
            {
                errorList.Add(new ValidationResult("Please enter a valid email address."));
            }
            else if(user.EmailAddress.IsNullOrWhiteSpace())
            {
                errorList.Add(new ValidationResult("Email Address is required"));
            }
        }

        public void CheckPassword(User user, List<ValidationResult> errorList)
        {
            if(user.Password.IsNullOrWhiteSpace())
            {
                errorList.Add(new ValidationResult("Password is required."));
            }
            else if(user.Password.Length <= 5)
            {
                errorList.Add(new ValidationResult("Please enter a stronger password."));
            }
        }
    }
}
