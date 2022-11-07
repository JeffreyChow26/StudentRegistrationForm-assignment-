using Newtonsoft.Json;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ViewModel
{
    public class StudentSummary
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateOfBirth { get; set; }

        [JsonProperty("date")]
        public string CustomDate
        {
            get { return DateOfBirth.ToString("MM/dd/yyyy"); }
            set { DateOfBirth = DateTime.ParseExact(value, "MM/dd/yyyy", null); }
        }
        public string GuardianName { get; set; }
        public string NationalIdentityNumber { get; set; }
        public int UserId { get; set; }
        public string SubjectTaken { get; set; }
        public int TotalMark { get; set; }
        public Status StatusId { get; set; }
        public string StatusName
        {
            get { return StatusId.ToString(); }
            set { StatusName = StatusId.ToString(); }
        }
    }
}
