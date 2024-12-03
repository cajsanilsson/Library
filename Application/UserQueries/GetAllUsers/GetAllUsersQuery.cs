
using Domain.Models;
using MediatR;

namespace Application.UserQueries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<User>>
    {

    }
}
