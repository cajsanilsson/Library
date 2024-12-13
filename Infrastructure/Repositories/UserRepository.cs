using Domain.Models;
using Application.Interfaces.RepositoryInterfaces;
using Infrastructure.Database;


namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDatabase _database;

        public UserRepository(LibraryDatabase database)
        {
            _database = database;
        }
        public async Task<User> AddUser(User user)
        {
            _database.Users.Add(user);
            _database.SaveChanges();
            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await Task.FromResult(_database.Users.ToList());
        }

        public Task<User> LogInUser(string username, string password)
        {
            User user = _database.Users.FirstOrDefault(user => user.Username == username && user.Password == password);
            if (user is not null)
            {
                return Task.FromResult(user);
            }
            return Task.FromResult(user);
        }
    }
}
