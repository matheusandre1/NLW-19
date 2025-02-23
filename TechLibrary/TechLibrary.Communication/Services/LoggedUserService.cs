using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using TechLibrary.Domain.Entities;
using TechLibrary.Infrastruture.DataAccess;

namespace TechLibrary.Communication.Services
{
    public class LoggedUserService
    {
        private readonly HttpContext _httpContext;
        public LoggedUserService(HttpContext  httpContext)
        {
            _httpContext = httpContext;
        }

        public User User(TechLibraryDbContext dbContext)
        {
            var authentication = _httpContext.Request.Headers["Authorization"].ToString();
            var token = authentication["Bearer ".Length..].Trim();

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurity = tokenHandler.ReadJwtToken(token);

            var identifier = jwtSecurity.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;

            var userId = Guid.Parse(identifier);            

            return dbContext.Users.First(user => user.Id == userId);
        }
    }
}
