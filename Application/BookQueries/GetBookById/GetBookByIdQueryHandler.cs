using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.BookQueries.GetBookById
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery,OperationResult<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByIdQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<Book>> Handle(GetBookByIdQuery query, CancellationToken cancellationToken)
        {

            if (query.Id == Guid.Empty)
            {
                return OperationResult<Book>.Failure("Invalid book ID.");
            }

            try
            {
                // Hämtar författaren från repository
                var book = await _bookRepository.GetBookById(query.Id);

                if (book == null)
                {
                    return OperationResult<Book>.Failure($"Book with ID {query.Id} not found.");
                }

                return OperationResult<Book>.Successful(book);
            }
            catch (Exception)
            {
                return OperationResult<Book>.Failure("An error occurred while retrieving the book.");
            }
        }
    }
}
