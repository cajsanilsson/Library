using MediatR;
using Infrastructure.Database;
using Domain.Models;
using Microsoft.Extensions.Logging;


namespace Application.BookCommands.UpdateBookCommand
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Book>
    {
        private readonly FakeDatabase _fakeDatabase;
        
        private readonly ILogger<UpdateBookCommandHandler> _logger;

        public UpdateBookCommandHandler(FakeDatabase fakeDatabase, ILogger<UpdateBookCommandHandler> logger)
        {
            _fakeDatabase = fakeDatabase;
            _logger = logger;
        }

        public Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            if (request.BookId == Guid.Empty)
            {
                _logger.LogError("Attempted to update a book with an empty ID.");
                throw new ArgumentException("Invalid book ID.");
            }

            var bookToUpdate = _fakeDatabase.books.FirstOrDefault(b => b.Id == request.BookId);

            if (bookToUpdate == null)
            {
                _logger.LogWarning("Book with ID {BookId} not found.", request.BookId);
                throw new KeyNotFoundException($"Book with ID {request.BookId} not found.");
            }

            bookToUpdate.Title = request.NewTitle ?? bookToUpdate.Title;
            bookToUpdate.Description = request.NewDescription ?? bookToUpdate.Description;

            _logger.LogInformation("Successfully updated book with ID {BookId}. New Title: {NewTitle}, New Description: {NewDescription}",
                request.BookId, request.NewTitle, request.NewDescription);

            return Task.FromResult(bookToUpdate);
        }
    }
}
