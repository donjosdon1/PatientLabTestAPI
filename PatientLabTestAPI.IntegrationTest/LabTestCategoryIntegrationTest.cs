using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using PatientLabTestAPI.Dto;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace PatientLabTestAPI.IntegrationTest
{
    public class LabTestCategoryIntegrationTest : IClassFixture<WebAppFactory<Startup>>
    {
        private readonly HttpClient client;

        public LabTestCategoryIntegrationTest(WebAppFactory<Startup> factory)
        {
            client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("http://localhost"),
                HandleCookies = true,
                MaxAutomaticRedirections = 7
            });
        }

        [Theory]
        [InlineData("Test001", "Test Desc1")]
        [InlineData("Test002", "Test Desc2")]
        [InlineData("Test003", "Test Desc3")]
        [InlineData("Test004", "Test Desc4")]
        [InlineData("Test005", "Test Desc5")]
        [InlineData("Test006", "Test Desc6")]
        public async Task CreateRecord(string catName, string desc)
        {
            try
            {
                var content = JsonContent.Create(new LabTestCategoryRequestDto { CategoryName = catName, Description = desc });
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await Common.GetToken(client));
                var data = await client.PostAsync("/api/labtestcategory", content);
                data.EnsureSuccessStatusCode();
                var response = await data.Content.ReadAsStringAsync();
                Assert.Contains("100", response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        [Theory]
        [InlineData("Test001", "Test Desc1")]
        [InlineData("Test002", "Test Desc2")]
        [InlineData("Test003", "Test Desc3")]
        [InlineData("Test004", "Test Desc4")]
        [InlineData("Test005", "Test Desc5")]
        [InlineData("Test006", "Test Desc6")]
        public async Task UpdateRecord(string catName, string desc)
        {
            try
            {
                var content = JsonContent.Create(new LabTestCategoryRequestDto { CategoryName = catName, Description = desc });
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await Common.GetToken(client));
                var data = await client.PostAsync("/api/labtestcategory", content);
                data.EnsureSuccessStatusCode();
                var response = await data.Content.ReadAsStringAsync();
                Assert.Contains("100", response);
                var category = JsonConvert.DeserializeObject< LabTestCategoryResponseDto>(response);
                category.CategoryName = "Test0001";
                content = JsonContent.Create(category);

                data = await client.PutAsync("/api/labtestcategory", content);
                data.EnsureSuccessStatusCode();
                response = await data.Content.ReadAsStringAsync();
                Assert.Contains("200", response);
                category = JsonConvert.DeserializeObject<LabTestCategoryResponseDto>(response);

                data = await client.GetAsync($"/api/labtestcategory/{category.CategoryID}");
                data.EnsureSuccessStatusCode();
                response = await data.Content.ReadAsStringAsync();
                category = JsonConvert.DeserializeObject<LabTestCategoryResponseDto>(response);
                Assert.True(category.CategoryName == "Test0001" && response.Contains("600)"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Theory]
        [InlineData("Test001", "Test Desc1")]
        [InlineData("Test002", "Test Desc2")]
        [InlineData("Test003", "Test Desc3")]
        [InlineData("Test004", "Test Desc4")]
        [InlineData("Test005", "Test Desc5")]
        [InlineData("Test006", "Test Desc6")]
        public async Task DeleteRecord(string catName, string desc)
        {
            try
            {
                var content = JsonContent.Create(new LabTestCategoryRequestDto { CategoryName = catName, Description = desc });
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await Common.GetToken(client));
                var data = await client.PostAsync("/api/labtestcategory", content);
                data.EnsureSuccessStatusCode();
                var response = await data.Content.ReadAsStringAsync();
                Assert.Contains("100", response);
                var category = JsonConvert.DeserializeObject<LabTestCategoryResponseDto>(response);

                data = await client.DeleteAsync($"/api/labtestcategory/{category.CategoryID}");
                data.EnsureSuccessStatusCode();
                response = await data.Content.ReadAsStringAsync();
                Assert.Contains("300", response);

                data = await client.GetAsync($"/api/labtestcategory/{category.CategoryID}");
                data.EnsureSuccessStatusCode();
                response = await data.Content.ReadAsStringAsync();
                Assert.Contains("400", response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
