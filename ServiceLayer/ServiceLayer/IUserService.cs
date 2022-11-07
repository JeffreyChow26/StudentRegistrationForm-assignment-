using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ServiceLayer
{
    public interface IUserService
    {
        int GetSessionUserId(User user);
        Tuple<User, List<ValidationResult>> Login(User user);
        List<ValidationResult> Register(User user);
        bool isEnrolled(int userId);
    }
}