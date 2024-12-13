using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.UserQueries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, OperationResult<List<User>>>
    {
        private readonly IUserRepository _userRepository;
        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<OperationResult<List<User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<User> allUsers = await _userRepository.GetAllUsers();

                if (allUsers == null || !allUsers.Any())
                {
                    return OperationResult<List<User>>.Failure("User list is empty or null", "No users found");
                }

                return OperationResult<List<User>>.Successful(allUsers, "Users fetched successfully");
            }
            catch (Exception ex)
            {
                return OperationResult<List<User>>.Failure($"An error occurred while fetching users: {ex.Message}");
            }
        }
    }
}
