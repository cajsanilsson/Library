using MediatR;
using Domain;
using Infrastructure.Database;

namespace Application.BookCommands.AddBookCommand
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, Book>
    {
        private readonly FakeDatabase _fakeDatabase;

        public AddBookCommandHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task<Book> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {

            Book newBook = new()
            {
                Id = Guid.NewGuid(),
                Title = request.NewBook.Title,
                Description = request.NewBook.Description
            };


            _fakeDatabase.books.Add(newBook);
            

            return Task.FromResult(newBook);
        }



    }
}
