using MediatR;
using Domain;

namespace Application.AuthorQueries.GetAllAuthors
{
    public class GetAllAuthorsQuery : IRequest<List <Author>>
    {
    }
}
