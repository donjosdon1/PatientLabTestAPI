using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatientLabTestAPI.Common;
using PatientLabTestAPI.Dto;
using PatientLabTestAPI.Mapper;
using PatientLabTestAPI.Models;
using PatientLabTestAPI.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Controllers
{
    [EnableCors]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;
        private readonly ILogger<UserController> logger;
        private readonly IObjectMapper objectMapper;
        public UserController(IUserService userService, ILogger<UserController> logger, IObjectMapper objectMapper)
        {
            service = userService;
            this.logger = logger;
            this.objectMapper = objectMapper;
        }

        [HttpPost]
        public async Task<Message> CreateRecord([FromBody] UserRequestDto record)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return (await service.CreateRecord(objectMapper.MapObject<UserRequestDto, User>(record)));
                }
                else
                {
                    return new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = ModelState.Values.Any() ? string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)) : string.Empty };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage };
            }
        }

        [HttpPost]
        [Route("validateuser")]
        public async Task<string> ValidateUser(UserValidateRequest user) => await service.ValidateUser(objectMapper.MapObject< UserValidateRequest, User>(user));
    }

}
