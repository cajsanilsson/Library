
using Domain.Models;
using MediatR;

namespace Application.UserQueries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<OperationResult<List<User>>>
    {

    }
}
