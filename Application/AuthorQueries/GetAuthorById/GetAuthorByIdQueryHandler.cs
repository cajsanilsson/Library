using Application.BookQueries.GetBookById;
using Domain;
using Infrastructure.Database;
using MediatR;


namespace Application.AuthorQueries.GetAuthorById
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author>
    {
        private readonly FakeDatabase _fakeDatabase;

        public GetAuthorByIdQueryHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task<Author> Handle(GetAuthorByIdQuery query, CancellationToken cancellationToken)
        {
            Author authorOfChoice = _fakeDatabase.authors.FirstOrDefault(author => author.Id == query.Id)!;
            return Task.FromResult(authorOfChoice);
        }
    }
}
