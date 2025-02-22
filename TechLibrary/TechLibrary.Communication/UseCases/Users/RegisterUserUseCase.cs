using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;
using TechLibrary.Communication.Request;
using TechLibrary.Communication.Response;
using TechLibrary.Domain.Entities;
using TechLibrary.Exception;
using TechLibrary.Infrastruture;
using TechLibrary.Infrastruture.DataAccess;
using TechLibrary.Infrastruture.Security.Encryption;
using TechLibrary.Infrastruture.Tokens.Access;

namespace TechLibrary.Application.UseCases.Users
{
    public class RegisterUserUseCase
    {
        public ResponseRegisterUserJson Execute(RequestUserJson requestUserJson)
        {
            var dbcontext = new TechLibraryDbContext();
            Validate(requestUserJson, dbcontext);

            var criptografy = new BCriptAlgorithm();

            var entity = new User
            {
                Email = requestUserJson.Email,
                Name = requestUserJson.Name,
                Password = criptografy.HashPassword(requestUserJson.Password)

            };            

            dbcontext.Users.Add(entity);
            dbcontext.SaveChanges();

            var tokenGenerator = new JwtTokenGenerator();
            return new ResponseRegisterUserJson()
            {
                Name = entity.Name,
                AccessToken = tokenGenerator.Generate(entity)
            };

        }
        private void Validate(RequestUserJson requestUserJson, TechLibraryDbContext techLibraryDbContext)
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(requestUserJson);

            var existUserWithEmail = techLibraryDbContext.Users.Any(x => x.Email.Equals(requestUserJson.Email));

            if (existUserWithEmail)
            {
                result.Errors.Add(new ValidationFailure("Email", "Email already exists"));
            }


            
            if( result.IsValid == false)
            {
                var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnvalidationException(errorMessage);
            }
        }
    }
}
