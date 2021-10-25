using Microsoft.AspNetCore.Mvc.Testing;
using PatientLabTestAPI.Dto;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace PatientLabTestAPI.IntegrationTest
{
    public class UserIntegrationTest : IClassFixture<WebAppFactory<Startup>>
    {
        private readonly HttpClient client;

        public UserIntegrationTest(WebAppFactory<Startup> factory)
        {
            client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("http://localhost"),
                HandleCookies = true,
                MaxAutomaticRedirections = 7
            });
        }

        [Fact]
        public async Task CreateRecord()
        {
            try
            {
                var content = JsonContent.Create(new UserRequestDto { UserName = Guid.NewGuid().ToString(), Password = "test001", Role = "Admin" });
                var data = await client.PostAsync("/api/user", content);
                data.EnsureSuccessStatusCode();
                var response = await data.Content.ReadAsStringAsync();
                Assert.Contains("750", response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        [Fact]
        public async Task ValidateUser_Success()
        {
            try
            {
                var content = JsonContent.Create(new UserRequestDto { UserName = Guid.NewGuid().ToString(), Password = "test001", Role = "Admin" });
                var data = await client.PostAsync("/api/user", content);
                data.EnsureSuccessStatusCode();
                var response = await data.Content.ReadAsStringAsync();
                Assert.Contains("750", response);
                data = await client.PostAsync("/api/user/validateuser", content);
                data.EnsureSuccessStatusCode();
                response = await data.Content.ReadAsStringAsync();
                Assert.True(response != string.Empty);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        [Fact]
        public async Task ValidateUser_Failed()
        {
            try
            {
                var content = JsonContent.Create(new UserValidateRequest { UserName = Guid.NewGuid().ToString(), Password = "test001" });
                var data = await client.PostAsync("/api/user/validateuser", content);
                data.EnsureSuccessStatusCode();
                var response = await data.Content.ReadAsStringAsync();
                Assert.True(response == string.Empty);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

    }
}
