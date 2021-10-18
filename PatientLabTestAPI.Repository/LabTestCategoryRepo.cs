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
    public class LabTestCategoryRepo : ILabTestCategoryRepo
    {
        private readonly PatientLabTestDbContext patientLabTestDbContext;
        private readonly ILogger<LabTestCategoryRepo> logger;
        private readonly IRepoCommon<LabTestCategory> repoCommon;

        public LabTestCategoryRepo(PatientLabTestDbContext context, ILogger<LabTestCategoryRepo> logger,
                                    IRepoCommon<LabTestCategory> repoCommon)
        {
            patientLabTestDbContext = context;
            this.logger = logger;
            this.repoCommon = repoCommon;
        }
        public async Task<LabTestCategory> CreateRecord(LabTestCategory record)
        {
            try
            {
                await patientLabTestDbContext.LabTestCategories.AddAsync(record);
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

        public async Task<LabTestCategory> UpdateRecord(LabTestCategory record)
        {
            var data = await patientLabTestDbContext.LabTestCategories.FirstOrDefaultAsync(x => x.CategoryID == record.CategoryID);
            record.Message = await repoCommon.Update(patientLabTestDbContext, record, data);
            return record;
        }

        public async Task<Message> Delete(long key)
        {
            var record = await patientLabTestDbContext.LabTestCategories.FirstOrDefaultAsync(x => x.CategoryID == key);
            return await repoCommon.Delete(patientLabTestDbContext, record);
        }

        public async Task<IEnumerable<LabTestCategory>> GetAllData() =>
             await patientLabTestDbContext.LabTestCategories.OrderBy(x => x.CategoryName).ToListAsync();

        public async Task<LabTestCategory> GetDataByKey(long key)
        {
            var data = await patientLabTestDbContext.LabTestCategories.FindAsync(key);
            if (data == null)
            {
                return new LabTestCategory { Message = new Message { MessageCode = Constants.RecordnotfoundErrorCode, MessageDescription = Constants.RecordnotfoundErrorMessage } };
            }
            else
            {
                data.Message = new Message { MessageCode = Constants.RecordfoundCode, MessageDescription = Constants.RecordfoundMessage };
                return data;
            }
        }

    }
}
