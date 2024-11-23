using Application.BookCommands.UpdateBookCommand;
using Domain;
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
            // Leta upp boken baserat på ID
            var authorToUpdate = _fakeDatabase.authors.FirstOrDefault(b => b.Id == request.AuthorId);

            if (authorToUpdate == null)
            {
                throw new KeyNotFoundException($"Book with ID {request.AuthorId} not found.");
            }

            // Uppdatera bokens egenskaper
            authorToUpdate.Name = request.UpdatedName ?? authorToUpdate.Name;
            

            // Returnera den uppdaterade boken
            return Task.FromResult(authorToUpdate);
        }

    }
}
