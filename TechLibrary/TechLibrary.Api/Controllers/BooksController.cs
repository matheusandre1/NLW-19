using Microsoft.AspNetCore.Mvc;
using TechLibrary.Communication.Request;
using TechLibrary.Communication.Response;
using TechLibrary.Communication.UseCases.Books;

namespace TechLibrary.Api.Controllers
{
    public class BooksController  : ControllerBase
    {
        [HttpGet("Filter")]
        [ProducesResponseType(typeof(ResponseBooksJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Filter(int pageNumber, string? title)
        {
            var useCase = new FilterBookUseCase();

            var result = useCase.Execute(new RequestFilterBooksJson
            {
                PageNumber = pageNumber,
                Title = title
            });

            if (result.Books.Count >= 0)
            {
                return Ok(result);
            }
            return NoContent();
        }
    }
}
