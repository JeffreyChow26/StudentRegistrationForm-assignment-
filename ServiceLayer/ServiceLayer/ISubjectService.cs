using Repository.Models;
using System.Collections.Generic;

namespace ServiceLayer.ServiceLayer
{
    public interface ISubjectService
    {
        List<Subject> DisplaySubject();
    }
}