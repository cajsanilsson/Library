using Domain;
using MediatR;
using Infrastructure.Database;


namespace Application.BookCommands.UpdateBookCommand
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Book>
    {
        private readonly FakeDatabase _fakeDatabase;

        public UpdateBookCommandHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            // Leta upp boken baserat på ID
            var bookToUpdate = _fakeDatabase.books.FirstOrDefault(b => b.Id == request.BookId);

            if (bookToUpdate == null)
            {
                throw new KeyNotFoundException($"Book with ID {request.BookId} not found.");
            }

            // Uppdatera bokens egenskaper
            bookToUpdate.Title = request.NewTitle ?? bookToUpdate.Title;
            bookToUpdate.Description = request.NewDescription ?? bookToUpdate.Description;

            // Returnera den uppdaterade boken
            return Task.FromResult(bookToUpdate);
        }

    }
}
