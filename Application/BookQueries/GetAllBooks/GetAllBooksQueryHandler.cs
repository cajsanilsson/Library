
using MediatR;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Application.Interfaces.RepositoryInterfaces;

namespace Application.BookQueries.GetAllBooks
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, OperationResult <List<Book>>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBooksQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<List<Book>>> Handle(GetAllBooksQuery query, CancellationToken cancellationToken)
        {
            try
            {
                // Hämta alla böcker från databasen
                var books = await _bookRepository.GetAllBooks();

                // Kontrollera om listan är null eller tom
                if (books == null || !books.Any())
                {
                    return OperationResult<List<Book>>.Failure("No books found.");
                }

                // Returnera framgång med böcker
                return OperationResult<List<Book>>.Successful(books);
            }
            catch (Exception)
            {
                // Fånga oväntade fel
                return OperationResult<List<Book>>.Failure("An error occurred while fetching books.");
            }
        }


    }
}
