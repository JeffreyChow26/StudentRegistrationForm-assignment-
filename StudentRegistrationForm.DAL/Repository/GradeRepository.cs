using Repository.CommonUtils;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class GradeRepository : IGradeRepository
    {
        public List<Grade> GetAllGrade()
        {
            List<Grade> GradesLst = new List<Grade>();
            SqlUtils sqlUtils = new SqlUtils();
            SqlCommand sqlCommand = new SqlCommand(SqlDbCommand.SelectGradeQuery, sqlUtils.sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Grade grade = new Grade()
                {
                    Mark = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
                GradesLst.Add(grade);
            }
            return GradesLst;
        }
    }

}
