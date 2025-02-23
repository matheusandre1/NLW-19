using TechLibrary.Communication.Request;
using TechLibrary.Communication.Response;
using TechLibrary.Infrastruture.DataAccess;

namespace TechLibrary.Communication.UseCases.Books
{
    public class FilterBookUseCase
    {
        private const int PageSize = 10;
        public ResponseBooksJson Execute(RequestFilterBooksJson request)
        {
            var dbContext = new TechLibraryDbContext();

            var query = dbContext.Books.AsQueryable();

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(x => x.Title.Contains(request.Title));
            }

            var books = query
                .OrderBy(x => x.Title)
                .Skip((request.PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            var totalCount = 0;
            if(string.IsNullOrWhiteSpace(request.Title))
            {
                totalCount = dbContext.Books.Count();
            }
            else
            {
                totalCount = dbContext.Books.Count(x => x.Title.Contains(request.Title));
            }
            return new ResponseBooksJson
                {
                    Pagination = new ResponsePaginationJson
                    {
                        PageNumber = request.PageNumber,
                        TotalCount = totalCount
                    },
                    Books = books.Select(x => new ResponseBookJson
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Author = x.Author


                    }).ToList()

                };
        }
    }
}
