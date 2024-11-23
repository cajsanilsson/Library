using Domain;
using MediatR;

namespace Application.AuthorQueries.GetAuthorById
{
    public class GetAuthorByIdQuery : IRequest<Author>
    {
        public GetAuthorByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
