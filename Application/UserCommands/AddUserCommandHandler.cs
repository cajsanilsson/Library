

using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.UserCommands
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, OperationResult<User>>
    {
        private readonly IUserRepository _userRepository;

        public AddUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task <OperationResult<User>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.NewUser.Username))
            {
                throw new ArgumentException("Username cannot be empty.", nameof(request.NewUser.Username));
            }

            try
            {
                User newUser = new()
                {
                    Id = Guid.NewGuid(),
                    Username = request.NewUser.Username
                };

                await _userRepository.AddUser(newUser);

                return OperationResult<User>.Successful(newUser);
            }
            catch (Exception ex)
            {
                return OperationResult<User>.Failure("An error occurred while adding a new user.");
            }
           
        }
    }

}
