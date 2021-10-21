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

        /// <summary>
        /// This method helps to create a new SubTest Category. The request dto object will be converted to the LabSubTestCategory data model for saving to DB.
        /// The LastUpdatedBy will be picked from the User object.
        /// </summary>
        /// <param name="record">LabSubTestCategoryRequestDto</param>
        /// <returns>LabSubTestCategoryResponseDto</returns>
        [HttpPost]
        public async Task<LabTestSubCategoryResponseDto> CreateRecord([FromBody] LabTestSubCategoryRequestDto record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = objectMapper.MapObject<LabTestSubCategoryRequestDto, LabTestSubCategory>(record);
                    data.LastUpdatedBy = User.Identity.Name;
                    return objectMapper.MapObject<LabTestSubCategory, LabTestSubCategoryResponseDto>(await service.CreateRecord(data));
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

        /// <summary>
        /// This method helps to update a new SubTest Category. The request dto object will be converted to the LabSubTestCategory data model for saving to DB.
        /// The LastUpdatedBy will be picked from the User object.
        /// </summary>
        /// <param name="record">LabSubTestCategoryRequestDto</param>
        /// <returns>LabSubTestCategoryResponseDto</returns>
        [HttpPut]
        public async Task<LabTestSubCategoryResponseDto> UpdateRecord([FromBody] LabTestSubCategoryRequestDto record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = objectMapper.MapObject<LabTestSubCategoryRequestDto, LabTestSubCategory>(record);
                    data.LastUpdatedBy = User.Identity.Name;
                    return objectMapper.MapObject<LabTestSubCategory, LabTestSubCategoryResponseDto>(await service.UpdateRecord(data));
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

        /// <summary>
        /// This method helps to Delete SubTest Category. 
        /// </summary>
        /// <param name="key">long</param>
        /// <returns>Message</returns>
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

        /// <summary>
        /// This method helps to retrieve all SubTest Categories
        /// </summary>
        /// <returns>LabSubTestCategoryResponseDto</returns>
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

        /// <summary>
        /// This method helps to retrieve SubTest Category. 
        /// </summary>
        /// <param name="long">key</param>
        /// <returns>LabSubTestCategoryResponseDto</returns>
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
