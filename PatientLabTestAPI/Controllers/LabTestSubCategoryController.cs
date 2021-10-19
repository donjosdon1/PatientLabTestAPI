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
    [Route("api/labsubcategory/")]
    [ApiController]
    public class LabTestSubCategoryController : ControllerBase, IApiBase<LabTestSubCategoryRequestDto, LabTestSubCategoryResponseDto>
    {
        private readonly ILabTestSubCategoryService service;
        private readonly ILogger<LabTestSubCategoryController> logger;
        public readonly IObjectMapper objectMapper;
        public LabTestSubCategoryController(ILabTestSubCategoryService labTestSubCategoryService, ILogger<LabTestSubCategoryController> loggerCategory,
            IObjectMapper objectMapper)
        {
            service = labTestSubCategoryService;
            logger = loggerCategory;
            this.objectMapper = objectMapper;
        }

        [HttpPost]
        public async Task<LabTestSubCategoryResponseDto> CreateRecord([FromBody] LabTestSubCategoryRequestDto record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = await service.CreateRecord(objectMapper.MapObject<LabTestSubCategoryRequestDto, LabTestSubCategory>(record));
                    return objectMapper.MapObject<LabTestSubCategory, LabTestSubCategoryResponseDto>(data);
                }
                else
                {
                    return new LabTestSubCategoryResponseDto { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = ModelState.Values.Any() ? string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) : string.Empty } };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new LabTestSubCategoryResponseDto { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage } };
            }
        }

        [HttpPut]
        public async Task<LabTestSubCategoryResponseDto> UpdateRecord([FromBody] LabTestSubCategoryRequestDto record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = await service.UpdateRecord(objectMapper.MapObject<LabTestSubCategoryRequestDto, LabTestSubCategory>(record));
                    return objectMapper.MapObject<LabTestSubCategory, LabTestSubCategoryResponseDto>(data);
                }
                else
                {
                    return new LabTestSubCategoryResponseDto { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = ModelState.Values.Any() ? string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) : string.Empty } };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new LabTestSubCategoryResponseDto { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage } };
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
        public async Task<IEnumerable<LabTestSubCategoryResponseDto>> GetAllData()
        {
            try
            {
                return objectMapper.MapList<LabTestSubCategory, LabTestSubCategoryResponseDto>(await service.GetAllData());
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpGet("{key}")]
        public async Task<LabTestSubCategoryResponseDto> GetDataByKey(long key)
        {
            try
            {
                return objectMapper.MapObject<LabTestSubCategory, LabTestSubCategoryResponseDto>(await service.GetDataByKey(key));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }

}
