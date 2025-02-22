using TechLibrary.Application.Request;
using TechLibrary.Communication.Response;
using TechLibrary.Exception;
using TechLibrary.Infrastruture.DataAccess;
using TechLibrary.Infrastruture.Security.Encryption;
using TechLibrary.Infrastruture.Tokens.Access;

namespace TechLibrary.Application.UseCases.Login.DoLogin
{
    public class DoLoginUseCase
    {
        public ResponseRegisterUserJson Execute(RequestLoginJson requestLoginJson)
        {
            var dbcontext = new TechLibraryDbContext();

            var user = dbcontext.Users.FirstOrDefault(x => x.Email.Equals(requestLoginJson.Email));

            if (user is null)
            {
                throw new InvalidLoginException();
            }

            var criptografy = new BCriptAlgorithm();

            var passwordIsValid = criptografy.Verify(requestLoginJson.Password, user);

            if (passwordIsValid == false)
            {
                throw new InvalidLoginException();
            }

            var tokenGenerator = new JwtTokenGenerator();

            return new ResponseRegisterUserJson
            {
                Name = user.Name,
                AccessToken = tokenGenerator.Generate(user)
            };
        }
    }
}
