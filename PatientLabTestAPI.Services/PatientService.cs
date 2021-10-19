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

        public async Task<IEnumerable<Patient>> GetAllData() => await patientRepo.GetAllData();

        public async Task<Patient> GetDataByKey(long key) => await patientRepo.GetDataByKey(key);                
    }
}
