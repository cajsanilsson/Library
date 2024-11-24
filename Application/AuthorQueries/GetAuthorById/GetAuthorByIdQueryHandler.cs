using Application.BookQueries.GetBookById;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Application.AuthorQueries.GetAuthorById
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author>
    {
        private readonly FakeDatabase _fakeDatabase;
        private readonly ILogger<GetAuthorByIdQueryHandler> _logger;

        public GetAuthorByIdQueryHandler(FakeDatabase fakeDatabase, ILogger<GetAuthorByIdQueryHandler> logger)
        {
            _fakeDatabase = fakeDatabase;
            _logger = logger;
        }

        public async Task<Author> Handle(GetAuthorByIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                if (query.Id == Guid.Empty)
                {
                    throw new ArgumentException("Invalid author ID.", nameof(query.Id));
                }
                var authorOfChoice = _fakeDatabase.authors.FirstOrDefault(author => author.Id == query.Id);

                if (authorOfChoice == null)
                {
                    throw new KeyNotFoundException($"Author with ID {query.Id} not found.");
                }

                return authorOfChoice;
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid author ID provided.");
                throw; 
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Author not found.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving author by ID.");
                throw new ApplicationException("An error occurred while retrieving the author.", ex);
            }
        }
    }
}
