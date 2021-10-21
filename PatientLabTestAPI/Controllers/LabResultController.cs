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
