using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;


namespace Application.UserQueries.LogIn.Helpers
{
    public class LogInUserQueryHandler : IRequestHandler<LogInUserQuery, OperationResult<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenHelper _tokenHelper;

        public LogInUserQueryHandler(IUserRepository userRepository, TokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
        }
        public async Task<OperationResult<string>> Handle(LogInUserQuery request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrWhiteSpace(request.LogInUser.Username) || string.IsNullOrWhiteSpace(request.LogInUser.Password))
            {
                return OperationResult<string>.Failure("Username and password cannot be empty");
            }

            var user = await _userRepository.LogInUser(request.LogInUser.Username, request.LogInUser.Password);

            if (user == null)
            {
                return OperationResult<string>.Failure("Invalid username or password");
            }

            string token = _tokenHelper.GenerateJwtToken(user);

            return OperationResult<string>.Successful(token, "Login successful");
        }
    }
}
