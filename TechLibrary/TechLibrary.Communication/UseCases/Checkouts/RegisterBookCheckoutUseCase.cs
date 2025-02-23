using TechLibrary.Communication.Services;
using TechLibrary.Domain.Entities;
using TechLibrary.Exception;
using TechLibrary.Infrastruture.DataAccess;

namespace TechLibrary.Communication.UseCases.Checkouts
{
    public class RegisterBookCheckoutUseCase
    {
        private const int MaxLoanDays = 30;
        private readonly LoggedUserService _loggedUserService;

        public RegisterBookCheckoutUseCase(LoggedUserService loggedUserService)
        {
            _loggedUserService = loggedUserService;
        }
        public void Execute(Guid bookId)
        {
            var dbContext = new TechLibraryDbContext();

            Validate(dbContext, bookId);

            var user = _loggedUserService.User(dbContext);

            var entity = new Checkout
            {
                UserId = user.Id,
                BookId = bookId,
                ExpectedReturnDate = DateTime.UtcNow.AddDays(MaxLoanDays)                
            };

            dbContext.Checkouts.Add(entity);

            dbContext.SaveChanges();
        }

        private void Validate(TechLibraryDbContext techLibraryDbContext, Guid bookId) 
        {
            var book = techLibraryDbContext.Books.FirstOrDefault(x => x.Id == bookId);

            if (book == null)
            {
                throw new NotFoundException("Livro não encontrado");
            }

           var amountBooknotReturned = techLibraryDbContext
                .Checkouts
                .Count(x => x.BookId == bookId && x.ReturnedDate == null);


            if (amountBooknotReturned == book.Amount)
            {
                throw new ConflitcException("Livro não está disponivel para empréstimo");
            }
        }
    }
}
