using JwtAuthenticationDemo.DTOs;
using static JwtAuthenticationDemo.Responses.CustomResponses;

namespace JwtAuthenticationDemo.Services
{
    public interface IAccountService
    {
        Task<RegistrationResponse> RegisterAsync(RegisterDTO model);
        Task<LoginResopnse> LoginAsync(LoginDTO model);
    }
}
