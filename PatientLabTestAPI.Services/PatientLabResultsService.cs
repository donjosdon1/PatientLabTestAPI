using PatientLabTestAPI.Common;
using PatientLabTestAPI.Dto;
using PatientLabTestAPI.Models;
using PatientLabTestAPI.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Services
{
    public class PatientLabResultsService : IPatientLabResultsService
    {
        private readonly IPatientLabResultsRepo patientRepo;
        public PatientLabResultsService(IPatientLabResultsRepo repo) => patientRepo = repo;
        public async Task<PatientLabResults> CreateRecord(PatientLabResults record) => await patientRepo.CreateRecord(record);

        public async Task<PatientLabResults> UpdateRecord(PatientLabResults record) => await patientRepo.UpdateRecord(record);

        public async Task<Message> Delete(long key)=>await patientRepo.Delete(key);

        public async Task<IEnumerable<PatientLabResults>> GetAllData() => await patientRepo.GetAllData();

        public async Task<PatientLabResults> GetDataByKey(long key) => await patientRepo.GetDataByKey(key);
        public async Task<IEnumerable<PatientLabReportResponseDto>> GetPatientWithLabReport(long resultID, DateTime startDate, DateTime endDate) =>
            await patientRepo.GetPatientWithLabReport(resultID, startDate, endDate);
    }
}
