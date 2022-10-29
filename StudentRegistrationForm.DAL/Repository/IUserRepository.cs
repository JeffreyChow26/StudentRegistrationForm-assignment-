﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Repository
{
    public interface IUserRepository
    {
        List<User> GetAllUser();
        User FindUser(string email);
        void InsertUser(User user, string passwordHash);
    }
}