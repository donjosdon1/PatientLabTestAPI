﻿using PatientLabTestAPI.Common;
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

        public async Task<LabTestCategory> UpdateRecord(LabTestCategory record) => await categoryRepo.UpdateRecord(record);

        public async Task<Message> Delete(long key)=>await categoryRepo.Delete(key);

        public Task<IEnumerable<LabTestCategory>> GetAllData() => categoryRepo.GetAllData();

        public Task<LabTestCategory> GetDataByKey(long key) => categoryRepo.GetDataByKey(key);                
    }
}
