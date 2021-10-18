using PatientLabTestAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientLabTestAPI.Models
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public virtual PatientContact PatientPrimaryContact { get; set; }
        public virtual PatientContact PatientEmergencyContact { get; set; }
        public virtual ICollection<PatientLabResults> PatientLabResults { get; set; }
        [NotMapped]
        public Message Message { get; set; }
    }
}
