using Domain.Model;
using Domain.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi.Authentication
{
    public class JwtAuthentication : IJwtAuthentication<JwtAuthModel>
    {
        public IConfiguration Configuration { get; }

        public JwtAuthentication(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string GetJsonWebToken(JwtAuthModel entity)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);


            var claims = new[]//هذا عبارة عن ارراي بس اسمه كلايمز والي هو بتعامل معه السيكيورتي توكن
            {
                new Claim(JwtRegisteredClaimNames.NameId,entity.Id),
                new Claim(JwtRegisteredClaimNames.Email,entity.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName,entity.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),//سوال
            };

            var Token = new JwtSecurityToken(
            audience: Configuration["Jwt:Audience"],
            issuer: Configuration["Jwt:issuer"],
            claims: claims,
            expires: Convert.ToDateTime(DateTime.Now.AddSeconds(6000)),
            signingCredentials: Credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(Token);//هذا الي بحول التوكن لسترنج
        }
        public RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiersOn = DateTime.UtcNow.AddDays(10),
                CreateOn = DateTime.UtcNow

            };
        }


    }
}
