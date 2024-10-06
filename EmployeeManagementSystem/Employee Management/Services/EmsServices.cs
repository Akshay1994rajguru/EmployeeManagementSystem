using Employee_Management.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Employee_Management.Services
{
    public class EmsServices
    {
        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("https://localhost:44308/api/Employee/"),
        };
        public static async Task PostRequest(Employee employee, string token)
        {
            string bearerToken = token;
            string endpoint = "AddEmployee";
            using StringContent jsonContent = new(
                                                    System.Text.Json.JsonSerializer.Serialize(employee),
                                                    Encoding.UTF8,
                                                    "application/json");
            sharedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            HttpResponseMessage response = await sharedClient.PostAsync(endpoint, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);
            }
            else
            {
                Console.WriteLine("Error:" + response.StatusCode);
            }
        }


        /// <summary>
        /// Get list of employees from employee service
        /// </summary>
        /// <returns></returns>
        public async static Task<List<Employee>> GetAll(string token)
        {
            try
            {
                AccountService accountService = new AccountService();
                string bearerToken = token;
                string endpoint = "GetAllUsers";

                sharedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                HttpResponseMessage response = await sharedClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Employee>>(responseContent);
                }
                else
                {
                    Console.WriteLine("Error:" + response.StatusCode);
                }
                return new List<Employee>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Delete the record of employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async static Task<bool> DeleteEmployee(string id, string token)
        {
            string bearerToken = token;
            string endpoint = $"DeleteEmployee/{id}";
         
            sharedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            HttpResponseMessage response = await sharedClient.DeleteAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return true;
            }
            else
            {
                Console.WriteLine("Error:" + response.StatusCode);
            }
            return false;
        }



    }
}
