using Microsoft.Extensions.DependencyInjection;
using PatientLabTestAPI.Repository;
using PatientLabTestAPI.Services;

namespace PatientLabTestAPI.Register
{
    public static class RegisterInstances
    {
        public static void RegisterAll(IServiceCollection services)
        {
            using (var context = new PatientLabTestDbContext())
            {
                RepoInitialize.Initialize(context);
                LoadSampleData.LoadData(context);
            }

            //Dependency Injection
            services.AddDbContext<PatientLabTestDbContext>();
            services.AddScoped<ILabTestCategoryRepo, LabTestCategoryRepo>();
            services.AddScoped<ILabTestCategoryService, LabTestCategoryService>();
            services.AddScoped<ILabTestSubCategoryRepo, LabTestSubCategoryRepo>();
            services.AddScoped<ILabTestSubCategoryService, LabTestSubCategoryService>();
            services.AddScoped<ILabResultRepo, LabResultRepo>();
            services.AddScoped(typeof(IRepoCommon<>), typeof(RepoCommon<>));
            services.AddScoped<ILabResultService, LabResultService>();
        }
    }
}
