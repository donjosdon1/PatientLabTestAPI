using System;
using System.ComponentModel.DataAnnotations;

namespace PatientLabTestAPI.Dto
{
    public class LabTestSubCategoryRequestDto
    {
        public long SubCategoryID { get; set; }
        public long CategoryID { get; set; }
        [Required]
        [MaxLength(100)]
        public string SubCategoryName { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }
}
