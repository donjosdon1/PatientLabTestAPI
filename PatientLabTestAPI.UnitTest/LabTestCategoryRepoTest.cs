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

        [Fact]
        public async Task CreateCategory()
        {
            var repo = new LabTestCategoryRepo(context, logger, repoCommon, cache);
            //create category
            var labTestCategory = new LabTestCategory { CategoryName = "Test001", Description = "Test Desc", LastUpdatedBy = "Test user" };
            await repo.CreateRecord(labTestCategory);

            //check the newly created on by fetching from the DB        
            var dataByKey = await repo.GetDataByKey(labTestCategory.CategoryID);
            Assert.True(dataByKey.CategoryID == labTestCategory.CategoryID && labTestCategory.CategoryName == dataByKey.CategoryName && dataByKey.Description == labTestCategory.Description);
        }

        [Fact]
        public async Task UpdateCategory()
        {
            var repo = new LabTestCategoryRepo(context, logger, repoCommon, cache);
            //create category
            var labTestCategory = new LabTestCategory { CategoryName = "Test002", Description = "Test Desc2", LastUpdatedBy = "Test user2" };
            await repo.CreateRecord(labTestCategory);
            labTestCategory.CategoryName = "Updated Test";
            labTestCategory.Description = "Updaed Desc2";
            await repo.UpdateRecord(labTestCategory);

            //check the newly created on by fetching from the DB        
            var dataByKey = await repo.GetDataByKey(labTestCategory.CategoryID);
            Assert.True(dataByKey.CategoryID == labTestCategory.CategoryID && labTestCategory.CategoryName == dataByKey.CategoryName && dataByKey.Description == labTestCategory.Description);
        }
        [Fact]
        public async Task DeleteCategory()
        {
            var repo = new LabTestCategoryRepo(context, logger, repoCommon, cache);
            //create category
            var labTestCategory = new LabTestCategory { CategoryName = "Test002", Description = "Test Desc2", LastUpdatedBy = "Test user2" };
            await repo.CreateRecord(labTestCategory);

            await repo.Delete(labTestCategory.CategoryID);
            //check the newly created on by fetching from the DB        
            var dataByKey = await repo.GetDataByKey(labTestCategory.CategoryID);
            Assert.True(dataByKey.CategoryID==0);
        }

        [Fact]
        public async Task GetAllCategory()
        {
            var repo = new LabTestCategoryRepo(context, logger, repoCommon, cache);
            //create category
            var labTestCategory = new LabTestCategory { CategoryName = "Test002", Description = "Test Desc2", LastUpdatedBy = "Test user2" };
            await repo.CreateRecord(labTestCategory);
            var data = await repo.GetAllData();
            Assert.True(data.Any());
        }
    }
}
