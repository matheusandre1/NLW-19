using Microsoft.AspNetCore.Mvc;
using TechLibrary.Communication.Request;
using TechLibrary.Communication.Response;
using TechLibrary.Communication.UseCases.Login.DoLogin;

namespace TechLibrary.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorMessages), StatusCodes.Status401Unauthorized)]
        public IActionResult DoLogin( RequestLoginJson request)
        {
            var useCase = new DoLoginUseCase();

            var response = useCase.Execute(request);

            return Ok(response);


        }
    }
}
