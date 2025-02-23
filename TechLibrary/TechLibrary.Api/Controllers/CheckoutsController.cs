using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechLibrary.Communication.Services;
using TechLibrary.Communication.UseCases.Checkouts;

namespace TechLibrary.Api.Controllers
{
    public class CheckoutsController : ControllerBase
    {
        [HttpPost]
        [Route("{bookId}")]
        [Authorize]
        public IActionResult BooksCheckout(Guid bookId)
        {
            var loggedUser = new LoggedUserService(HttpContext);
            var useCase = new RegisterBookCheckoutUseCase(loggedUser);

            useCase.Execute(bookId);

            return NoContent();
        }
    }
}
