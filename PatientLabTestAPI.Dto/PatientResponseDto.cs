using PatientLabTestAPI.Common;
using PatientLabTestAPI.Models;
using System;

namespace PatientLabTestAPI.Dto
{
    public class PatientResponseDto
    {
        public long PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public int Gender { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyPhone { get; set; }
        public string EmergencyEmail { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public virtual PatientContact PatientPrimaryContact { get; set; }
        public Message Message { get; set; }
    }
}
