using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PatientLabResultAPI.Cache;
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
        private readonly ICache<IEnumerable<LabTestCategory>> cache;
        
        public LabTestCategoryRepo(PatientLabTestDbContext context, ILogger<LabTestCategoryRepo> logger,
                                    IRepoCommon<LabTestCategory> repoCommon, ICache<IEnumerable<LabTestCategory>> cache)
        {
            patientLabTestDbContext = context;
            this.logger = logger;
            this.repoCommon = repoCommon;
            this.cache = cache;
        }
        public async Task<LabTestCategory> CreateRecord(LabTestCategory record)
        {
            try
            {
                await patientLabTestDbContext.LabTestCategories.AddAsync(record);
                await patientLabTestDbContext.SaveChangesAsync();
                cache.Clear(nameof(LabTestCategory));
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
            cache.Clear(nameof(LabTestCategory));
            return record;
        }

        public async Task<Message> Delete(long key)
        {
            var record = await patientLabTestDbContext.LabTestCategories.FirstOrDefaultAsync(x => x.CategoryID == key);
            cache.Clear(nameof(LabTestCategory));
            return await repoCommon.Delete(patientLabTestDbContext, record);
        }

        public async Task<IEnumerable<LabTestCategory>> GetAllData()
        {
            return await GetData();
        }

        private async Task<IEnumerable<LabTestCategory>> GetData()
        {
            var data = cache.GetCache(nameof(LabTestCategory));
            if (data != null && data.Any())
            {
                return data.OrderBy(x => x.CategoryName).ToList();
            }
            else
            {
                data = await patientLabTestDbContext.LabTestCategories.OrderBy(x => x.CategoryName).ToListAsync();
                cache.SetCache(data, nameof(LabTestCategory));
                return data;
            }
        }

        public async Task<LabTestCategory> GetDataByKey(long key)
        {
            var data = (await GetData())?.FirstOrDefault(x=>x.CategoryID == key);
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
