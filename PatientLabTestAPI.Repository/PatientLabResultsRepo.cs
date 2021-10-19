using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PatientLabTestAPI.Common;
using PatientLabTestAPI.Dto;
using PatientLabTestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Repository
{
    public class PatientLabResultsRepo : IPatientLabResultsRepo
    {
        private readonly PatientLabTestDbContext patientLabTestDbContext;
        private readonly ILogger<PatientLabResultsRepo> logger;
        private readonly IRepoCommon<PatientLabResults> repoCommon;
        public PatientLabResultsRepo(PatientLabTestDbContext context, ILogger<PatientLabResultsRepo> logger, IRepoCommon<PatientLabResults> repoCommon)
        {
            patientLabTestDbContext = context;
            this.logger = logger;
            this.repoCommon = repoCommon;
        }
        public async Task<PatientLabResults> CreateRecord(PatientLabResults record)
        {
            try
            {
                await patientLabTestDbContext.PatientLabResults.AddAsync(record);
                await patientLabTestDbContext.SaveChangesAsync();
                record.Message = new Message { MessageCode = Constants.RecordCreatedCode, MessageDescription = Constants.RecordCreatedMessage };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message ?? string.Empty);
                record.Message = new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage };
            }
            return record;
        }

        public async Task<PatientLabResults> UpdateRecord(PatientLabResults record)
        {
            var data = await patientLabTestDbContext.PatientLabResults.FirstOrDefaultAsync(x => x.PatientID == record.PatientID);
            record.Message = await repoCommon.Update(patientLabTestDbContext, record, data);
            return record;
        }

        public async Task<Message> Delete(long key)
        {
            var record = await patientLabTestDbContext.PatientLabResults.FirstOrDefaultAsync(x => x.PatientID == key);
            return await repoCommon.Delete(patientLabTestDbContext, record);

        }

        public async Task<IEnumerable<PatientLabResults>> GetAllData() =>
             await patientLabTestDbContext.PatientLabResults.OrderBy(x => x.CollectionDate).ToListAsync();

        public async Task<PatientLabResults> GetDataByKey(long key) => await patientLabTestDbContext.PatientLabResults.FindAsync(key);

        public async Task<IEnumerable<PatientLabReportResponseDto>> GetPatientWithLabReport(long resultID, DateTime startDate, DateTime endDate)
        {
            return await patientLabTestDbContext.PatientLabResults.Include(x => x.Patient).Include(x => x.LabResult).
                Where(x => x.ResultID == resultID && x.CollectionDate >= startDate && x.CollectionDate <= endDate).
                Select(x => new PatientLabReportResponseDto
                {
                    PatientLabResultID = x.PatientLabResultID,
                    PatientID = x.PatientID,
                    ResultID = x.ResultID,
                    FirstName = x.Patient.FirstName,
                    LastName = x.Patient.LastName,
                    ResultType = x.LabResult.ResultType,
                    LowRange = x.LabResult.LowRange,
                    HighRange = x.LabResult.HighRange,
                    ResultUnit = x.LabResult.ResultUnit,
                    ResultDescription = x.LabResult.ResultDescription,
                    CollectionDate = x.CollectionDate,
                    LabLocation = x.LabLocation,
                    CollectedBy = x.CollectedBy,
                    TestResultAvailableDate = x.TestResultAvailableDate,
                    TestedBy = x.TestedBy,
                    TestedDate = x.TestedDate,
                    Result = x.Result,
                    Comments = x.Comments
                }).ToListAsync();
        }
    }
}