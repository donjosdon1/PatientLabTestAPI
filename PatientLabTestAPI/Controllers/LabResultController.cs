using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatientLabTestAPI.Common;
using PatientLabTestAPI.Models;
using PatientLabTestAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Controllers
{
    [Route("api/labresult")]
    [ApiController]
    public class LabResultController : ControllerBase, IApiBase<LabResult>
    {
        private readonly ILabResultService service;
        private readonly ILogger<LabResultController> logger;
        public LabResultController(ILabResultService labResultService, ILogger<LabResultController> loggerResult)
        {
            service = labResultService;
            logger = loggerResult;
        }

        [HttpPost]
        public async Task<LabResult> CreateRecord([FromBody] LabResult record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await service.CreateRecord(record);
                }
                else
                {
                    record.Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = ModelState.Values.Any() ? string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) : string.Empty };
                }
                return record;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new LabResult { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage } };
            }
        }

        [HttpPut]
        public async Task<LabResult> UpdateRecord([FromBody] LabResult record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await service.UpdateRecord(record);
                }
                else
                {
                    record.Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = ModelState.Values.Any() ? string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) : string.Empty };
                }
                return record;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new LabResult { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage } };
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
        public async Task<IEnumerable<LabResult>> GetAllData()
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
        public async Task<LabResult> GetDataByKey(long key)
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
