using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
