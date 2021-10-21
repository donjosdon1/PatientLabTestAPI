using PatientLabTestAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientLabTestAPI.Models
{
    public class Patient : ModelBase
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
        public string StreetAddress { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Required]
        public int Gender { get; set; }    
        [MaxLength(100)]
        public string EmergencyContactName { get; set; }
        [MaxLength(15)]
        public string EmergencyPhone { get; set; }
        [MaxLength(50)]
        public string EmergencyEmail { get; set; }
        public virtual ICollection<PatientLabResults> PatientLabResults { get; set; }
        [NotMapped]
        public Message Message { get; set; }
    }
}
