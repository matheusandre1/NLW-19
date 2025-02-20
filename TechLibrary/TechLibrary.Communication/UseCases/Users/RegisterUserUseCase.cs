using TechLibrary.Communication.Request;
using TechLibrary.Communication.Response;
using TechLibrary.Domain.Entities;
using TechLibrary.Exception;
using TechLibrary.Infrastruture;

namespace TechLibrary.Application.UseCases.Users
{
    public class RegisterUserUseCase
    {
        public ResponseRegisterUserJson Execute(RequestUserJson requestUserJson)
        {
            Validate(requestUserJson);

            var entity = new User
            {
                Email = requestUserJson.Email,
                Name = requestUserJson.Name,
                Password = requestUserJson.Password

            };

            var dbcontext = new TechLibraryDbContext();

            dbcontext.Users.Add(entity);
            dbcontext.SaveChanges();
            return new ResponseRegisterUserJson()
            {
                Name = entity.Name,
            };

        }
        private void Validate(RequestUserJson requestUserJson)
        {
            var validator = new RegisterUserValidator();
            var result = validator.Validate(requestUserJson);
            
            if( result.IsValid == false)
            {
                var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnvalidationException(errorMessage);
            }
        }
    }
}
