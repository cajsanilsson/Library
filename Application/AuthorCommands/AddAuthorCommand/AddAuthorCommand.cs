using Domain.Models;
using MediatR;

namespace Application.AuthorCommands.AddAuthorCommand
{
    public class AddAuthorCommand : IRequest<OperationResult<Author>>
    {
        public Author NewAuthor { get; }

        public AddAuthorCommand(Author newAuthor)
        {
            NewAuthor = newAuthor ?? throw new ArgumentNullException(nameof(newAuthor));
        }



    }
}
