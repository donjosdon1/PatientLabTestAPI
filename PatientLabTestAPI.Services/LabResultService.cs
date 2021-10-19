using PatientLabTestAPI.Common;
using PatientLabTestAPI.Models;
using PatientLabTestAPI.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Services
{
    public class LabResultService : ILabResultService
    {
        private readonly ILabResultRepo labResultRepo;
        public LabResultService(ILabResultRepo repo) => labResultRepo = repo;
        public async Task<LabResult> CreateRecord(LabResult record) => await labResultRepo.CreateRecord(record);

        public async Task<LabResult> UpdateRecord(LabResult record) => await labResultRepo.UpdateRecord(record);

        public async Task<Message> Delete(long key)=>await labResultRepo.Delete(key);

        public async Task<IEnumerable<LabResult>> GetAllData() => await labResultRepo.GetAllData();

        public async Task<LabResult> GetDataByKey(long key) => await labResultRepo.GetDataByKey(key);                
    }
}
