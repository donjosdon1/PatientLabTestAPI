using PatientLabTestAPI.Common;
using System;

namespace PatientLabTestAPI.Dto
{
    public class LabTestCategoryResponseDto
    {
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public Message Message { get; set; }
    }
}
