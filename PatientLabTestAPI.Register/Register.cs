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
        }
    }
}
