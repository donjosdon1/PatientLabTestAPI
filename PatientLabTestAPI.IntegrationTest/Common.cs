using PatientLabTestAPI.Dto;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PatientLabTestAPI.IntegrationTest
{
    public static class Common
    {
        public static async Task<string> GetToken(HttpClient client)
        {
            try
            {
                var content = JsonContent.Create(new UserRequestDto { UserName = Guid.NewGuid().ToString(), Password = "test001", Role = "Admin" });
                var data = await client.PostAsync("/api/user", content);
                data.EnsureSuccessStatusCode();
                data = await client.PostAsync("/api/user/validateuser", content);
                data.EnsureSuccessStatusCode();
                return await data.Content.ReadAsStringAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }
    }
}
