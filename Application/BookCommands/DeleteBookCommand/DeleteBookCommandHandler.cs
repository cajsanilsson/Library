using Domain;
using Infrastructure.Database;
using MediatR;

namespace Application.BookCommands.DeleteBookCommand
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Book>
    {
        private readonly FakeDatabase _fakeDatabase;

        public DeleteBookCommandHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task<Book> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var bookToDelete = _fakeDatabase.books.FirstOrDefault(b => b.Id == request.Id);


            _fakeDatabase.books.Remove(bookToDelete);

            return Task.FromResult(bookToDelete);
        }
    }
}
