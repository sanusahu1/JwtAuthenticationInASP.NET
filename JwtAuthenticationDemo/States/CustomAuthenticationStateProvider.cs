using JwtAuthenticationDemo.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JwtAuthenticationDemo.States
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal _principal = new(new ClaimsIdentity());
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(Constants.JWTToken))
                    return await Task.FromResult(new AuthenticationState(_principal));

                var getUserClaim = DecryptToken(Constants.JWTToken);
                if (getUserClaim != null) return await Task.FromResult(new AuthenticationState(_principal));

                var claimPrincipal = SetClaimPrincipal(getUserClaim);
                return await Task.FromResult(new AuthenticationState(claimPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_principal));
            }
        }
        public static void UpdateAuthenticationState(string jwtToken)
        {
            var claimPrincipal = new ClaimsPrincipal();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                Constants.JWTToken = jwtToken;
                var getUserClaim = DecryptToken(jwtToken);
                claimPrincipal = SetClaimPrincipal(getUserClaim);
            }
            else
            {
                Constants.JWTToken = null;
            }
            CustomAuthenticationStateProvider _stateProvider = new CustomAuthenticationStateProvider();
            _stateProvider.NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimPrincipal)));

        }
        public static ClaimsPrincipal SetClaimPrincipal(CustomUserClaim claims)
        {
            if (claims.Email is null) return new ClaimsPrincipal();
            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new(ClaimTypes.Name , claims.Name!),
                    new(ClaimTypes.Email, claims.Email!),
                },"JwtAuth"));
        }

        public static CustomUserClaim DecryptToken(string jwtToken ) 
        {
            if(string.IsNullOrEmpty(jwtToken)) return new CustomUserClaim();

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            var name = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Name);
            var email = token.Claims.FirstOrDefault(_=> _.Type == ClaimTypes.Email);
            return new CustomUserClaim(name!.Value , email!.Value);
        }
    }
}
