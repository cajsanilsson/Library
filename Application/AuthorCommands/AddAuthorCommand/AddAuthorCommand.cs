using Domain.Models;
using MediatR;

namespace Application.AuthorCommands.AddAuthorCommand
{
    public class AddAuthorCommand : IRequest<Author>
    {
        public Author NewAuthor { get; set; } // Använd set för att tillåta deserialisering

        // Standardkonstruktor som används av deserialisering
        public AddAuthorCommand()
        {
        }
       


    }
}
