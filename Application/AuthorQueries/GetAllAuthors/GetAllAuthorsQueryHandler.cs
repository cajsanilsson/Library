using Application.BookQueries.GetAllBooks;
using Domain;
using Infrastructure.Database;
using MediatR;


namespace Application.AuthorQueries.GetAllAuthors
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, List<Author>>
    {
        private readonly FakeDatabase _fakeDatabase;

        public GetAllAuthorsQueryHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task<List<Author>> Handle(GetAllAuthorsQuery query, CancellationToken cancellationToken)
        {
            List<Author> authors = _fakeDatabase.authors;
            return Task.FromResult(authors);
        }


    }
}
