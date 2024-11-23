
using Domain;
using Infrastructure.Database;
using MediatR;


namespace Application.AuthorCommands.DeleteAuthorCommand
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Author>
    {
        private readonly FakeDatabase _fakeDatabase;

        public DeleteAuthorCommandHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task<Author> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorToDelete = _fakeDatabase.authors.FirstOrDefault(b => b.Id == request.Id);


            _fakeDatabase.authors.Remove(authorToDelete);

            return Task.FromResult(authorToDelete);
        }
    }
}


