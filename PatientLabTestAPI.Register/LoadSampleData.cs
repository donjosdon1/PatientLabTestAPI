using PatientLabTestAPI.Common;
using PatientLabTestAPI.Models;
using PatientLabTestAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientLabTestAPI.Register
{
    public static class LoadSampleData
    {
        public static void LoadData(PatientLabTestDbContext patientLabTestDbContext)
        {
            //Add categories            
            patientLabTestDbContext.LabTestCategories.AddRange(GetLabTestCategories());
            patientLabTestDbContext.SaveChanges();
            patientLabTestDbContext.LabTestSubCategories.AddRange(
                GetLabTestSubCategories(patientLabTestDbContext));
            patientLabTestDbContext.SaveChanges();
            patientLabTestDbContext.LabResults.AddRange(
                GetLabResults(patientLabTestDbContext));
            patientLabTestDbContext.SaveChanges();
        }

        private static List<LabTestSubCategory> GetLabTestSubCategories(PatientLabTestDbContext patientLabTestDbContext)
        {
            return new List<LabTestSubCategory>
                {
                    new LabTestSubCategory { CategoryID = GetCategoryID(LabConstants.GeneralHealth, patientLabTestDbContext), Cost=149, Description = LabConstants.CholestrolPanelDesc, SubCategoryName = LabConstants.CholestrolPanel, LastUpdatedBy = LabConstants.Admin, LastUpdatedDate = DateTime.Now },
                    new LabTestSubCategory { CategoryID = GetCategoryID(LabConstants.MensHealth, patientLabTestDbContext), Cost=199, Description = LabConstants.BasicHealthProfileDesc, SubCategoryName = LabConstants.BasicHealthProfile, LastUpdatedBy = LabConstants.Admin, LastUpdatedDate = DateTime.Now },
                };
        }

        private static List<LabResult> GetLabResults(PatientLabTestDbContext patientLabTestDbContext)
        {
            return new List<LabResult>
                {
                    new LabResult { ResultType="LDL Cholestrol", HighRange=129, LowRange=100, ResultDescription="Bad Cholestrol", ResultUnit="mg/dL", 
                        SubCategoryID=GetSubCategoryID(LabConstants.CholestrolPanel, patientLabTestDbContext), LastUpdatedBy = LabConstants.Admin, LastUpdatedDate = DateTime.Now },
                };
        }

        private static long GetCategoryID(string categoryName, PatientLabTestDbContext patientLabTestDbContext)
        {
            return patientLabTestDbContext.LabTestCategories.Any(x => x.CategoryName.Trim().Equals(categoryName)) ?
                patientLabTestDbContext.LabTestCategories.FirstOrDefault(x => x.CategoryName.Trim().Equals(categoryName)).CategoryID : 1;
        }

        private static long GetSubCategoryID(string categoryName, PatientLabTestDbContext patientLabTestDbContext)
        {
            return patientLabTestDbContext.LabTestSubCategories.Any(x => x.SubCategoryName.Trim().Equals(categoryName)) ?
                patientLabTestDbContext.LabTestSubCategories.FirstOrDefault(x => x.SubCategoryName.Trim().Equals(categoryName)).CategoryID : 1;
        }

        private static List<LabTestCategory> GetLabTestCategories()
        {
            return new List<LabTestCategory>
                {
                    new LabTestCategory { CategoryName = LabConstants.GeneralHealth, Description = LabConstants.GeneralHealthDesc, LastUpdatedBy =LabConstants.Admin, LastUpdatedDate = DateTime.Now },
                    new LabTestCategory { CategoryName = LabConstants.MensHealth, Description = LabConstants.MensHealthDesc, LastUpdatedBy = LabConstants.Admin, LastUpdatedDate = DateTime.Now },
                    new LabTestCategory { CategoryName = LabConstants.WomensHealth, Description = LabConstants.WomensHealthDesc, LastUpdatedBy = LabConstants.Admin, LastUpdatedDate = DateTime.Now },
                    new LabTestCategory { CategoryName = LabConstants.KidsHealth, Description = LabConstants.KidsHealthDesc, LastUpdatedBy = LabConstants.Admin, LastUpdatedDate = DateTime.Now }
                };
        }
    }
}
