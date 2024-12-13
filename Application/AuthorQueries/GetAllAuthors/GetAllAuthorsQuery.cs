using MediatR;
using Domain.Models;

namespace Application.AuthorQueries.GetAllAuthors
{
    public class GetAllAuthorsQuery : IRequest<OperationResult<List <Author>>>
    {
    }
}
