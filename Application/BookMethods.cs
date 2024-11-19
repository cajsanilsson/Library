using Domain;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class BookMethods
    {
        private readonly FakeDatabase _fakeDatabase;

        public BookMethods(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        //Här ska det vara CQRS
        public Book AddNewBook()
        {
            Book newBookToAdd = new Book(1, "Bok", "Good Book");

            return _fakeDatabase.AddNewBook(newBookToAdd);

        }

        public void DeleteBook()
        {

        }

        public void GetBooks()
        {

        }

        public void UpdateBook()
        {

        }
    }
}
