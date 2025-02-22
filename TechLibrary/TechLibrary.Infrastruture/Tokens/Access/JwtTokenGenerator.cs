using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechLibrary.Domain.Entities;

namespace TechLibrary.Infrastruture.Tokens.Access
{
    public class JwtTokenGenerator
    {
        public string Generate(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };
            var tokenDescription = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(GenerateKey(), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(securityToken);
        }


        public static SymmetricSecurityKey GenerateKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MaZx8Iohq5x66WkyOg5s1K4hC8YClrGg"));
        }
    }
}
