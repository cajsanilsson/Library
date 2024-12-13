using MediatR;
using Domain.Models;

namespace Application.BookCommands.AddBookCommand
{
    public class AddBookCommand : IRequest<OperationResult<Book>>
    {
       

        public AddBookCommand(Book newBook)
        {
            NewBook = newBook;
            
        }

        public Book NewBook { get;}


    }
   
}
