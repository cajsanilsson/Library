using Domain;
using MediatR;

namespace Application.AuthorCommands.AddAuthorCommand
{
    public class AddAuthorCommand : IRequest<Author>
    {
        public AddAuthorCommand(Author author)
        {
            NewAuthor = author;

        }

        public Author NewAuthor { get; }


    }
}
