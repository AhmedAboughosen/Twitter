using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain.Entities;
using Core.Domain.Exceptions;
using Core.Domain.Model.DTO.RequestDTO;
using Core.Domain.Model.DTO.ResponseDto;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.Handler
{
    public class AuthenticationHandler : IRequestHandler<UserLoginRequestDto, UserLoginResponseDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfigurationSection _jwtSettings;

        public AuthenticationHandler(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _jwtSettings = configuration.GetSection("JwtSettings");
        }


        public async Task<UserLoginResponseDto> Handle(UserLoginRequestDto dto,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                var signingCredentials = GetSigningCredentials();
                var claims = GetClaims(user);
                var tokenOptions = GenerateTokenOptions(signingCredentials, await claims);


                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                var roles = await _userManager.GetRolesAsync(user);


                return new UserLoginResponseDto()
                {
                    Email = user.Email,
                    Id = user.Id,
                    LastLogin = user.LastLogin,
                    PhoneNumber = user.PhoneNumber,
                    UserRole = roles[0],
                    Token = token,
                };
            }

            throw new APIException(
                "Invalid UserName or Password", HttpStatusCode.NotFound);
        }


        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                _jwtSettings.GetSection("Issuer").Value,
                _jwtSettings.GetSection("Audience").Value,
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection("TokenTimeout").Value)),
                signingCredentials: signingCredentials);
            return tokenOptions;
        }


        private async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.NameIdentifier, user.Id),
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}