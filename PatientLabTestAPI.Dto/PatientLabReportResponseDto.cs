using System;

namespace PatientLabTestAPI.Dto
{
    public class PatientLabReportResponseDto
    {
        public long PatientLabResultID { get; set; }
        public long PatientID { get; set; }
        public long ResultID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ResultType { get; set; }
        public int LowRange { get; set; }
        public int HighRange { get; set; }
        public string ResultUnit { get; set; }
        public string ResultDescription { get; set; }
        public DateTime CollectionDate { get; set; }
        public string LabLocation { get; set; }
        public string CollectedBy { get; set; }
        public DateTime TestResultAvailableDate { get; set; }
        public string TestedBy { get; set; }
        public DateTime TestedDate { get; set; }
        public int Result { get; set; }
        public string Comments { get; set; }
    }
}
