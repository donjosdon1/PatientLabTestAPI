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

        /// <summary>
        /// This method helps to create a new PatientPabResult. The request dto object will be converted to the PatientLabResult data model for saving to DB.
        /// The LastUpdatedBy will be picked from the User object.
        /// </summary>
        /// <param name="record">PatientLabResultsRequestDto</param>
        /// <returns>PatientLabResultsResponseDto</returns>
        [HttpPost]
        public async Task<PatientLabResultsResponseDto> CreateRecord([FromBody] PatientLabResultsRequestDto record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = objectMapper.MapObject<PatientLabResultsRequestDto, PatientLabResults>(record);
                    data.LastUpdatedBy = User.Identity.Name;
                    return objectMapper.MapObject<PatientLabResults, PatientLabResultsResponseDto>(await service.CreateRecord(data));
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

        /// <summary>
        /// This method helps to update PatientPabResult. The request dto object will be converted to the PatientLabResult data model for saving to DB.
        /// The LastUpdatedBy will be picked from the User object.
        /// </summary>
        /// <param name="record">PatientLabResultsRequestDto</param>
        /// <returns>PatientLabResultsResponseDto</returns>
        [HttpPut]
        public async Task<PatientLabResultsResponseDto> UpdateRecord([FromBody] PatientLabResultsRequestDto record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = objectMapper.MapObject<PatientLabResultsRequestDto, PatientLabResults>(record);
                    data.LastUpdatedBy = User.Identity.Name;
                    return objectMapper.MapObject<PatientLabResults, PatientLabResultsResponseDto>(await service.UpdateRecord(data));
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

        /// <summary>
        /// This method helps to delete Patient labresult
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
        /// This method helps to retrieve all Patient labresult
        /// </summary>
        /// <returns>PatientLabResultsResponseDto</returns>
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

        /// <summary>
        /// This method helps to delete Patient labresult
        /// </summary>
        /// <param name="key">long</param>
        /// <returns>PatientLabResultsResponseDto</returns>
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

        /// <summary>
        /// This method helps to GetPatient that have had a certain type of lab reports
        /// </summary>
        /// <param name="resultID">long</param>
        /// <param name="startDate">DateTime</param>
        /// <param name="endDate">DateTime</param>
        /// <returns>PatientLabResultsResponseDto</returns>
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
