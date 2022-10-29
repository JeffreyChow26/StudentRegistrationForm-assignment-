using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.ServiceLayer
{
    public class SubjectService
    {
        private readonly ISubjectRepository _repository;

        public SubjectService(ISubjectRepository iSubjectRepository)
        {
            this._repository = iSubjectRepository;
        }
        public List<Subject> DisplaySubject()
        {
            List<Subject> SubjectLst = _repository.GetAllSubject();

            return SubjectLst;
        }
    }
}