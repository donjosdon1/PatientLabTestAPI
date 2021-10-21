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
    [Route("api/labresult")]
    [ApiController]
    public class LabResultController : ControllerBase, IApiBase<LabResultRequestDto, LabResultResponseDto>
    {
        private readonly ILabResultService service;
        private readonly ILogger<LabResultController> logger;
        public readonly IObjectMapper objectMapper;
        public LabResultController(ILabResultService labResultService, ILogger<LabResultController> loggerResult, IObjectMapper objectMapper)
        {
            service = labResultService;
            logger = loggerResult;
            this.objectMapper = objectMapper;
        }

        /// <summary>
        /// This method helps to create a new LabResult. The request dto object will be converted to the LabResult data model for saving to DB.
        /// The LastUpdatedBy will be picked from the User object.
        /// </summary>
        /// <param name="record">LabResultRequestDto</param>
        /// <returns>LabResultResponseDto</returns>
        [HttpPost]
        public async Task<LabResultResponseDto> CreateRecord([FromBody] LabResultRequestDto record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = objectMapper.MapObject<LabResultRequestDto, LabResult>(record);
                    data.LastUpdatedBy = User.Identity.Name;
                    return objectMapper.MapObject<LabResult, LabResultResponseDto>(await service.CreateRecord(data));
                }
                else
                {
                    return new LabResultResponseDto { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = ModelState.Values.Any() ? string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) : string.Empty } };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new LabResultResponseDto { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage } };
            }
        }

        /// <summary>
        /// This method helps to update a new LabResult. The request dto object will be converted to the LabResult data model for saving to DB.
        /// The LastUpdatedBy will be picked from the User object.
        /// </summary>
        /// <param name="record">LabResultRequestDto</param>
        /// <returns>LabResultResponseDto</returns>
        [HttpPut]
        public async Task<LabResultResponseDto> UpdateRecord([FromBody] LabResultRequestDto record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = objectMapper.MapObject<LabResultRequestDto, LabResult>(record);
                    data.LastUpdatedBy = User.Identity.Name;
                    return objectMapper.MapObject<LabResult, LabResultResponseDto>(await service.UpdateRecord(data));

                }
                else
                {
                    return new LabResultResponseDto { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = ModelState.Values.Any() ? string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) : string.Empty } };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new LabResultResponseDto { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage } };
            }
        }

        /// <summary>
        /// This method helps to delete a new LabResult based on the id/key. 
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
        /// This method helps to retrieve all the data from DB
        /// </summary>
        /// <returns>LabResultResponseDto</returns>
        [HttpGet]
        public async Task<IEnumerable<LabResultResponseDto>> GetAllData()
        {
            try
            {
                return objectMapper.MapList<LabResult, LabResultResponseDto>(await service.GetAllData());
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// This method helps to retrieve data based on id/key
        /// </summary>
        /// <param name="key">long</param>
        /// <returns>LabResultResponseDto</returns>
        [HttpGet("{key}")]
        public async Task<LabResultResponseDto> GetDataByKey(long key)
        {
            try
            {
                return objectMapper.MapObject<LabResult, LabResultResponseDto>(await service.GetDataByKey(key));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }

}
