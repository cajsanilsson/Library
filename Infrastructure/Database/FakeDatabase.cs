using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public class FakeDatabase
    {
        public List <Book> books { get { return booksInDb; } set{ booksInDb = value; } }

        public List<Author> authors { get { return authorsInDb; } set { authorsInDb = value; } } 

        List<Book> booksInDb = new List<Book>
        {
            new Book (1, "FirstBook", "The first book"),
            new Book (2, "SecondBook", "The second book"),
            new Book (3, "ThirdBook", "The third book"),
            new Book (4, "FourthBook", "The fourth book"),
            new Book (5, "Fi´fthBook", "The fifth book")
        };

        List<Author> authorsInDb = new List<Author>
        {
            new Author (1, "Author1"),
            new Author (2, "Author2"),
            new Author (3, "Author3")
        };
        public Book AddNewBook(Book book)
        {
            booksInDb.Add (book);
            return book;
        }
    }
}
