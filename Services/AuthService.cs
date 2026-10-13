using Microsoft.AspNetCore.Identity;
using TaskList.Models;
using TaskList.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace TaskList.Services
{
    public class AuthService
    {
        //jwt configration
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        private readonly List<User> users = new ();

        private readonly PasswordHasher<User> passwordHasher = new ();

        public bool Register(RegistrationDto data)
        {
            var existing = users.FirstOrDefault(x => data.UserName == x.UserName);
            if(existing != null)
            {
                return false;
            }

            //create a new user
            var user = new User
            {
                Id = users.Count + 1,
                UserName = data.UserName,
                Role = "User"
            };

            //hashing password
            user.PasswordHash = passwordHasher.HashPassword(
                user,
                data.Password
                );

            users.Add(user);

            return true;
        }

        public string? Login(LoginDto data)
        {
            var user = users.FirstOrDefault(x => x.UserName == data.UserName);

            if(user == null)
            {
                return null;
            }

            //varifying password
            var result = passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                data.Password
                );
            if(result == PasswordVerificationResult.Failed)
            {
                return null;
            }
            else
            {
                
                return GenerateToken(user);
            }
        }

        private string GenerateToken(User user)
        {
            //read Jwt settings
            var Key = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("Jwt key is missing");
            var Issuer = _configuration["Jwt:Issuer"];
            var Audience = _configuration["Jwt:Audience"];

            //converts secret key into bytes
            var SecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Key)
                );

            //create signing crediantials
            var credentials = new SigningCredentials(
                SecurityKey,
                SecurityAlgorithms.HmacSha256
                );

            //create claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role,user.Role)
            };

            //create Jwt Token
            var token = new JwtSecurityToken(
                issuer : Issuer,
                audience : Audience,
                claims:claims,
                expires:DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
                );

            //Convert token into string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}