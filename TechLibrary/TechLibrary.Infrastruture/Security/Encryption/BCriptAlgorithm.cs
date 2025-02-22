using TechLibrary.Domain.Entities;

namespace TechLibrary.Infrastruture.Security.Encryption
{
    public class BCriptAlgorithm
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, User user)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.Password);

        }
    }
}
