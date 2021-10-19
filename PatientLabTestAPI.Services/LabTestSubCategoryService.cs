using PatientLabTestAPI.Common;
using PatientLabTestAPI.Models;
using PatientLabTestAPI.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Services
{
    public class LabTestSubCategoryService : ILabTestSubCategoryService
    {
        private readonly ILabTestSubCategoryRepo categoryRepo;
        public LabTestSubCategoryService(ILabTestSubCategoryRepo repo) => categoryRepo = repo;
        public async Task<LabTestSubCategory> CreateRecord(LabTestSubCategory record) => await categoryRepo.CreateRecord(record);
        public async Task<LabTestSubCategory> UpdateRecord(LabTestSubCategory record) => await categoryRepo.UpdateRecord(record);

        public async Task<Message> Delete(long key)=>await categoryRepo.Delete(key);

        public async Task<IEnumerable<LabTestSubCategory>> GetAllData() => await categoryRepo.GetAllData();

        public async Task<LabTestSubCategory> GetDataByKey(long key) => await categoryRepo.GetDataByKey(key);        
    }
}
