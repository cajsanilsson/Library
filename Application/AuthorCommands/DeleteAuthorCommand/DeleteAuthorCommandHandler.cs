using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;


namespace Application.AuthorCommands.DeleteAuthorCommand
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, OperationResult<Author>>
    {
        private readonly IAuthorRepository _authorRepository;

        public DeleteAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<Author>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            {
                return OperationResult<Author>.Failure("Invalid author ID.");
            }

            var authorToDelete = await _authorRepository.GetAuthorById(request.Id);

            if (authorToDelete == null)
            {
                return OperationResult<Author>.Failure($"Author with ID {request.Id} not found.");
            }

            try
            {
                await _authorRepository.DeleteAuthor(request.Id);
                return OperationResult<Author>.Successful(authorToDelete);
            }
            catch (Exception ex)
            {
                return OperationResult<Author>.Failure("An error occurred while deleting the author.");
            }
        }
    }
}


