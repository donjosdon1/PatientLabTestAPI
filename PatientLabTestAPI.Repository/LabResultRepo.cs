using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PatientLabTestAPI.Common;
using PatientLabTestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Repository
{
    public class LabResultRepo : ILabResultRepo
    {
        private readonly PatientLabTestDbContext patientLabTestDbContext;
        private readonly ILogger<LabResultRepo> logger;
        private readonly IRepoCommon<LabResult> repoCommon;
        public LabResultRepo(PatientLabTestDbContext context, ILogger<LabResultRepo> logger, IRepoCommon<LabResult> repoCommon)
        {
            patientLabTestDbContext = context;
            this.logger = logger;
            this.repoCommon = repoCommon;
        }
        public async Task<LabResult> CreateRecord(LabResult record)
        {
            try
            {
                await patientLabTestDbContext.LabResults.AddAsync(record);
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

        public async Task<LabResult> UpdateRecord(LabResult record)
        {
            var data = await patientLabTestDbContext.LabResults.FirstOrDefaultAsync(x => x.ResultID == record.ResultID);
            record.Message = await repoCommon.Update(patientLabTestDbContext, record, data);
            return record;
        }

        public async Task<Message> Delete(long key)
        {
            var record = await patientLabTestDbContext.LabResults.FirstOrDefaultAsync(x => x.ResultID == key);
            return await repoCommon.Delete(patientLabTestDbContext, record);
            
        }

        public async Task<IEnumerable<LabResult>> GetAllData() =>
             await patientLabTestDbContext.LabResults.OrderBy(x => x.ResultType).ToListAsync();

        public async Task<LabResult> GetDataByKey(long key) => await patientLabTestDbContext.LabResults.FindAsync(key);
        
    }
}
