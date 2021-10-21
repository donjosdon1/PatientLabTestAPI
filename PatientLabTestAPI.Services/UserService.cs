using Microsoft.Extensions.Configuration;
using PatientLabTestAPI.Common;
using PatientLabTestAPI.Jwt;
using PatientLabTestAPI.Models;
using PatientLabTestAPI.Repository;
using System;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo userRepo;
        private readonly IConfiguration configuration;
        public UserService(IUserRepo repo, IConfiguration configuration)
        {
            userRepo = repo;
            this.configuration = configuration;
        }
        public async Task<Message> CreateRecord(User record) => await userRepo.CreateRecord(record);

        public async Task<string> ValidateUser(User user)
        {
            user = await userRepo.ValidateUser(user);
            if (user != null)
            {
                var secret = configuration["Secret"];
                var expiry = Convert.ToInt16(configuration["TokenExpiry"]);
                return JwtTokens.GetToken(secret, user.UserName.Trim(), user.Role.Trim(), expiry);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
