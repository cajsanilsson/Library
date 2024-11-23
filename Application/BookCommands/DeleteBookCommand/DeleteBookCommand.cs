using MediatR;
using Domain;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Application.BookCommands.DeleteBookCommand
{
    public class DeleteBookCommand : IRequest<Book>
    {
        public Guid Id { get; }

        public DeleteBookCommand(Guid id)
        {
            Id = id;
        }
    }
}
