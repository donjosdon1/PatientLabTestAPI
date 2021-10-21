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

        /// <summary>
        /// This method helps to create a new user with username and password
        /// </summary>
        /// <param name="record">UserRequestDto</param>
        /// <returns>Message</returns>
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

        /// <summary>
        /// This method helps to validate the user with username and password. If valid, will generate the token.
        /// This token need to be sent in the Authorization headers with the subsequent requests
        /// </summary>
        /// <param name="user">UserValidateRequest</param>
        /// <returns>string</returns>
        [HttpPost]
        [Route("validateuser")]
        public async Task<string> ValidateUser(UserValidateRequest user) => await service.ValidateUser(objectMapper.MapObject< UserValidateRequest, User>(user));
    }

}
