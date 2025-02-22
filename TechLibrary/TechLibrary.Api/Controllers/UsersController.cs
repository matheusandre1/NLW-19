using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechLibrary.Application.Response;
using TechLibrary.Application.UseCases.Users;
using TechLibrary.Communication.Request;
using TechLibrary.Communication.Response;
using TechLibrary.Exception;

namespace TechLibrary.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorMessages), StatusCodes.Status400BadRequest)]
        public IActionResult Register(RequestUserJson request)
        {
            var useCase = new RegisterUserUseCase();

            var response = useCase.Execute(request);

            return Created(string.Empty, response);
        }
    }
}
