using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatientLabTestAPI.Models;
using PatientLabTestAPI.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabTestCategoryController : ControllerBase//, IApiBase<LabTestCategory, long>
    {
        private readonly ILabTestCategoryService service;
        private readonly ILogger<LabTestCategoryController> logger;
        public LabTestCategoryController(ILabTestCategoryService labTestCategoryService, ILogger<LabTestCategoryController> loggerCategory)
        {
            service = labTestCategoryService;
            logger = loggerCategory;
        }

        [HttpPost]
        [Route("createcategory")]
        public async Task<LabTestCategory> CreateRecord([FromBody] LabTestCategory record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await service.CreateRecord(record);
                }
                return record;
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        //public Task Delete(long key)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<LabTestCategory>> GetAllData()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<LabTestCategory> GetDataByKey(long key)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<LabTestCategory>> GetFilteredData(DateTime startDate, DateTime endDate)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
