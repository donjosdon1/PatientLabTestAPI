﻿using Microsoft.AspNetCore.Mvc;
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
    [Route("api/labtestcategory")]
    [ApiController]
    public class LabTestCategoryController : ControllerBase, IApiBase<LabTestCategory>
    {
        private readonly ILabTestCategoryService service;
        private readonly ILogger<LabTestCategoryController> logger;
        public LabTestCategoryController(ILabTestCategoryService labTestCategoryService, ILogger<LabTestCategoryController> loggerCategory)
        {
            service = labTestCategoryService;
            logger = loggerCategory;
        }

        [HttpPost]
        public async Task<LabTestCategory> CreateRecord([FromBody] LabTestCategory record)
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
                return new LabTestCategory { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage } };
            }
        }

        [HttpPut]
        public async Task<LabTestCategory> UpdateRecord([FromBody] LabTestCategory record)
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
                return new LabTestCategory { Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage } };
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
        public async Task<IEnumerable<LabTestCategory>> GetAllData()
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
        public async Task<LabTestCategory> GetDataByKey(long key)
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
