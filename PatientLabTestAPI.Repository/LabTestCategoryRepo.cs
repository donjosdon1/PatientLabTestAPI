using Microsoft.EntityFrameworkCore;
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
        public LabTestCategoryRepo(PatientLabTestDbContext context) => patientLabTestDbContext = context;
        public async Task<LabTestCategory> CreateRecord(LabTestCategory record)
        {
            await patientLabTestDbContext.LabTestCategories.AddAsync(record);
            await patientLabTestDbContext.SaveChangesAsync();
            return record;
        }

        public async Task Delete(long key)
        {
            var record = await patientLabTestDbContext.LabTestCategories.FirstOrDefaultAsync(x => x.CategoryID == key);
            if (record != null)
            {
                patientLabTestDbContext.Remove(record);
            }
        }

        public async Task<IEnumerable<LabTestCategory>> GetAllData() =>
             await patientLabTestDbContext.LabTestCategories.OrderBy(x => x.CategoryName).ToListAsync();

        public async Task<LabTestCategory> GetDataByKey(long key) => await patientLabTestDbContext.LabTestCategories.FindAsync(key);
        
        public Task<IEnumerable<LabTestCategory>> GetFilteredData(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
