using PatientLabTestAPI.Common;
using PatientLabTestAPI.Models;
using PatientLabTestAPI.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepo patientRepo;
        public PatientService(IPatientRepo repo) => patientRepo = repo;
        public async Task<Patient> CreateRecord(Patient record) => await patientRepo.CreateRecord(record);

        public async Task<Patient> UpdateRecord(Patient record) => await patientRepo.UpdateRecord(record);

        public async Task<Message> Delete(long key)=>await patientRepo.Delete(key);

        public Task<IEnumerable<Patient>> GetAllData() => patientRepo.GetAllData();

        public Task<Patient> GetDataByKey(long key) => patientRepo.GetDataByKey(key);                
    }
}
