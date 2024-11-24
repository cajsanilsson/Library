using Domain.Models;
using MediatR;

namespace Application.AuthorCommands.DeleteAuthorCommand
{
    public class DeleteAuthorCommand : IRequest<Author>
    {
        public  Guid Id { get; }

        public DeleteAuthorCommand(Guid id)
        {
            Id = id;
        }
    }
}
