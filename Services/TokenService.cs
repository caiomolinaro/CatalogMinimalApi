using CatalogMinimalApi.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace CatalogMinimalApi.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(string key, string issuer, string audience, UserModel user)
        {
            //payload do token
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString())
            };

            //gerar a chave secreta
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            //aplica o algoritmo na chave
            var credentials = new SigningCredentials(securityKey,
                                                             SecurityAlgorithms.HmacSha256);

            //gera o token
            var token = new JwtSecurityToken(issuer: issuer,
                                      audience: audience,
                                      claims: claims,
                                      expires: DateTime.Now.AddMinutes(10),
                                      signingCredentials: credentials);


            //desserializa o token
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
