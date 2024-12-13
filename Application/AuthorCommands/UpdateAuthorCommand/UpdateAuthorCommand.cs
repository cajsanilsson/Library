using Domain.Models;
using MediatR;

namespace Application.AuthorCommands.UpdateAuthorCommand
{
    public class UpdateAuthorCommand : IRequest<OperationResult<Author>>
    {
        public Guid AuthorId { get; set; }
        public string? UpdatedName { get; set; }

        public UpdateAuthorCommand() { }

        public UpdateAuthorCommand(Guid updatedId, string? updatedName)
        {
            AuthorId = updatedId;
            UpdatedName = updatedName;

           
        }
    }
}
