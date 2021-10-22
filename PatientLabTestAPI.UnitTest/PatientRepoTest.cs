using Microsoft.Extensions.Logging;
using Moq;
using PatientLabTestAPI.Models;
using PatientLabTestAPI.Repository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PatientLabTestAPI.UnitTest
{
    public class PatientRepoTest : TestStartup
    {
        private readonly PatientLabTestDbContext context = new();
        private readonly ILogger<PatientRepo> logger = new Mock<ILogger<PatientRepo>>().Object;
        private readonly RepoCommon<Patient> repoCommon = new Mock<RepoCommon<Patient>>(new Mock<ILogger<Patient>>().Object).Object;

        [Theory]
        [InlineData("Test001", "Test 1", "test", "test", "01/01/2001", "aaa@test000001.com", 1, "SSSS001", "11111111111")]
        [InlineData("Test002", "Test 2", "test", "test", "01/01/2001", "aaa@test000002.com", 1, "SSSS002", "12111111111")]
        [InlineData("Test003", "Test 3", "test", "test", "01/01/2001", "aaa@test000003.com", 1, "SSSS003", "11311111111")]
        [InlineData("Test004", "Test 4", "test", "test", "01/01/2001", "aaa@test000004.com", 1, "SSSS004", "11141111111")]
        [InlineData("Test005", "Test 5", "test", "test", "01/01/2001", "aaa@test000005.com", 1, "SSSS005", "11111511111")]
        [InlineData("Test006", "Test 6", "test", "test", "01/01/2001", "aaa@test000006.com", 1, "SSSS006", "11111115111")]
        public async Task CreatePatientTest(string fName, string lName, string addr, string city, DateTime dob, string email, int gender, string contactName, string phone)
        {
            var repo = new PatientRepo(context, logger, repoCommon);
            //create 
            var patient = new Patient { FirstName = fName, LastName = lName, Address2 = addr, City = city, DOB = dob, Email = email, Gender = gender, EmergencyContactName = contactName, Phone = phone };
            await repo.CreateRecord(patient);

            //check the newly created on by fetching from the DB        
            var dataByKey = await repo.GetDataByKey(patient.PatientID);
            Assert.True(dataByKey.PatientID == patient.PatientID && patient.FirstName == dataByKey.FirstName &&
                        dataByKey.LastName == patient.LastName && dataByKey.Email == patient.Email);
        }

        [Theory]
        [InlineData("Test001", "Test 1", "test", "test", "01/01/2001", "aaa@test000001.com", 1, "SSSS001", "11111111111")]
        [InlineData("Test002", "Test 2", "test", "test", "01/01/2001", "aaa@test000002.com", 1, "SSSS002", "12111111111")]
        [InlineData("Test003", "Test 3", "test", "test", "01/01/2001", "aaa@test000003.com", 1, "SSSS003", "11311111111")]
        [InlineData("Test004", "Test 4", "test", "test", "01/01/2001", "aaa@test000004.com", 1, "SSSS004", "11141111111")]
        [InlineData("Test005", "Test 5", "test", "test", "01/01/2001", "aaa@test000005.com", 1, "SSSS005", "11111511111")]
        [InlineData("Test006", "Test 6", "test", "test", "01/01/2001", "aaa@test000006.com", 1, "SSSS006", "11111115111")]
        public async Task UpdatePatientTest(string fName, string lName, string addr, string city, DateTime dob, string email, int gender, string contactName, string phone)
        {
            var repo = new PatientRepo(context, logger, repoCommon);
            //create 
            var patient = new Patient { FirstName = fName, LastName = lName, Address2 = addr, City = city, DOB = dob, Email = email, Gender = gender, EmergencyContactName = contactName, Phone = phone };
            await repo.CreateRecord(patient);
            patient.FirstName = "Updated Test";
            patient.LastName = "Updaed Desc2";
            await repo.UpdateRecord(patient);

            //check the newly created on by fetching from the DB        
            var dataByKey = await repo.GetDataByKey(patient.PatientID);
            Assert.True(dataByKey.PatientID == patient.PatientID && patient.FirstName == dataByKey.FirstName &&
                        dataByKey.LastName == patient.LastName && dataByKey.Email == patient.Email);
        }

        [Theory]
        [InlineData("Test001", "Test 1", "test", "test", "01/01/2001", "aaa@test000001.com", 1, "SSSS001", "11111111111")]
        [InlineData("Test002", "Test 2", "test", "test", "01/01/2001", "aaa@test000002.com", 1, "SSSS002", "12111111111")]
        [InlineData("Test003", "Test 3", "test", "test", "01/01/2001", "aaa@test000003.com", 1, "SSSS003", "11311111111")]
        [InlineData("Test004", "Test 4", "test", "test", "01/01/2001", "aaa@test000004.com", 1, "SSSS004", "11141111111")]
        [InlineData("Test005", "Test 5", "test", "test", "01/01/2001", "aaa@test000005.com", 1, "SSSS005", "11111511111")]
        [InlineData("Test006", "Test 6", "test", "test", "01/01/2001", "aaa@test000006.com", 1, "SSSS006", "11111115111")]
        public async Task DeletePatientTest(string fName, string lName, string addr, string city, DateTime dob, string email, int gender, string contactName, string phone)
        {
            var repo = new PatientRepo(context, logger, repoCommon);
            //create 
            var patient = new Patient { FirstName = fName, LastName = lName, Address2 = addr, City = city, DOB = dob, Email = email, Gender = gender, EmergencyContactName = contactName, Phone = phone };
            await repo.CreateRecord(patient);

            await repo.Delete(patient.PatientID);
            //check the newly created on by fetching from the DB        
            var dataByKey = await repo.GetDataByKey(patient.PatientID);
            Assert.True(dataByKey == null || dataByKey.PatientID == 0);
        }

        [Theory]
        [InlineData("Test001", "Test 1", "test", "test", "01/01/2001", "aaa@test000001.com", 1, "SSSS001", "11111111111")]
        [InlineData("Test002", "Test 2", "test", "test", "01/01/2001", "aaa@test000002.com", 1, "SSSS002", "12111111111")]
        [InlineData("Test003", "Test 3", "test", "test", "01/01/2001", "aaa@test000003.com", 1, "SSSS003", "11311111111")]
        [InlineData("Test004", "Test 4", "test", "test", "01/01/2001", "aaa@test000004.com", 1, "SSSS004", "11141111111")]
        [InlineData("Test005", "Test 5", "test", "test", "01/01/2001", "aaa@test000005.com", 1, "SSSS005", "11111511111")]
        [InlineData("Test006", "Test 6", "test", "test", "01/01/2001", "aaa@test000006.com", 1, "SSSS006", "11111115111")]
        public async Task GetAllPatient_Test(string fName, string lName, string addr, string city, DateTime dob, string email, int gender, string contactName, string phone)
        {
            var repo = new PatientRepo(context, logger, repoCommon);
            //create 
            var patient = new Patient { FirstName = fName, LastName = lName, Address2 = addr, City = city, DOB = dob, Email = email, Gender = gender, EmergencyContactName = contactName, Phone = phone };
            await repo.CreateRecord(patient);

            var data = await repo.GetAllData();
            Assert.Contains(data, x => x.FirstName.Equals(patient.FirstName));
        }
    }
}
