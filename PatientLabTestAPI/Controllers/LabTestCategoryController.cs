using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatientLabTestAPI.Common;
using PatientLabTestAPI.Dto;
using PatientLabTestAPI.Mapper;
using PatientLabTestAPI.Models;
using PatientLabTestAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Controllers
{
    [Route("api/labtestcategory")]
    [ApiController]
    public class LabTestCategoryController : ControllerBase, IApiBase<LabTestCategoryRequestDto, LabTestCategory>
    {
        private readonly ILabTestCategoryService service;
        private readonly ILogger<LabTestCategoryController> logger;
        public readonly IObjectMapper<LabTestCategoryRequestDto, LabTestCategory> objectMapper;
        public LabTestCategoryController(ILabTestCategoryService labTestCategoryService, ILogger<LabTestCategoryController> loggerCategory,
             IObjectMapper<LabTestCategoryRequestDto, LabTestCategory> objectMapper)
        {
            service = labTestCategoryService;
            logger = loggerCategory;
            this.objectMapper = objectMapper;
        }

        [HttpPost]
        public async Task<LabTestCategory> CreateRecord([FromBody] LabTestCategoryRequestDto record)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    return await service.CreateRecord(objectMapper.MapObject(record));
                }
                else
                {
                    return new LabTestCategory {Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = ModelState.Values.Any() ? string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) : string.Empty } };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new LabTestCategory { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage } };
            }
        }

        [HttpPut]
        public async Task<LabTestCategory> UpdateRecord([FromBody] LabTestCategoryRequestDto record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return await service.UpdateRecord(objectMapper.MapObject(record));
                }
                else
                {
                    return new LabTestCategory { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = ModelState.Values.Any() ? string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) : string.Empty } };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new LabTestCategory { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage } };
            }
        }

        [HttpDelete("{key}")]
        public async Task<Message> Delete(long key)
        {
            try
            {
                return await service.Delete(key);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage };
            }
        }

        [HttpGet]        
        public async Task<IEnumerable<LabTestCategory>> GetAllData()
        {
            try
            {
                return await service.GetAllData();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpGet("{key}")]        
        public async Task<LabTestCategory> GetDataByKey(long key)
        {
            try
            {
                return await service.GetDataByKey(key);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }

}
