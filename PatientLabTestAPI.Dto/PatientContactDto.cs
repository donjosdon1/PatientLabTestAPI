namespace PatientLabTestAPI.Dto
{
    public class PatientContactDto
    {
        public long ContactID { get; set; }
        public long PatientID { get; set; }
        public string StreetAddress { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ContactType { get; set; }
    }
}
