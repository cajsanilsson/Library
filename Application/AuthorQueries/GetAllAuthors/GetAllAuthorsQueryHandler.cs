
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.AuthorQueries.GetAllAuthors
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, OperationResult <List<Author>>>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAllAuthorsQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<List<Author>>> Handle(GetAllAuthorsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var authors = await _authorRepository.GetAllAuthors();

                if (authors == null || authors.Count == 0)
                {
                    return OperationResult<List<Author>>.Failure("No authors found.");
                }

                return OperationResult<List<Author>>.Successful(authors);
            }
            catch (Exception ex)
            {
                return OperationResult<List<Author>>.Failure("An error occurred while retrieving the authors.");
            }
        }


    }
}
