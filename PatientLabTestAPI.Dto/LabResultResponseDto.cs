using PatientLabTestAPI.Common;
using PatientLabTestAPI.Models;
using System;

namespace PatientLabTestAPI.Dto
{
    public class LabResultResponseDto
    {
        public long ResultID { get; set; }
        public long SubCategoryID { get; set; }
        public string ResultType { get; set; }
        public int LowRange { get; set; }
        public int HighRange { get; set; }
        public string ResultUnit { get; set; }
        public string ResultDescription { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public LabTestSubCategory LabTestSubCategory { get; set; }        
        public Message Message { get; set; }
    }
}
