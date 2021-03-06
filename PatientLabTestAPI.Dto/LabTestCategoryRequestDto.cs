using System;
using System.ComponentModel.DataAnnotations;

namespace PatientLabTestAPI.Dto
{
    public class LabTestCategoryRequestDto
    {
        public long CategoryID { get; set; }
        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
