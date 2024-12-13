
using Domain.Models;
using MediatR;

namespace Application.UserCommands
{
    public class AddUserCommand : IRequest<OperationResult<User>>
    {
        public User NewUser { get; }

        public AddUserCommand(User newUser)
        {
            NewUser = newUser ?? throw new ArgumentNullException(nameof(newUser));
        }

    }
}
