using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PatientLabTestAPI.Dto
{
    public class PatientLabResultsRequestDto
    {
        public long PatientLabResultID { get; set; }
        [Required]
        public long PatientID { get; set; }
        [Required]
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
    }
}
