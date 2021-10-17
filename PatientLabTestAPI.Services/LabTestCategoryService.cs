using PatientLabTestAPI.Models;
using PatientLabTestAPI.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Services
{
    public class LabTestCategoryService : ILabTestCategoryService
    {
        private readonly ILabTestCategoryRepo categoryRepo;
        public LabTestCategoryService(ILabTestCategoryRepo repo) => categoryRepo = repo;
        public async Task<LabTestCategory> CreateRecord(LabTestCategory record) => await categoryRepo.CreateRecord(record);
        
        public async Task Delete(long key)=>await categoryRepo.Delete(key);

        public Task<IEnumerable<LabTestCategory>> GetAllData() => categoryRepo.GetAllData();

        public Task<LabTestCategory> GetDataByKey(long key) => categoryRepo.GetDataByKey(key);

        public Task<IEnumerable<LabTestCategory>> GetFilteredData(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
