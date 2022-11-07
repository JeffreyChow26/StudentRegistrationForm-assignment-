using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public interface IGradeRepository
    {
        List<Grade> GetAllGrade();
    }
}
