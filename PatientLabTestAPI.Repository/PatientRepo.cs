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
    public class PatientRepo : IPatientRepo
    {
        private readonly PatientLabTestDbContext patientLabTestDbContext;
        private readonly ILogger<PatientRepo> logger;
        private readonly IRepoCommon<Patient> repoCommon;
        public PatientRepo(PatientLabTestDbContext context, ILogger<PatientRepo> logger, IRepoCommon<Patient> repoCommon)
        {
            patientLabTestDbContext = context;
            this.logger = logger;
            this.repoCommon = repoCommon;
        }
        public async Task<Patient> CreateRecord(Patient record)
        {
            try
            {
                await patientLabTestDbContext.Patients.AddAsync(record);
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

        public async Task<Patient> UpdateRecord(Patient record)
        {
            var data = await patientLabTestDbContext.Patients.FirstOrDefaultAsync(x => x.PatientID == record.PatientID);
            record.Message = await repoCommon.Update(patientLabTestDbContext, record, data);
            return record;
        }

        public async Task<Message> Delete(long key)
        {
            var record = await patientLabTestDbContext.Patients.FirstOrDefaultAsync(x => x.PatientID == key);
            return await repoCommon.Delete(patientLabTestDbContext, record);
            
        }

        public async Task<IEnumerable<Patient>> GetAllData() =>
             await patientLabTestDbContext.Patients.OrderBy(x => x.FirstName).ToListAsync();

        public async Task<Patient> GetDataByKey(long key) => await patientLabTestDbContext.Patients.FindAsync(key);
        
    }
}
