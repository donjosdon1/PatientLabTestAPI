namespace PatientLabTestAPI.Repository
{
    public static class RepoInitialize
    {
        public static void Initialize(PatientLabTestDbContext patientLabTestDbContext)
        {
            patientLabTestDbContext.Database.EnsureDeletedAsync();
            patientLabTestDbContext.Database.EnsureCreatedAsync();            
        }
    }
}
