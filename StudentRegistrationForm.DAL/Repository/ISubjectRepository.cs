using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repository.Repository
{
    public interface ISubjectRepository
    {
        List<Subject> GetAllSubject();
    }
}