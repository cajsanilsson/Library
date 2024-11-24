using Domain.Models;
using Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.BookCommands.DeleteBookCommand
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Book>
    {
        private readonly FakeDatabase _fakeDatabase;

        private readonly ILogger<DeleteBookCommandHandler> _logger;

        public DeleteBookCommandHandler(FakeDatabase fakeDatabase, ILogger<DeleteBookCommandHandler> logger)
        {
            _fakeDatabase = fakeDatabase;
            _logger = logger;
        }

        public Task<Book> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                _logger.LogError("Attempted to delete a book with an empty ID.");
                throw new ArgumentException("Invalid book ID.");
            }

            var bookToDelete = _fakeDatabase.books.FirstOrDefault(b => b.Id == request.Id);

            if (bookToDelete == null)
            {
                _logger.LogWarning("Book with ID {BookId} not found.", request.Id);
                throw new KeyNotFoundException($"Book with ID {request.Id} not found.");
            }

            _fakeDatabase.books.Remove(bookToDelete);

            _logger.LogInformation("Successfully deleted book with ID {BookId}.", request.Id);

            return Task.FromResult(bookToDelete);
        }

        
    }
}
