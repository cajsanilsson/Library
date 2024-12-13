
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Application.AuthorQueries.GetAuthorById
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, OperationResult<Author>>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<Author>> Handle(GetAuthorByIdQuery query, CancellationToken cancellationToken)
        {
            if (query.Id == Guid.Empty)
            {
                return OperationResult<Author>.Failure("Invalid author ID.");
            }

            try
            {
                var author = await _authorRepository.GetAuthorById(query.Id);

                if (author == null)
                {
                    return OperationResult<Author>.Failure($"Author with ID {query.Id} not found.");
                }

                return OperationResult<Author>.Successful(author);
            }
            catch (Exception)
            {
                return OperationResult<Author>.Failure("An error occurred while retrieving the author.");
            }
        }
    }
}
