using Domain.Models;
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
            if (request.Id == Guid.Empty)
            {
                throw new ArgumentException("Invalid author ID.", nameof(request.Id));
            }

            var authorToDelete = _fakeDatabase.authors.FirstOrDefault(a => a.Id == request.Id);

            if (authorToDelete == null)
            {
                throw new KeyNotFoundException($"Author with ID {request.Id} not found.");
            }

            try
            {
                _fakeDatabase.authors.Remove(authorToDelete);

                return Task.FromResult(authorToDelete);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting the author.", ex);
            }
        }

    }
}


