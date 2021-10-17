using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientLabTestAPI.Models
{
    public class PatientLabResults
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PatientLabResultID { get; set; }
        [Required]
        [ForeignKey("Patient")]
        public long PatientID { get; set; }
        [Required]
        [ForeignKey("LabResult")]
        public long ResultID { get; set; }
        [Required]
        public DateTime CollectionDate { get; set; }
        [Required]
        public string LabLocation { get; set; }
        [Required]
        [MaxLength(100)]
        public string CollectedBy { get; set; }        
        public DateTime TestResultAvailableDate { get; set; }
        [MaxLength(100)]
        public string TestedBy { get; set; }
        public DateTime TestedDate { get; set; }
        public int Result { get; set; }
        [MaxLength(1000)]
        public string Comments { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual LabResult LabResult { get; set; }
    }
}
