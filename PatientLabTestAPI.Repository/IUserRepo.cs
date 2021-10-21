using PatientLabTestAPI.Common;
using PatientLabTestAPI.Models;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Repository
{
    public interface IUserRepo
    {
        Task<Message> CreateRecord(User record);
        Task<User> ValidateUser(User user);
    }
}
