using Repository.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceLayer
{
    public class GradeService
    {
        private readonly IGradeRepository _repository;
        public GradeService(IGradeRepository iGradeRepository)
        {
            this._repository = iGradeRepository;
        }

        public List<Grade> DisplayGrades()
        {
            List<Grade> GradesLst = _repository.GetAllGrade();
            return GradesLst;
        }
    }
}
