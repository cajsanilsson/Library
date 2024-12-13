
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;


namespace Application.AuthorCommands.AddAuthorCommand
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, OperationResult<Author>>
    {
        private readonly IAuthorRepository _authorRepository;

        public AddAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<Author>> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.NewAuthor.Name))
            {
                throw new ArgumentException("Author name cannot be empty.", nameof(request.NewAuthor.Name));
            }

            try
            {
                Author newAuthor = new()
                {
                    Id = Guid.NewGuid(),
                    Name = request.NewAuthor.Name
                };

                await _authorRepository.AddAuthor(newAuthor);

                return OperationResult<Author>.Successful(newAuthor);
            }
            catch (Exception ex)
            {
                return OperationResult<Author>.Failure("An error occurred while adding a new author.");
            }
        }
    }
}
