using JwtAuthenticationDemo.DTOs;
using JwtAuthenticationDemo.Responses;
using static JwtAuthenticationDemo.Responses.CustomResponses;

namespace JwtAuthenticationDemo.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResopnse> LoginAsync(LoginDTO model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/account/login", model);
            var result = await response.Content.ReadFromJsonAsync<LoginResopnse>();

            return result!;
        }

        public async Task<RegistrationResponse> RegisterAsync(RegisterDTO model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/account/register", model);
            var result = await response.Content.ReadFromJsonAsync<RegistrationResponse>();

            return result!;
        }
    }
}
