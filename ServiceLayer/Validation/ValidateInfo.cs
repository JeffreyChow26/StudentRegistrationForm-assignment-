using Repository.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
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
    }
}
