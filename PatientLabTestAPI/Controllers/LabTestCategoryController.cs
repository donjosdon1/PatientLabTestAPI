using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    [EnableCors]
    [Authorize]
    [Route("api/labtestcategory")]
    [ApiController]
    public class LabTestCategoryController : ControllerBase, IApiBase<LabTestCategoryRequestDto, LabTestCategoryResponseDto>
    {
        private readonly ILabTestCategoryService service;
        private readonly ILogger<LabTestCategoryController> logger;
        public readonly IObjectMapper objectMapper;
        public LabTestCategoryController(ILabTestCategoryService labTestCategoryService, ILogger<LabTestCategoryController> loggerCategory,
             IObjectMapper objectMapper)
        {
            service = labTestCategoryService;
            logger = loggerCategory;
            this.objectMapper = objectMapper;
        }

        [HttpPost]
        public async Task<LabTestCategoryResponseDto> CreateRecord([FromBody] LabTestCategoryRequestDto record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = objectMapper.MapObject<LabTestCategoryRequestDto, LabTestCategory>(record);
                    data.LastUpdatedBy = User.Identity.Name;
                    return objectMapper.MapObject<LabTestCategory, LabTestCategoryResponseDto>(await service.CreateRecord(data));
                }
                else
                {
                    return new LabTestCategoryResponseDto { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = ModelState.Values.Any() ? string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) : string.Empty } };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new LabTestCategoryResponseDto { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage } };
            }
        }

        [HttpPut]
        public async Task<LabTestCategoryResponseDto> UpdateRecord([FromBody] LabTestCategoryRequestDto record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = objectMapper.MapObject<LabTestCategoryRequestDto, LabTestCategory>(record);
                    data.LastUpdatedBy = User.Identity.Name;
                    return objectMapper.MapObject<LabTestCategory, LabTestCategoryResponseDto>(await service.UpdateRecord(data));
                }
                else
                {
                    return new LabTestCategoryResponseDto { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = ModelState.Values.Any() ? string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) : string.Empty } };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new LabTestCategoryResponseDto { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage } };
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
        public async Task<IEnumerable<LabTestCategoryResponseDto>> GetAllData()
        {
            try
            {
                return objectMapper.MapList<LabTestCategory, LabTestCategoryResponseDto>(await service.GetAllData());
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpGet("{key}")]        
        public async Task<LabTestCategoryResponseDto> GetDataByKey(long key)
        {
            try
            {
                return objectMapper.MapObject<LabTestCategory, LabTestCategoryResponseDto>(await service.GetDataByKey(key));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }

}
