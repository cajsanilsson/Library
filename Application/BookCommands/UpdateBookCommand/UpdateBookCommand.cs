using Domain.Models;
using MediatR;

namespace Application.BookCommands.UpdateBookCommand
{
    public class UpdateBookCommand : IRequest<OperationResult <Book>>
    {
        
            public Guid BookId { get; }
            public string? NewTitle { get; }
            public string? NewDescription { get; }

            public UpdateBookCommand(Guid bookId, string? newTitle, string? newDescription)
            {
                BookId = bookId;
                NewTitle = newTitle;
                NewDescription = newDescription;
            }
        



    }
}
