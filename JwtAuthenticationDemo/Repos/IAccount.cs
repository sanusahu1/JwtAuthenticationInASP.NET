using JwtAuthenticationDemo.DTOs;
using static JwtAuthenticationDemo.Responses.CustomResponses;

namespace JwtAuthenticationDemo.Repos
{
    public interface IAccount
    {
        Task<RegistrationResponse> RegisterAsync(RegisterDTO model);
        Task<LoginResopnse> LoginAsync(LoginDTO model);
    }
}
