using Domain;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BookQueries.GetBookById
{
    public class GetBookByIdQueryHandler
    {
        private readonly FakeDatabase _fakeDatabase;

        public GetBookByIdQueryHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Book Handle(GetBookByIdQuery query)
        {
            return _fakeDatabase.books.FirstOrDefault(b => b.Id == query.Id);
        }
    }
}
