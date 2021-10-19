using PatientLabTestAPI.Common;
using PatientLabTestAPI.Models;
using System;

namespace PatientLabTestAPI.Dto
{
    public class LabTestSubCategoryResponseDto
    {
        public long SubCategoryID { get; set; }
        public long CategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public virtual LabTestCategory LabTestCategory { get; set; }
        public Message Message { get; set; }
    }
}
