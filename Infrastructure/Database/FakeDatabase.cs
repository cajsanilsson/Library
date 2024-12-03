using Domain.Models;

namespace Infrastructure.Database
{
    public class FakeDatabase
    {
        public List<Book> books
        {
            get { return allBooksFromDB; }
            set { allBooksFromDB = value; }
        }

        List<Book> allBooksFromDB = new List<Book>()
        {
            new Book { Id = Guid.NewGuid(), Title = "FirstBoook", Description = "The first book" },
            new Book { Id = Guid.NewGuid(), Title = "SecondBook", Description = "The second book" },
            new Book { Id = Guid.NewGuid(), Title = "ThirdBook", Description = "The third book" },
            new Book { Id = Guid.NewGuid(), Title = "FourthBook", Description = "The fourth book" },
            new Book { Id = Guid.NewGuid(), Title = "FifthBook", Description = "The fifth book" },
        };

        public List<Author> authors
        {
            get { return allAuthorsFromDB; }
            set { allAuthorsFromDB = value; }
        }

        List<Author> allAuthorsFromDB = new List<Author>()
        {
            new Author { Id = Guid.NewGuid(), Name = "Author1" },
            new Author { Id = Guid.NewGuid(), Name = "Author2" },
            new Author { Id = Guid.NewGuid(), Name = "Author3" },
        };

        public List<User> users
        {
            get { return allUsersFromDB; }
            set { allUsersFromDB = value; }
        }

        List<User> allUsersFromDB = new List<User>()
        {
            new User { Id = Guid.NewGuid(),Username = "User1"},
            new User { Id = Guid.NewGuid(),Username = "User2"}
        };
    }
}
