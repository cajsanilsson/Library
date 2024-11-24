using MediatR;
using Infrastructure.Database;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.BookCommands.AddBookCommand
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, Book>
    {
        private readonly FakeDatabase _fakeDatabase;

        private readonly ILogger<AddBookCommandHandler> _logger;

        public AddBookCommandHandler(FakeDatabase fakeDatabase, ILogger<AddBookCommandHandler> logger )
        {
            _fakeDatabase = fakeDatabase;
            _logger = logger;
        }

        public Task<Book> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            if (request.NewBook == null)
            {
                _logger.LogError("Attempted to add a null book.");
                throw new ArgumentNullException(nameof(request.NewBook), "New book cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(request.NewBook.Title))
            {
                _logger.LogError("Book title is required.");
                throw new ArgumentException("Book title is required.");
            }

            if (string.IsNullOrWhiteSpace(request.NewBook.Description))
            {
                _logger.LogError("Book description is required.");
                throw new ArgumentException("Book description is required.");
            }

            Book newBook = new()
            {
                Id = Guid.NewGuid(),
                Title = request.NewBook.Title,
                Description = request.NewBook.Description
            };

            _fakeDatabase.books.Add(newBook);

            _logger.LogInformation("Book successfully added with ID: {BookId}", newBook.Id);

            return Task.FromResult(newBook);
        }

    



    }
}
