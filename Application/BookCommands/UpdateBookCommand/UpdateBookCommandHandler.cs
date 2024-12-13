using MediatR;
using Domain.Models;
using Application.Interfaces.RepositoryInterfaces;


namespace Application.BookCommands.UpdateBookCommand
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, OperationResult <Book>>
    {
        private readonly IBookRepository _bookRepository;

        public UpdateBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<Book>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            if (request.BookId == Guid.Empty)
            {
                return OperationResult<Book>.Failure("Invalid book ID.");
            }

            // Hämta boken från databasen
            var bookToUpdate = await _bookRepository.GetBookById(request.BookId);

            if (bookToUpdate == null)
            {
                return OperationResult<Book>.Failure($"Book with ID {request.BookId} not found.");
            }

            // Uppdatera titel om en ny titel tillhandahålls
            if (!string.IsNullOrEmpty(request.NewTitle))
            {
                bookToUpdate.Title = request.NewTitle;
            }

            // Uppdatera beskrivning om en ny beskrivning tillhandahålls
            if (!string.IsNullOrEmpty(request.NewDescription))
            {
                bookToUpdate.Description = request.NewDescription;
            }

            try
            {
                // Uppdatera boken i databasen
                await _bookRepository.UpdateBook(request.BookId, bookToUpdate); // Korrigera UpdateBook-metoden

                // Returnera framgångsresultat med uppdaterad bok
                return OperationResult<Book>.Successful(bookToUpdate, "Book updated successfully.");
            }
            catch (Exception ex)
            {
                // Returnera ett felresultat
                return OperationResult<Book>.Failure($"An error occurred while updating the book: {ex.Message}");
            }
        }
    }
}
