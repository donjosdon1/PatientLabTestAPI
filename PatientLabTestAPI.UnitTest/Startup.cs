using PatientLabTestAPI.Repository;

namespace PatientLabTestAPI.UnitTest
{
    public class TestStartup
    {
        public TestStartup()
        {
            var context = new PatientLabTestDbContext();
            RepoInitialize.Initialize(context);
            Register.LoadSampleData.LoadData(context);
        }
    }
}
