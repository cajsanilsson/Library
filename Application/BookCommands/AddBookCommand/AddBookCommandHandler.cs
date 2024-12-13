using MediatR;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Application.Interfaces.RepositoryInterfaces;

namespace Application.BookCommands.AddBookCommand
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, OperationResult <Book>>
    {
        

        private readonly IBookRepository _bookRepository;

        public AddBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<Book>> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            if (request.NewBook == null)
            {
                return OperationResult<Book>.Failure("New book cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(request.NewBook.Title))
            {
                return OperationResult<Book>.Failure("Book title is required.");
            }

            if (string.IsNullOrWhiteSpace(request.NewBook.Description))
            {
                return OperationResult<Book>.Failure("Book description is required.");
            }

            try
            {
                // Skapa en ny bok
                Book newBook = new()
                {
                    Id = Guid.NewGuid(),
                    Title = request.NewBook.Title,
                    Description = request.NewBook.Description
                };

                // Lägg till boken i databasen
                await _bookRepository.AddBook(newBook);

                // Returnera en lyckad operation
                return OperationResult<Book>.Successful(newBook);
            }
            catch (Exception)
            {
                return OperationResult<Book>.Failure("An error occurred while adding the book.");
            }
        }
    }
}
