using Employee_Management.Models;

namespace Employee_Management.Services
{
    public class AccountService
    {
        public async Task<string> GenerateAuthTokenAsync(UserLogin userLogin)
        {
            string endpoint = "Login";
            using (var _httpClient= new HttpClient())
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44308/api/Account/");
                var response = await _httpClient.PostAsJsonAsync(endpoint, userLogin);
                if (response != null && response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                return null;
            }
        }
    }
}
