using System;
using System.ComponentModel.DataAnnotations;

namespace PatientLabTestAPI.Dto
{
    public class PatientRequestDto
    {
        public long PatientID { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        [Required]
        public int Gender { get; set; }
        [MaxLength(100)]
        public string EmergencyContactName { get; set; }
        [MaxLength(15)]
        public string EmergencyPhone { get; set; }
        [MaxLength(50)]
        public string EmergencyEmail { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public virtual PatientContactDto PatientPrimaryContact { get; set; }
    }
}
