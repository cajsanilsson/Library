

using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.UserCommands
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
    {
        private readonly FakeDatabase _fakeDatabase;

        public AddUserCommandHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }
        public Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.NewUser == null ||
              string.IsNullOrWhiteSpace(request.NewUser.Username) ||
              string.IsNullOrWhiteSpace(request.NewUser.Password))
            {
                throw new ArgumentException("Author name and description cannot be empty or null");
            }

            User userToCreate = new()
            {
                Id = Guid.NewGuid(),
                Username = request.NewUser.Username,
                Password = request.NewUser.Password,
            };

            _fakeDatabase.users.Add(userToCreate);

            return Task.FromResult(userToCreate);
        }
    }

}
