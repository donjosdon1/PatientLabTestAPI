using PatientLabTestAPI.Dto;
using PatientLabTestAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Services
{
    public interface IPatientLabResultsService : IServiceBase<PatientLabResults>
    {
        Task<IEnumerable<PatientLabReportResponseDto>> GetPatientWithLabReport(long resultID, DateTime startDate, DateTime endDate);
    }
}
