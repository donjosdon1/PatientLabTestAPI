using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PatientLabTestAPI.Common;
using PatientLabTestAPI.Models;
using System;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly PatientLabTestDbContext patientLabTestDbContext;
        private readonly ILogger<UserRepo> logger;
        public UserRepo(PatientLabTestDbContext context, ILogger<UserRepo> logger)
        {
            patientLabTestDbContext = context;
            this.logger = logger;
        }
        public async Task<Message> CreateRecord(User record)
        {
            try
            {
                if (!await patientLabTestDbContext.Users.AnyAsync(x => x.UserName.Trim().ToLower().Equals(record.UserName.Trim().ToLower())))
                {
                    await patientLabTestDbContext.Users.AddAsync(record);
                    await patientLabTestDbContext.SaveChangesAsync();
                    return new Message { MessageCode = Constants.UserCreated, MessageDescription = Constants.UserCreatedMessage };
                }
                else
                {
                    return new Message { MessageCode = Constants.UserExists, MessageDescription = Constants.UserExistsMessage };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message ?? string.Empty);
                return new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage };
            }
        }        

        public async Task<User> ValidateUser(User user) =>
            await patientLabTestDbContext.Users.FirstOrDefaultAsync(x => (x.UserName.Trim().ToLower().Equals(user.UserName.Trim().ToLower())) && (x.Password.Trim().Equals(user.Password.Trim())));
    }
}
