using MediatR;
using Domain;

namespace Application.BookCommands.AddBookCommand
{
    public class AddBookCommand : IRequest<Book>
    {
       

        public AddBookCommand(Book newBook)
        {
            NewBook = newBook;
            
        }

        public Book NewBook { get;}


    }
   
}
