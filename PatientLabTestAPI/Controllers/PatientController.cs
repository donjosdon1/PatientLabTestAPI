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
    [Route("api/patient")]
    [ApiController]
    public class PatientController : ControllerBase, IApiBase<PatientRequestDto, PatientResponseDto>
    {
        private readonly IPatientService service;
        private readonly ILogger<PatientController> logger;
        public readonly IObjectMapper objectMapper;
        public PatientController(IPatientService patientService, ILogger<PatientController> loggerResult, IObjectMapper objectMapper)
        {
            service = patientService;
            logger = loggerResult;
            this.objectMapper = objectMapper;
        }

        /// <summary>
        /// This method helps to create a new Patient. The request dto object will be converted to the Patient data model for saving to DB.
        /// The LastUpdatedBy will be picked from the User object.
        /// </summary>
        /// <param name="record">PatientRequestDto</param>
        /// <returns>PatientResponseDto</returns>
        [HttpPost]
        public async Task<PatientResponseDto> CreateRecord([FromBody] PatientRequestDto record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = objectMapper.MapObject<PatientRequestDto, Patient>(record);
                    data.LastUpdatedBy = User.Identity.Name;
                    return objectMapper.MapObject<Patient, PatientResponseDto>(await service.CreateRecord(data));
                }
                else
                {
                    return new PatientResponseDto { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = ModelState.Values.Any() ? string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) : string.Empty } };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new PatientResponseDto { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage } };
            }
        }

        /// <summary>
        /// This method helps to update a new Patient. The request dto object will be converted to the Patient data model for saving to DB.
        /// The LastUpdatedBy will be picked from the User object.
        /// </summary>
        /// <param name="record">PatientRequestDto</param>
        /// <returns>PatientResponseDto</returns>
        [HttpPut]
        public async Task<PatientResponseDto> UpdateRecord([FromBody] PatientRequestDto record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = objectMapper.MapObject<PatientRequestDto, Patient>(record);
                    data.LastUpdatedBy = User.Identity.Name;
                    return objectMapper.MapObject<Patient, PatientResponseDto>(await service.UpdateRecord(data));
                }
                else
                {
                    return new PatientResponseDto { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = ModelState.Values.Any() ? string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) : string.Empty } };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new PatientResponseDto { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage } };
            }
        }

        /// <summary>
        /// This method helps to delete Patient. 
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
        /// This method helps to retrieve all Patients
        /// </summary>
        /// <returns>PatientResponseDto</returns>
        [HttpGet]
        public async Task<IEnumerable<PatientResponseDto>> GetAllData()
        {
            try
            {
                return objectMapper.MapList<Patient, PatientResponseDto>(await service.GetAllData());
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// This method helps to retrieve Patient by id/key
        /// </summary>
        /// <param name="key">long</param>
        /// <returns>PatientResponseDto</returns>
        [HttpGet("{key}")]
        public async Task<PatientResponseDto> GetDataByKey(long key)
        {
            try
            {
                return objectMapper.MapObject<Patient, PatientResponseDto>(await service.GetDataByKey(key));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }

}
