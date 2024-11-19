using Domain;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BookQueries.GetAllBooks
{
    public class GetAllBooksQueryHandler
    {
        private readonly FakeDatabase _fakeDatabase;

        public GetAllBooksQueryHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public List<Book> Handle(GetAllBooksQuery query)
        {
            return _fakeDatabase.books;
        }

        
    }
}
