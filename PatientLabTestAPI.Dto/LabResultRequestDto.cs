using System;
using System.ComponentModel.DataAnnotations;

namespace PatientLabTestAPI.Dto
{
    public class LabResultRequestDto
    {        
        public long ResultID { get; set; }
        [Required]
        public long SubCategoryID { get; set; }
        [Required]
        [MaxLength(100)]
        public string ResultType { get; set; }
        [Required]
        public int LowRange { get; set; }
        [Required]
        public int HighRange { get; set; }
        [Required]
        public string ResultUnit { get; set; }
        [Required]
        public string ResultDescription { get; set; }
    }
}
