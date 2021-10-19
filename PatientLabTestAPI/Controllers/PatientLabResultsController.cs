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
    [Route("api/patientlabreport")]
    [ApiController]
    public class PatientLabResultsController : ControllerBase, IApiBase<PatientLabResultsRequestDto, PatientLabResultsResponseDto>
    {
        private readonly IPatientLabResultsService service;
        private readonly ILogger<PatientLabResultsController> logger;
        public readonly IObjectMapper objectMapper;
        public PatientLabResultsController(IPatientLabResultsService patientService, ILogger<PatientLabResultsController> loggerResult, IObjectMapper objectMapper)
        {
            service = patientService;
            logger = loggerResult;
            this.objectMapper = objectMapper;
        }

        [HttpPost]
        public async Task<PatientLabResultsResponseDto> CreateRecord([FromBody] PatientLabResultsRequestDto record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = await service.CreateRecord(objectMapper.MapObject<PatientLabResultsRequestDto, PatientLabResults>(record));
                    return objectMapper.MapObject<PatientLabResults, PatientLabResultsResponseDto>(data);
                }
                else
                {
                    return new PatientLabResultsResponseDto { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = ModelState.Values.Any() ? string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) : string.Empty } };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new PatientLabResultsResponseDto { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage } };
            }
        }

        [HttpPut]
        public async Task<PatientLabResultsResponseDto> UpdateRecord([FromBody] PatientLabResultsRequestDto record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = await service.UpdateRecord(objectMapper.MapObject<PatientLabResultsRequestDto, PatientLabResults>(record));
                    return objectMapper.MapObject<PatientLabResults, PatientLabResultsResponseDto>(data);
                }
                else
                {
                    return new PatientLabResultsResponseDto { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = ModelState.Values.Any() ? string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) : string.Empty } };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new PatientLabResultsResponseDto { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage } };
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
        public async Task<IEnumerable<PatientLabResultsResponseDto>> GetAllData()
        {
            try
            {
                return objectMapper.MapList<PatientLabResults, PatientLabResultsResponseDto>(await service.GetAllData());
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpGet("{key}")]
        public async Task<PatientLabResultsResponseDto> GetDataByKey(long key)
        {
            try
            {
                return objectMapper.MapObject<PatientLabResults, PatientLabResultsResponseDto>(await service.GetDataByKey(key));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("getreport")]
        public async Task<IEnumerable<PatientLabReportResponseDto>> GetPatientWithLabReport(long resultID, DateTime startDate, DateTime endDate)
        {
            try
            {
                return await service.GetPatientWithLabReport(resultID, startDate, endDate);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }

}
