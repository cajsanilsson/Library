
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.AuthorCommands.UpdateAuthorCommand
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand,OperationResult<Author>>
    {
        private readonly IAuthorRepository _authorRepository;

        public UpdateAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<Author>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            if (request.AuthorId == Guid.Empty)
            {
                return OperationResult<Author>.Failure("Invalid author ID.");
            }

            var authorToUpdate = await _authorRepository.GetAuthorById(request.AuthorId);

            if (authorToUpdate == null)
            {
                return OperationResult<Author>.Failure($"Author with ID {request.AuthorId} not found.");
            }

            if (!string.IsNullOrEmpty(request.UpdatedName))
            {
                authorToUpdate.Name = request.UpdatedName;
            }
            try
            {
                await _authorRepository.UpdateAuthor(request.AuthorId, authorToUpdate);

                return OperationResult<Author>.Successful(authorToUpdate);
            }
            catch (Exception ex)
            {
                return OperationResult<Author>.Failure("An error occurred while updating the author.");
            }
        }
    }
}
