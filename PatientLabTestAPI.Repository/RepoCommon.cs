using Microsoft.Extensions.Logging;
using PatientLabTestAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Repository
{
    public class RepoCommon<T> : IRepoCommon<T> where T : class
    {
        private readonly ILogger<LabResultRepo> logger;

        public RepoCommon(ILogger<LabResultRepo> logger)
        {
            this.logger = logger;
        }
        public async Task<Message> Delete(PatientLabTestDbContext patientLabTestDbContext, T recordOnDB)
        {
            try
            {
                if (recordOnDB != null)
                {
                    patientLabTestDbContext.Remove(recordOnDB);
                    await patientLabTestDbContext.SaveChangesAsync();
                    return new Message { MessageCode = Constants.RecordDeletedCode, MessageDescription = Constants.RecordDeletedMessage };
                }
                else
                {
                    return new Message { MessageCode = Constants.RecordnotfoundErrorCode, MessageDescription = Constants.RecordnotfoundErrorMessage };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message ?? string.Empty);
                return new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage };
            }
        }

        public async Task<Message> Update(PatientLabTestDbContext patientLabTestDbContext, T recordInput, T recordOnDB)
        {
            try
            {
                if (recordOnDB != null)
                {
                    patientLabTestDbContext.Entry(recordOnDB).CurrentValues.SetValues(recordInput);
                    await patientLabTestDbContext.SaveChangesAsync();
                    return new Message { MessageCode = Constants.RecordUpdatedCode, MessageDescription = Constants.RecordUpdatedMessage };
                }
                else
                {
                    return new Message { MessageCode = Constants.RecordnotfoundErrorCode, MessageDescription = Constants.RecordnotfoundErrorMessage };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message ?? string.Empty);
                return new Message { MessageCode = Constants.GenericErrorcode, MessageDescription = Constants.GenericErrorMessage };
            }
        }
    }
}
