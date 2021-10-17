using PatientLabTestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Repository
{
    public static class RepoInitialize
    {
        public static void Initialize(PatientLabTestDbContext patientLabTestDbContext)
        {
            patientLabTestDbContext.Database.EnsureDeletedAsync();
            patientLabTestDbContext.Database.EnsureCreatedAsync();

            //Add categories            
            patientLabTestDbContext.LabTestCategories.AddRange(
                new List<LabTestCategory>
                {
                    new LabTestCategory { CategoryName = "General Health", Description = "General Health related lab test", LastUpdatedBy = "Admin", LastUpdatedDate = DateTime.Now },
                    new LabTestCategory { CategoryName = "Men's Health", Description = "Men's Health related lab test", LastUpdatedBy = "Admin", LastUpdatedDate = DateTime.Now },
                    new LabTestCategory { CategoryName = "Women's Health", Description = "Women's Health related lab test", LastUpdatedBy = "Admin", LastUpdatedDate = DateTime.Now },
                    new LabTestCategory { CategoryName = "Kids Health", Description = "Kids Health related lab test", LastUpdatedBy = "Admin", LastUpdatedDate = DateTime.Now }
                });
            patientLabTestDbContext.SaveChanges();
        }
    }
}
