using PatientLabTestAPI.Common;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Repository
{
    public interface IRepoCommon<T> where T : class
    {
        Task<Message> Update(PatientLabTestDbContext patientLabTestDbContext, T recordInput, T recordOnDB);
        Task<Message> Delete(PatientLabTestDbContext patientLabTestDbContext, T recordOnDB);
    }
}
