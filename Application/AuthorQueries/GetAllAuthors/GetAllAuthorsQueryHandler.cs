using Application.BookQueries.GetAllBooks;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Application.AuthorQueries.GetAllAuthors
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, List<Author>>
    {
        private readonly FakeDatabase _fakeDatabase;
        private readonly ILogger<GetAllAuthorsQueryHandler> _logger;
        

        public GetAllAuthorsQueryHandler(FakeDatabase fakeDatabase, ILogger<GetAllAuthorsQueryHandler> logger)
        {
            _fakeDatabase = fakeDatabase;
            _logger = logger;
        }

        public Task<List<Author>> Handle(GetAllAuthorsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                if (_fakeDatabase.authors == null)
                {
                    throw new InvalidOperationException("The authors collection is not available.");
                }

                return Task.FromResult(_fakeDatabase.authors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving authors from the database.");
                throw new ApplicationException("An error occurred while retrieving the authors.", ex);
            }
        }


    }
}
