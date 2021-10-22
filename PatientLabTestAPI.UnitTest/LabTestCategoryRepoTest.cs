using Microsoft.Extensions.Logging;
using Moq;
using PatientLabResultAPI.Cache;
using PatientLabTestAPI.Models;
using PatientLabTestAPI.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PatientLabTestAPI.UnitTest
{
    public class LabTestCategoryRepoTest : TestStartup
    {
        private readonly PatientLabTestDbContext context = new();
        private readonly ILogger<LabTestCategoryRepo> logger = new Mock<ILogger<LabTestCategoryRepo>>().Object;
        private readonly RepoCommon<LabTestCategory> repoCommon = new Mock<RepoCommon<LabTestCategory>>(new Mock<ILogger<LabTestCategory>>().Object).Object;
        private readonly ICache<IEnumerable<LabTestCategory>> cache = new Mock<ICache<IEnumerable<LabTestCategory>>>().Object;

        [Theory]
        [InlineData("Test001", "Test Desc1", "test")]
        [InlineData("Test002", "Test Desc2", "test")]
        [InlineData("Test003", "Test Desc3", "test")]
        [InlineData("Test004", "Test Desc4", "test")]
        [InlineData("Test005", "Test Desc5", "test")]
        [InlineData("Test006", "Test Desc6", "test")]
        public async Task CreateCategoryTest(string catName, string desc, string updatedBy)
        {
            var repo = new LabTestCategoryRepo(context, logger, repoCommon, cache);
            //create category
            var labTestCategory = new LabTestCategory { CategoryName = catName, Description = desc, LastUpdatedBy = updatedBy };
            await repo.CreateRecord(labTestCategory);

            //check the newly created on by fetching from the DB        
            var dataByKey = await repo.GetDataByKey(labTestCategory.CategoryID);
            Assert.True(dataByKey.CategoryID == labTestCategory.CategoryID && labTestCategory.CategoryName == dataByKey.CategoryName && dataByKey.Description == labTestCategory.Description);
        }

        [Theory]
        [InlineData("Test001", "Test Desc1", "test")]
        [InlineData("Test002", "Test Desc2", "test")]
        [InlineData("Test003", "Test Desc3", "test")]
        [InlineData("Test004", "Test Desc4", "test")]
        [InlineData("Test005", "Test Desc5", "test")]
        [InlineData("Test006", "Test Desc6", "test")]
        public async Task UpdateCategoryTest(string catName, string desc, string updatedBy)
        {
            var repo = new LabTestCategoryRepo(context, logger, repoCommon, cache);
            //create category
            var labTestCategory = new LabTestCategory { CategoryName = catName, Description = desc, LastUpdatedBy = updatedBy };
            await repo.CreateRecord(labTestCategory);
            labTestCategory.CategoryName = "Updated Test";
            labTestCategory.Description = "Updaed Desc2";
            await repo.UpdateRecord(labTestCategory);

            //check the newly created on by fetching from the DB        
            var dataByKey = await repo.GetDataByKey(labTestCategory.CategoryID);
            Assert.True(dataByKey.CategoryID == labTestCategory.CategoryID && labTestCategory.CategoryName == dataByKey.CategoryName && dataByKey.Description == labTestCategory.Description);
        }

        [Theory]
        [InlineData("Test001", "Test Desc1", "test")]
        [InlineData("Test002", "Test Desc2", "test")]
        [InlineData("Test003", "Test Desc3", "test")]
        [InlineData("Test004", "Test Desc4", "test")]
        [InlineData("Test005", "Test Desc5", "test")]
        [InlineData("Test006", "Test Desc6", "test")]
        public async Task DeleteCategoryTest(string catName, string desc, string updatedBy)
        {
            var repo = new LabTestCategoryRepo(context, logger, repoCommon, cache);
            //create category
            var labTestCategory = new LabTestCategory { CategoryName = catName, Description = desc, LastUpdatedBy = updatedBy };
            await repo.CreateRecord(labTestCategory);

            await repo.Delete(labTestCategory.CategoryID);
            //check the newly created on by fetching from the DB        
            var dataByKey = await repo.GetDataByKey(labTestCategory.CategoryID);
            Assert.True(dataByKey.CategoryID==0);
        }

        [Theory]
        [InlineData("Test001", "Test Desc1", "test")]
        [InlineData("Test002", "Test Desc2", "test")]
        [InlineData("Test003", "Test Desc3", "test")]
        [InlineData("Test004", "Test Desc4", "test")]
        [InlineData("Test005", "Test Desc5", "test")]
        [InlineData("Test006", "Test Desc6", "test")]
        public async Task GetAllCategoryTest(string catName, string desc, string updatedBy)
        {
            var repo = new LabTestCategoryRepo(context, logger, repoCommon, cache);
            //create category
            var labTestCategory = new LabTestCategory { CategoryName = catName, Description = desc, LastUpdatedBy = updatedBy };
            await repo.CreateRecord(labTestCategory);
            var data = await repo.GetAllData();
            Assert.True(data.Any());
        }
    }
}
