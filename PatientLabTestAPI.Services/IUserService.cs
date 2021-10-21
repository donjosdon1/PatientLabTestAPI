using PatientLabTestAPI.Common;
using PatientLabTestAPI.Models;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Services
{
    public interface IUserService
    {
        Task<Message> CreateRecord(User record);
        Task<string> ValidateUser(User user);
    }
}
