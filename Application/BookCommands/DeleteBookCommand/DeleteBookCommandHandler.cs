using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.BookCommands.DeleteBookCommand
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, OperationResult <Book>>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<Book>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                return OperationResult<Book>.Failure("Invalid book ID.");
            }

            var bookToDelete = await _bookRepository.GetBookById(request.Id);

            if (bookToDelete == null)
            {
                return OperationResult<Book>.Failure($"Author with ID {request.Id} not found.");
            }

            try
            {
                await _bookRepository.DeleteBook(request.Id);

                // Return success result with the deleted author
                return OperationResult<Book>.Successful(bookToDelete);
            }
            catch (Exception ex)
            {
                // Return failure result with error message
                return OperationResult<Book>.Failure("An error occurred while deleting the author.");
            }
        }
    }
}
