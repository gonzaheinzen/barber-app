using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BarberApp.EF;
using BarberApp.Interfaces;
using BarberApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BarberApp.Services
{
    public class AuthService(IConfiguration _config, IBarberRepository barberRepository) : IAuthService
    {

        // User loggin method
        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO login)
        {
            var barber = await barberRepository.GetByEmailAsync(login.Email);
            if (barber == null)
                return new LoginResponseDTO(false, "Email no encontrado");

            bool checkPassword = BCrypt.Net.BCrypt.Verify(login.Password, barber.PasswordHash);
            if (!checkPassword)
                return new LoginResponseDTO(false, "Contraseña incorrecta");


            //If Loggin Success, create the Token 
            string token = GenerateJWT(barber);
            return new LoginResponseDTO(true, "Login correcto", token);
        }

        // Token creation
        private string GenerateJWT(Barber barber)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
                                {
                                    new Claim(ClaimTypes.NameIdentifier, barber.BarberId.ToString()),
                                    new Claim(ClaimTypes.Name, barber.Name!),
                                    new Claim(ClaimTypes.Email, barber.Email!),
                                    //new Claim(ClaimTypes.UserData, user.UserName!)
                                };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
