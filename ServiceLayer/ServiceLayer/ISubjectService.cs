using Repository.Models;
using Repository.ViewModel;
using System.Collections.Generic;

namespace ServiceLayer.ServiceLayer
{
    public interface ISubjectService
    {
        List<Subject> DisplaySubject();
    }
}