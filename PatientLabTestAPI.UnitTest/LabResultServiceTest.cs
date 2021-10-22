using Microsoft.Extensions.Logging;
using Moq;
using PatientLabTestAPI.Models;
using PatientLabTestAPI.Repository;
using PatientLabTestAPI.Services;
using System.Threading.Tasks;
using Xunit;

namespace PatientLabTestAPI.UnitTest
{
    public class LabResultServiceTest : TestStartup
    {
        private readonly PatientLabTestDbContext context = new();
        private readonly ILogger<LabResultRepo> logger = new Mock<ILogger<LabResultRepo>>().Object;
        private readonly RepoCommon<LabResult> repoCommon = new Mock<RepoCommon<LabResult>>(new Mock<ILogger<LabResult>>().Object).Object;

        [Theory]
        [InlineData(1, "Test001", "Test Desc1", 100, 124, "test")]
        [InlineData(1, "Test002", "Test Desc2", 100, 124, "test")]
        [InlineData(1, "Test003", "Test Desc3", 100, 124, "test")]
        [InlineData(1, "Test004", "Test Desc4", 100, 124, "test")]
        [InlineData(1, "Test005", "Test Desc5", 100, 124, "test")]
        [InlineData(1, "Test006", "Test Desc6", 100, 124, "test")]
        public async Task CreateLabResultTest(long subCategoryId, string resultType, string desc, int low, int high, string unit)
        {
            var repo = new LabResultRepo(context, logger, repoCommon);
            var service = new LabResultService(repo);
            //create 
            var labResult = new LabResult { SubCategoryID = subCategoryId, ResultType = resultType, ResultDescription = desc, LowRange = low, HighRange = high, ResultUnit = unit };
            await service.CreateRecord(labResult);

            //check the newly created on by fetching from the DB        
            var dataByKey = await service.GetDataByKey(labResult.ResultID);
            Assert.True(dataByKey.ResultID == labResult.ResultID && labResult.ResultType == dataByKey.ResultType &&
                        dataByKey.ResultDescription == labResult.ResultDescription && dataByKey.LowRange == labResult.LowRange);
        }

        [Theory]
        [InlineData(1, "Test001", "Test Desc1", 100, 124, "test")]
        [InlineData(1, "Test002", "Test Desc2", 100, 124, "test")]
        [InlineData(1, "Test003", "Test Desc3", 100, 124, "test")]
        [InlineData(1, "Test004", "Test Desc4", 100, 124, "test")]
        [InlineData(1, "Test005", "Test Desc5", 100, 124, "test")]
        [InlineData(1, "Test006", "Test Desc6", 100, 124, "test")]
        public async Task UpdateLabResultTest(long subCategoryId, string resultType, string desc, int low, int high, string unit)
        {
            var repo = new LabResultRepo(context, logger, repoCommon);
            var service = new LabResultService(repo);

            //create 
            var labResult = new LabResult { SubCategoryID = subCategoryId, ResultType = resultType, ResultDescription = desc, LowRange = low, HighRange = high, ResultUnit = unit };
            await service.CreateRecord(labResult);
            labResult.ResultType = "Updated Test";
            labResult.ResultDescription = "Updaed Desc2";
            await service.UpdateRecord(labResult);

            //check the newly created on by fetching from the DB        
            var dataByKey = await service.GetDataByKey(labResult.ResultID);
            Assert.True(dataByKey.ResultID == labResult.ResultID && labResult.ResultType == dataByKey.ResultType &&
                        dataByKey.ResultDescription == labResult.ResultDescription && dataByKey.LowRange == labResult.LowRange);
        }

        [Theory]
        [InlineData(1, "Test001", "Test Desc1", 100, 124, "test")]
        [InlineData(1, "Test002", "Test Desc2", 100, 124, "test")]
        [InlineData(1, "Test003", "Test Desc3", 100, 124, "test")]
        [InlineData(1, "Test004", "Test Desc4", 100, 124, "test")]
        [InlineData(1, "Test005", "Test Desc5", 100, 124, "test")]
        [InlineData(1, "Test006", "Test Desc6", 100, 124, "test")]
        public async Task DeleteLabResultTest(long subCategoryId, string resultType, string desc, int low, int high, string unit)
        {
            var repo = new LabResultRepo(context, logger, repoCommon);
            var service = new LabResultService(repo);

            //create 
            var labResult = new LabResult { SubCategoryID = subCategoryId, ResultType = resultType, ResultDescription = desc, LowRange = low, HighRange = high, ResultUnit = unit };
            await service.CreateRecord(labResult);

            await service.Delete(labResult.ResultID);
            //check the newly created on by fetching from the DB        
            var dataByKey = await service.GetDataByKey(labResult.ResultID);
            Assert.True(dataByKey == null || dataByKey.ResultID == 0);
        }

        [Theory]
        [InlineData(1, "Test001", "Test Desc1", 100, 124, "test")]
        [InlineData(1, "Test002", "Test Desc2", 100, 124, "test")]
        [InlineData(1, "Test003", "Test Desc3", 100, 124, "test")]
        [InlineData(1, "Test004", "Test Desc4", 100, 124, "test")]
        [InlineData(1, "Test005", "Test Desc5", 100, 124, "test")]
        [InlineData(1, "Test006", "Test Desc6", 100, 124, "test")]
        public async Task GetAllLabResult_Test(long subCategoryId, string resultType, string desc, int low, int high, string unit)
        {
            var repo = new LabResultRepo(context, logger, repoCommon);
            var service = new LabResultService(repo);

            //create 
            var labResult = new LabResult { SubCategoryID = subCategoryId, ResultType = resultType, ResultDescription = desc, LowRange = low, HighRange = high, ResultUnit = unit };
            await service.CreateRecord(labResult);

            var data = await service.GetAllData();
            Assert.Contains(data, x => x.ResultType.Equals(labResult.ResultType));
        }
    }
}
