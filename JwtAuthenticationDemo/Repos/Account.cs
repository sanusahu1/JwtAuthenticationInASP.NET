using JwtAuthenticationDemo.Data;
using JwtAuthenticationDemo.DTOs;
using JwtAuthenticationDemo.Models;
using JwtAuthenticationDemo.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static JwtAuthenticationDemo.Responses.CustomResponses;

namespace JwtAuthenticationDemo.Repos
{
    public class Account : IAccount
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _config;

        public Account(AppDbContext appDbContext, IConfiguration config)
        {
            _appDbContext = appDbContext;
            _config = config;
        }

        public async Task<LoginResopnse> LoginAsync(LoginDTO model)
        {
            var findUser = await GetUser(model.Email);
            if (findUser == null) return new LoginResopnse(false, "User Not Found ");

            if (!BCrypt.Net.BCrypt.Verify(model.Password, findUser.Password))
                return new LoginResopnse(false, "Email/Password is not valid ");
            string jwtToken = GenerateToken(findUser);
            return new LoginResopnse (true,"Login Succesfull",jwtToken);
        }

        public async Task<RegistrationResponse> RegisterAsync(RegisterDTO model)
        {
            var findUser = await GetUser(model.Email);
            if (findUser != null) return new RegistrationResponse(false, "User Already Exists ");
            _appDbContext.Users.Add(
                new ApplicationUser()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                }
            );
            await _appDbContext.SaveChangesAsync();
            return new RegistrationResponse(true, "Success");
        }

        private async Task<ApplicationUser> GetUser(string email)
            => await _appDbContext.Users.FirstOrDefaultAsync(e => e.Email == email);

        private string GenerateToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creadentails = new SigningCredentials(securityKey , SecurityAlgorithms.HmacSha256);

            var userClaims = new[]
            {
                //new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name , user.Name!),
                new Claim(ClaimTypes.Email, user.Email!)
            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"]!,
                audience: _config["Jwt:Auddience"],
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creadentails
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
