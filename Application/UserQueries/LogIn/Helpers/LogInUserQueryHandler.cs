using Domain.Models;
using Infrastructure.Database;
using MediatR;


namespace Application.UserQueries.LogIn.Helpers
{
    public class LogInUserQueryHandler : IRequestHandler<LogInUserQuery, string>
    {
        private readonly FakeDatabase _fakeDatabase;
        private readonly TokenHelper _tokenHelper;

        public LogInUserQueryHandler(FakeDatabase fakeDatabase, TokenHelper tokenHelper)
        {
            _fakeDatabase = fakeDatabase;
            _tokenHelper = tokenHelper;
        }
        public Task<string> Handle(LogInUserQuery request, CancellationToken cancellationToken)
        {
            var user = _fakeDatabase.users.FirstOrDefault(user => user.Username == request.LogInUser.Username && user.Password == request.LogInUser.Password);
            if (user == null)

            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            string token = _tokenHelper.GenerateJwtToken(user);
            return Task.FromResult(token);
        }
    }
}
