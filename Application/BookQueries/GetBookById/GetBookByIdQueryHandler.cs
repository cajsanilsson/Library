using Domain;
using Infrastructure.Database;
using MediatR;

namespace Application.BookQueries.GetBookById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly FakeDatabase _fakeDatabase;

        public GetBookByIdQueryHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task <Book> Handle(GetBookByIdQuery query, CancellationToken cancellationToken)
        {
            Book bookOfChoice = _fakeDatabase.books.FirstOrDefault(book => book.Id == query.Id)!;
            return Task.FromResult(bookOfChoice);
        }
    }
}
