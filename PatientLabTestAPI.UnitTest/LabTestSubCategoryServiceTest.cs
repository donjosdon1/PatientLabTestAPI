using Microsoft.Extensions.Logging;
using Moq;
using PatientLabTestAPI.Models;
using PatientLabTestAPI.Repository;
using PatientLabTestAPI.Services;
using System.Threading.Tasks;
using Xunit;

namespace PatientLabTestAPI.UnitTest
{
    public class LabTestSubCategoryServiceTest : TestStartup
    {
        private readonly PatientLabTestDbContext context = new();
        private readonly ILogger<LabTestSubCategoryRepo> logger = new Mock<ILogger<LabTestSubCategoryRepo>>().Object;
        private readonly RepoCommon<LabTestSubCategory> repoCommon = new Mock<RepoCommon<LabTestSubCategory>>(new Mock<ILogger<LabTestSubCategory>>().Object).Object;

        [Theory]
        [InlineData("Test001", "Test Desc1", "test")]
        [InlineData("Test002", "Test Desc2", "test")]
        [InlineData("Test003", "Test Desc3", "test")]
        [InlineData("Test004", "Test Desc4", "test")]
        [InlineData("Test005", "Test Desc5", "test")]
        [InlineData("Test006", "Test Desc6", "test")]
        public async Task CreateSubCategoryTest(string catName, string desc, string updatedBy)
        {
            var repo = new LabTestSubCategoryRepo(context, logger, repoCommon);
            var service = new LabTestSubCategoryService(repo);
            //create category
            var labTestSubCategory = new LabTestSubCategory { SubCategoryName = catName, Description = desc, LastUpdatedBy = updatedBy, CategoryID = 1 };
            await service.CreateRecord(labTestSubCategory);

            //check the newly created on by fetching from the DB        
            var dataByKey = await service.GetDataByKey(labTestSubCategory.SubCategoryID);
            Assert.True(dataByKey.SubCategoryID == labTestSubCategory.SubCategoryID && labTestSubCategory.SubCategoryName == dataByKey.SubCategoryName &&
                        dataByKey.Description == labTestSubCategory.Description);
        }

        [Theory]
        [InlineData("Test001", "Test Desc1", "test")]
        [InlineData("Test002", "Test Desc2", "test")]
        [InlineData("Test003", "Test Desc3", "test")]
        [InlineData("Test004", "Test Desc4", "test")]
        [InlineData("Test005", "Test Desc5", "test")]
        [InlineData("Test006", "Test Desc6", "test")]
        public async Task UpdateSubCategoryTest(string catName, string desc, string updatedBy)
        {
            var repo = new LabTestSubCategoryRepo(context, logger, repoCommon);
            var service = new LabTestSubCategoryService(repo);

            //create category
            var labTestSubCategory = new LabTestSubCategory { SubCategoryName = catName, Description = desc, LastUpdatedBy = updatedBy, CategoryID = 1 };
            await service.CreateRecord(labTestSubCategory);
            labTestSubCategory.SubCategoryName = "Updated Test";
            labTestSubCategory.Description = "Updaed Desc2";
            await service.UpdateRecord(labTestSubCategory);

            //check the newly created on by fetching from the DB        
            var dataByKey = await service.GetDataByKey(labTestSubCategory.SubCategoryID);
            Assert.True(dataByKey.SubCategoryID == labTestSubCategory.SubCategoryID && labTestSubCategory.SubCategoryName == dataByKey.SubCategoryName &&
                                    dataByKey.Description == labTestSubCategory.Description);
        }

        [Theory]
        [InlineData("Test001", "Test Desc1", "test")]
        [InlineData("Test002", "Test Desc2", "test")]
        [InlineData("Test003", "Test Desc3", "test")]
        [InlineData("Test004", "Test Desc4", "test")]
        [InlineData("Test005", "Test Desc5", "test")]
        [InlineData("Test006", "Test Desc6", "test")]
        public async Task DeleteSubCategoryTest(string catName, string desc, string updatedBy)
        {
            var repo = new LabTestSubCategoryRepo(context, logger, repoCommon);
            var service = new LabTestSubCategoryService(repo);

            //create category
            var labTestSubCategory = new LabTestSubCategory { SubCategoryName = catName, Description = desc, LastUpdatedBy = updatedBy, CategoryID = 1 };
            await service.CreateRecord(labTestSubCategory);

            await service.Delete(labTestSubCategory.SubCategoryID);
            //check the newly created on by fetching from the DB        
            var dataByKey = await service.GetDataByKey(labTestSubCategory.SubCategoryID);
            Assert.True(dataByKey.SubCategoryID == 0);
        }

        [Theory]
        [InlineData("Test001", "Test Desc1", "test")]
        [InlineData("Test002", "Test Desc2", "test")]
        [InlineData("Test003", "Test Desc3", "test")]
        [InlineData("Test004", "Test Desc4", "test")]
        [InlineData("Test005", "Test Desc5", "test")]
        [InlineData("Test006", "Test Desc6", "test")]
        public async Task GetAllSubCategoryTest(string catName, string desc, string updatedBy)
        {
            var repo = new LabTestSubCategoryRepo(context, logger, repoCommon);
            var service = new LabTestSubCategoryService(repo);

            //create category
            var labTestSubCategory = new LabTestSubCategory { SubCategoryName = catName, Description = desc, LastUpdatedBy = updatedBy, CategoryID = 1 };
            await service.CreateRecord(labTestSubCategory);
            var data = await service.GetAllData();
            Assert.Contains(data, x => x.SubCategoryName.Equals(labTestSubCategory.SubCategoryName));
        }
    }
}
