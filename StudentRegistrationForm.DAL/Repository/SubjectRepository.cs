using Repository.CommonUtils;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Repository.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        public List<Subject> GetAllSubject()
        {
            List<Subject> SubjectLst = new List<Subject>();
            SqlUtils sqlUtils = new SqlUtils();
            
            SqlCommand sqlCommand = new SqlCommand(SqlDbCommand.SelectAllSubjectQuery, sqlUtils.sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Subject subject = new Subject()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
                SubjectLst.Add(subject);
            }

            return SubjectLst;
        }
    }
}