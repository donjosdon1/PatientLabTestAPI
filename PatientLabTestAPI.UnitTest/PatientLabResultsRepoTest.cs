using Microsoft.Extensions.Logging;
using Moq;
using PatientLabTestAPI.Models;
using PatientLabTestAPI.Repository;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PatientLabTestAPI.UnitTest
{
    public class PatientLabResultsRepoTest : TestStartup
    {
        private readonly PatientLabTestDbContext context = new();
        private readonly ILogger<PatientRepo> logger = new Mock<ILogger<PatientRepo>>().Object;
        private readonly RepoCommon<Patient> repoCommon = new Mock<RepoCommon<Patient>>(new Mock<ILogger<Patient>>().Object).Object;
        private readonly ILogger<PatientLabResultsRepo> loggerResult = new Mock<ILogger<PatientLabResultsRepo>>().Object;
        private readonly RepoCommon<PatientLabResults> repoCommonResult = new Mock<RepoCommon<PatientLabResults>>(new Mock<ILogger<PatientLabResults>>().Object).Object;

        [Theory]
        [InlineData("Test001", "Test 1", "test", "test", "01/01/2001", "aaa@test000001.com", 1, "SSSS001", "11111111111",1, "LABS001", "01/01/2001")]
        [InlineData("Test002", "Test 2", "test", "test", "01/01/2001", "aaa@test000002.com", 1, "SSSS002", "12111111111",2, "LABS002", "01/01/2001")]
        [InlineData("Test003", "Test 3", "test", "test", "01/01/2001", "aaa@test000003.com", 1, "SSSS003", "11311111111",3, "LABS003", "01/01/2001")]
        [InlineData("Test004", "Test 4", "test", "test", "01/01/2001", "aaa@test000004.com", 1, "SSSS004", "11141111111",4, "LABS004", "01/01/2001")]
        [InlineData("Test005", "Test 5", "test", "test", "01/01/2001", "aaa@test000005.com", 1, "SSSS005", "11111511111",5, "LABS005", "01/01/2001")]
        [InlineData("Test006", "Test 6", "test", "test", "01/01/2001", "aaa@test000006.com", 1, "SSSS006", "11111115111",6, "LABS006", "01/01/2001")]
        public async Task CreatePatientLabReportTest(string fName, string lName, string addr, string city, DateTime dob, string email, int gender, string contactName, string phone,
            int result, string labLocation, DateTime dateAvailable)
        {
            var repo = new PatientRepo(context, logger, repoCommon);
            //create 
            var patient = new Patient { FirstName = fName, LastName = lName, Address2 = addr, City = city, DOB = dob, Email = email, Gender = gender, EmergencyContactName = contactName, Phone = phone };
            await repo.CreateRecord(patient);

            var repoResult = new PatientLabResultsRepo(context, loggerResult, repoCommonResult);
            var patientResult = new PatientLabResults { PatientID = patient.PatientID, ResultID=1, Result = result, CollectedBy = fName, LabLocation = labLocation, TestResultAvailableDate = dateAvailable };

            await repoResult.CreateRecord(patientResult);

            //check the newly created on by fetching from the DB        
            var dataByKey = await repoResult.GetDataByKey(patientResult.PatientLabResultID);
            Assert.True(dataByKey.PatientLabResultID == patientResult.PatientLabResultID && dataByKey.PatientID == patientResult.PatientID && patientResult.CollectedBy == dataByKey.CollectedBy &&
                        dataByKey.TestResultAvailableDate == patientResult.TestResultAvailableDate && dataByKey.Result == patientResult.Result);
        }

        [Theory]
        [InlineData("Test001", "Test 1", "test", "test", "01/01/2001", "aaa@test000001.com", 1, "SSSS001", "11111111111", 1, "LABS001", "01/01/2001")]
        [InlineData("Test002", "Test 2", "test", "test", "01/01/2001", "aaa@test000002.com", 1, "SSSS002", "12111111111", 2, "LABS002", "01/01/2001")]
        [InlineData("Test003", "Test 3", "test", "test", "01/01/2001", "aaa@test000003.com", 1, "SSSS003", "11311111111", 3, "LABS003", "01/01/2001")]
        [InlineData("Test004", "Test 4", "test", "test", "01/01/2001", "aaa@test000004.com", 1, "SSSS004", "11141111111", 4, "LABS004", "01/01/2001")]
        [InlineData("Test005", "Test 5", "test", "test", "01/01/2001", "aaa@test000005.com", 1, "SSSS005", "11111511111", 5, "LABS005", "01/01/2001")]
        [InlineData("Test006", "Test 6", "test", "test", "01/01/2001", "aaa@test000006.com", 1, "SSSS006", "11111115111", 6, "LABS006", "01/01/2001")]
        public async Task UpdatePatientTest(string fName, string lName, string addr, string city, DateTime dob, string email, int gender, string contactName, string phone,
            int result, string labLocation, DateTime dateAvailable)
        {
            var repo = new PatientRepo(context, logger, repoCommon);
            //create 
            var patient = new Patient { FirstName = fName, LastName = lName, Address2 = addr, City = city, DOB = dob, Email = email, Gender = gender, EmergencyContactName = contactName, Phone = phone };
            await repo.CreateRecord(patient);

            var repoResult = new PatientLabResultsRepo(context, loggerResult, repoCommonResult);
            var patientResult = new PatientLabResults { PatientID = patient.PatientID, ResultID = 1, Result = result, CollectedBy = fName, LabLocation = labLocation, TestResultAvailableDate = dateAvailable };

            await repoResult.CreateRecord(patientResult);
            patientResult.TestedBy = "Updated Test";
            patientResult.CollectedBy = "Updaed Desc2";
            await repoResult.UpdateRecord(patientResult);

            //check the newly created on by fetching from the DB        
            var dataByKey = await repoResult.GetDataByKey(patientResult.PatientLabResultID);
            Assert.True(dataByKey.PatientLabResultID == patientResult.PatientLabResultID && dataByKey.PatientID == patientResult.PatientID && patientResult.CollectedBy == dataByKey.CollectedBy &&
                        dataByKey.TestResultAvailableDate == patientResult.TestResultAvailableDate && dataByKey.Result == patientResult.Result);
        }

        [Theory]
        [InlineData("Test001", "Test 1", "test", "test", "01/01/2001", "aaa@test000001.com", 1, "SSSS001", "11111111111", 1, "LABS001", "01/01/2001")]
        [InlineData("Test002", "Test 2", "test", "test", "01/01/2001", "aaa@test000002.com", 1, "SSSS002", "12111111111", 2, "LABS002", "01/01/2001")]
        [InlineData("Test003", "Test 3", "test", "test", "01/01/2001", "aaa@test000003.com", 1, "SSSS003", "11311111111", 3, "LABS003", "01/01/2001")]
        [InlineData("Test004", "Test 4", "test", "test", "01/01/2001", "aaa@test000004.com", 1, "SSSS004", "11141111111", 4, "LABS004", "01/01/2001")]
        [InlineData("Test005", "Test 5", "test", "test", "01/01/2001", "aaa@test000005.com", 1, "SSSS005", "11111511111", 5, "LABS005", "01/01/2001")]
        [InlineData("Test006", "Test 6", "test", "test", "01/01/2001", "aaa@test000006.com", 1, "SSSS006", "11111115111", 6, "LABS006", "01/01/2001")]
        public async Task DeletePatientTest(string fName, string lName, string addr, string city, DateTime dob, string email, int gender, string contactName, string phone,
            int result, string labLocation, DateTime dateAvailable)
        {
            var repo = new PatientRepo(context, logger, repoCommon);
            //create 
            var patient = new Patient { FirstName = fName, LastName = lName, Address2 = addr, City = city, DOB = dob, Email = email, Gender = gender, EmergencyContactName = contactName, Phone = phone };
            await repo.CreateRecord(patient);

            var repoResult = new PatientLabResultsRepo(context, loggerResult, repoCommonResult);
            var patientResult = new PatientLabResults { PatientID = patient.PatientID, ResultID = 1, Result = result, CollectedBy = fName, LabLocation = labLocation, TestResultAvailableDate = dateAvailable };
            await repoResult.CreateRecord(patientResult);

            await repoResult.Delete(patientResult.PatientLabResultID);
            //check the newly created on by fetching from the DB        
            var dataByKey = await repoResult.GetDataByKey(patientResult.PatientLabResultID);
            Assert.True(dataByKey == null || dataByKey.PatientLabResultID == 0);
        }

        [Theory]
        [InlineData("Test001", "Test 1", "test", "test", "01/01/2001", "aaa@test000001.com", 1, "SSSS001", "11111111111", 1, "LABS001", "01/01/2001")]
        [InlineData("Test002", "Test 2", "test", "test", "01/01/2001", "aaa@test000002.com", 1, "SSSS002", "12111111111", 2, "LABS002", "01/01/2001")]
        [InlineData("Test003", "Test 3", "test", "test", "01/01/2001", "aaa@test000003.com", 1, "SSSS003", "11311111111", 3, "LABS003", "01/01/2001")]
        [InlineData("Test004", "Test 4", "test", "test", "01/01/2001", "aaa@test000004.com", 1, "SSSS004", "11141111111", 4, "LABS004", "01/01/2001")]
        [InlineData("Test005", "Test 5", "test", "test", "01/01/2001", "aaa@test000005.com", 1, "SSSS005", "11111511111", 5, "LABS005", "01/01/2001")]
        [InlineData("Test006", "Test 6", "test", "test", "01/01/2001", "aaa@test000006.com", 1, "SSSS006", "11111115111", 6, "LABS006", "01/01/2001")]
        public async Task GetAllPatient_Test(string fName, string lName, string addr, string city, DateTime dob, string email, int gender, string contactName, string phone,
            int result, string labLocation, DateTime dateAvailable)
        {
            var repo = new PatientRepo(context, logger, repoCommon);
            //create 
            var patient = new Patient { FirstName = fName, LastName = lName, Address2 = addr, City = city, DOB = dob, Email = email, Gender = gender, EmergencyContactName = contactName, Phone = phone };
            await repo.CreateRecord(patient);

            var repoResult = new PatientLabResultsRepo(context, loggerResult, repoCommonResult);
            var patientResult = new PatientLabResults { PatientID = patient.PatientID, ResultID = 1, Result = result, CollectedBy = fName, LabLocation = labLocation, TestResultAvailableDate = dateAvailable };
            await repoResult.CreateRecord(patientResult);

            var data = await repoResult.GetAllData();
            Assert.Contains(data, x => x.PatientID.Equals(patient.PatientID));
        }
    }
}
