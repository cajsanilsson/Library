using Domain.Models;
using Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.BookQueries.GetBookById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly FakeDatabase _fakeDatabase;
        private readonly ILogger<GetBookByIdQueryHandler> _logger;

        public GetBookByIdQueryHandler(FakeDatabase fakeDatabase, ILogger<GetBookByIdQueryHandler> logger)
        {
            _fakeDatabase = fakeDatabase;
            _logger = logger;
        }

        public Task<Book> Handle(GetBookByIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                if (query.Id == Guid.Empty)
                {
                    _logger.LogWarning("Invalid book ID provided: {Id}", query.Id);
                    throw new ArgumentException("Invalid book ID", nameof(query.Id));
                }

                var book = _fakeDatabase.books.FirstOrDefault(b => b.Id == query.Id);

                if (book == null)
                {
                    _logger.LogWarning("Book with ID {Id} not found.", query.Id);
                    throw new KeyNotFoundException($"Book with ID {query.Id} not found.");
                }

                _logger.LogInformation("Book with ID {Id} found and returned.", query.Id);

                return Task.FromResult(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching book with ID {Id}.", query.Id);
                throw; 
            }
        }
    }
}
