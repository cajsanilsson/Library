using Application.BookCommands.UpdateBookCommand;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AuthorCommands.UpdateAuthorCommand
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Author>
    {
        private readonly FakeDatabase _fakeDatabase;

        public UpdateAuthorCommandHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task<Author> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            if (request.AuthorId == Guid.Empty)
            {
                throw new ArgumentException("Invalid author ID.", nameof(request.AuthorId));
            }

            var authorToUpdate = _fakeDatabase.authors.FirstOrDefault(a => a.Id == request.AuthorId);

            if (authorToUpdate == null)
            {
                throw new KeyNotFoundException($"Author with ID {request.AuthorId} not found.");
            }

            if (!string.IsNullOrEmpty(request.UpdatedName))
            {
                authorToUpdate.Name = request.UpdatedName;
            }

            try
            {
                return Task.FromResult(authorToUpdate);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the author.", ex);
            }
        }

    }
}
