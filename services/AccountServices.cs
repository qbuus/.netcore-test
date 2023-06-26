using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Controllers;
using API.entityFramework;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.entityFramework
{
    public interface IAccountService
    {
        string GenerateJwt(LoginDto dto);
    }
    public class AccountServices: IAccountService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly AuthSettings _authenticationSettings;
        private readonly IPasswordHasher<User> _passwordHasher;
        public AccountServices(RestaurantDbContext context, ILogger loggerl, AuthSettings authenticationSettings, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = context;
            _logger = loggerl;
            _authenticationSettings = authenticationSettings;
            _passwordHasher = passwordHasher;
        }
        public void RegisterUser(RegisterUserDTO dto)
        {
            var newUser = new RegisterUserDTO()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId,
            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }

        public string GenerateJwt(LoginDto dto)
        {
            var user = _dbContext.Users.Include(user => user.Role).FirstOrDefault(x => x.Email == dto.Email);

            if (user is null)
            {
                throw new BadRequestException("invalid username or password");
            }

            var rsults = _passwordHasher.VerifyHashedPassword(user, user.Password.Hash, dto.Password);

            if (rsults == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("invalid username or passwordd");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.firstName} {user.lastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
                new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")),
                new Claim("Nationality", user.Nationality),
            };

            if (!string.IsNullOrEmpty(user.Nationality))
            {
                claims.Add(
                    new Claim("Nationality", user.Nationality)
                    );
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, 
                _authenticationSettings.JwtIssuer, 
                claims,
                expires: expires, 
                signingCredentials: cred);        

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
