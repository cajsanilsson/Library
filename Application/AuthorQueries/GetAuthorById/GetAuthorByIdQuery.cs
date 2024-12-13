using Domain.Models;
using MediatR;

namespace Application.AuthorQueries.GetAuthorById
{
    public class GetAuthorByIdQuery : IRequest<OperationResult<Author>>
    {
        public GetAuthorByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
