using Domain.Models;
using Infrastructure.Database;
using MediatR;

namespace Application.UserQueries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<User>>
    {
        private readonly FakeDatabase _fakeDatabase;

        public GetAllUsersQueryHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }
        public Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<User> allUsersFromFakeDatabase = _fakeDatabase.users;

            if (allUsersFromFakeDatabase == null || !allUsersFromFakeDatabase.Any())
            {
                throw new ArgumentException("Userlist is empty or null");
            }

            return Task.FromResult(allUsersFromFakeDatabase);
        }
    }
}
