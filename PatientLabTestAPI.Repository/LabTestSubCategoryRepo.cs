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
    public class LabTestSubCategoryRepo : ILabTestSubCategoryRepo
    {
        private readonly PatientLabTestDbContext patientLabTestDbContext;
        private readonly ILogger<LabTestSubCategoryRepo> logger;
        private readonly IRepoCommon<LabTestSubCategory> repoCommon;

        public LabTestSubCategoryRepo(PatientLabTestDbContext context, ILogger<LabTestSubCategoryRepo> logger,
                                    IRepoCommon<LabTestSubCategory> repoCommon)
        {
            patientLabTestDbContext = context;
            this.repoCommon = repoCommon;
            this.logger = logger;
        }
        public async Task<LabTestSubCategory> CreateRecord(LabTestSubCategory record)
        {
            try
            {
                await patientLabTestDbContext.LabTestSubCategories.AddAsync(record);
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

        public async Task<LabTestSubCategory> UpdateRecord(LabTestSubCategory record)
        {
            var data = await patientLabTestDbContext.LabTestSubCategories.FirstOrDefaultAsync(x => x.SubCategoryID == record.SubCategoryID);
            record.Message = await repoCommon.Update(patientLabTestDbContext, record, data);
            return record;
        }

        public async Task<Message> Delete(long key)
        {
            var record = await patientLabTestDbContext.LabTestSubCategories.FirstOrDefaultAsync(x => x.CategoryID == key);
            return await repoCommon.Delete(patientLabTestDbContext, record);
        }

        public async Task<IEnumerable<LabTestSubCategory>> GetAllData() =>
             await patientLabTestDbContext.LabTestSubCategories.Include(x => x.LabTestCategory).OrderBy(x => x.SubCategoryName).ToListAsync();

        public async Task<LabTestSubCategory> GetDataByKey(long key)
        {
            var data = await patientLabTestDbContext.LabTestSubCategories.Include(x => x.LabTestCategory).FirstOrDefaultAsync(x=>x.SubCategoryID == key);
            if (data == null)
            {
                return new LabTestSubCategory { Message = new Message { MessageCode = Constants.RecordnotfoundErrorCode, MessageDescription = Constants.RecordnotfoundErrorMessage } };
            }
            else
            {
                data.Message = new Message { MessageCode = Constants.RecordfoundCode, MessageDescription = Constants.RecordfoundMessage };
                return data;
            }
        }

    }
}
