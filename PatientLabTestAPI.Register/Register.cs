using Microsoft.Extensions.DependencyInjection;
using PatientLabTestAPI.Repository;
using PatientLabTestAPI.Services;

namespace PatientLabTestAPI.Register
{
    public static class Register
    {
        public static void RegisterAll(IServiceCollection services)
        {
            RepoInitialize.Initialize(new PatientLabTestDbContext());

            services.AddDbContext<PatientLabTestDbContext>();
            services.AddScoped<ILabTestCategoryRepo, LabTestCategoryRepo>();
            services.AddScoped<ILabTestCategoryService, LabTestCategoryService>();
            services.AddScoped<ILabTestSubCategoryRepo, LabTestSubCategoryRepo>();
            services.AddScoped<ILabTestSubCategoryService, LabTestSubCategoryService>();
            services.AddScoped<ILabResultRepo, LabResultRepo>();
            services.AddScoped(typeof(IRepoCommon<>), typeof(RepoCommon<>));
        }
    }
}
