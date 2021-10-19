﻿using Microsoft.AspNetCore.Mvc;
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
    [Route("api/labresult")]
    [ApiController]
    public class LabResultController : ControllerBase, IApiBase<LabResultRequestDto, LabResult>
    {
        private readonly ILabResultService service;
        private readonly ILogger<LabResultController> logger;
        public readonly IObjectMapper<LabResultRequestDto, LabResult> objectMapper;
        public LabResultController(ILabResultService labResultService, ILogger<LabResultController> loggerResult, IObjectMapper<LabResultRequestDto, LabResult> objectMapper)
        {
            service = labResultService;
            logger = loggerResult;
            this.objectMapper = objectMapper;
        }

        [HttpPost]
        public async Task<LabResult> CreateRecord([FromBody] LabResultRequestDto record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return await service.CreateRecord(objectMapper.MapObject(record));
                }
                else
                {
                    return new LabResult { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = ModelState.Values.Any() ? string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) : string.Empty } };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new LabResult { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage } };
            }
        }

        [HttpPut]
        public async Task<LabResult> UpdateRecord([FromBody] LabResultRequestDto record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return await service.UpdateRecord(objectMapper.MapObject(record));
                }
                else
                {
                    return new LabResult { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = ModelState.Values.Any() ? string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) : string.Empty } };
                }
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
