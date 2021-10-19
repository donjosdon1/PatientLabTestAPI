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
    [Route("api/patient")]
    [ApiController]
    public class PatientController : ControllerBase, IApiBase<PatientRequestDto, Patient>
    {
        private readonly IPatientService service;
        private readonly ILogger<PatientController> logger;
        public readonly IObjectMapper<PatientRequestDto, Patient> objectMapper;
        public PatientController(IPatientService patientService, ILogger<PatientController> loggerResult, IObjectMapper<PatientRequestDto, Patient> objectMapper)
        {
            service = patientService;
            logger = loggerResult;
            this.objectMapper = objectMapper;
        }

        [HttpPost]
        public async Task<Patient> CreateRecord([FromBody] PatientRequestDto record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return await service.CreateRecord(objectMapper.MapObject(record));
                }
                else
                {
                    return new Patient { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = ModelState.Values.Any() ? string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) : string.Empty } };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new Patient { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage } };
            }
        }

        [HttpPut]
        public async Task<Patient> UpdateRecord([FromBody] PatientRequestDto record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return await service.UpdateRecord(objectMapper.MapObject(record));
                }
                else
                {
                    return new Patient { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = ModelState.Values.Any() ? string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) : string.Empty } };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new Patient { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage } };
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
        public async Task<IEnumerable<Patient>> GetAllData()
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
        public async Task<Patient> GetDataByKey(long key)
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
