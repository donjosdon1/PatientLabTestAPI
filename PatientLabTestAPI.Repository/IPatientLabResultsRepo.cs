using PatientLabTestAPI.Dto;
using PatientLabTestAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Repository
{
    public interface IPatientLabResultsRepo : IBaseRepo<PatientLabResults>
    {
        Task<IEnumerable<PatientLabReportResponseDto>> GetPatientWithLabReport(long resultID, DateTime startDate, DateTime endDate);
    }
}
