using Domain.Models;
using MediatR;

namespace Application.UserQueries.LogIn.Helpers
{
    public class LogInUserQuery : IRequest<OperationResult<String>>
    {

        public LogInUserQuery(User logInUser)
        {
            LogInUser = logInUser;
        }

        public User LogInUser { get; }
    }
}
