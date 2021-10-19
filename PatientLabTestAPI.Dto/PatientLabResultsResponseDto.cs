using PatientLabTestAPI.Common;
using PatientLabTestAPI.Models;
using System;

namespace PatientLabTestAPI.Dto
{
    public class PatientLabResultsResponseDto
    {
        public long PatientLabResultID { get; set; }
        public long PatientID { get; set; }
        public long ResultID { get; set; }
        public DateTime CollectionDate { get; set; }
        public string LabLocation { get; set; }
        public string CollectedBy { get; set; }
        public DateTime TestResultAvailableDate { get; set; }
        public string TestedBy { get; set; }
        public DateTime TestedDate { get; set; }
        public int Result { get; set; }
        public string Comments { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedBy { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual LabResult LabResult { get; set; }
        public Message Message { get; set; }
    }
}
