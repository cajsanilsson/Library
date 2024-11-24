using Application.BookCommands.AddBookCommand;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AuthorCommands.AddAuthorCommand
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, Author>
    {
        private readonly FakeDatabase _fakeDatabase;

        public AddAuthorCommandHandler(FakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public async Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.NewAuthor.Name))
            {
                throw new ArgumentException("Author name cannot be empty.", nameof(request.NewAuthor.Name));
            }

            var existingAuthor = _fakeDatabase.authors
                .FirstOrDefault(a => a.Name.Equals(request.NewAuthor.Name, StringComparison.OrdinalIgnoreCase));
            if (existingAuthor != null)
            {
                throw new InvalidOperationException($"An author with the name '{request.NewAuthor.Name}' already exists.");
            }

            try
            {
                Author newAuthor = new()
                {
                    Id = Guid.NewGuid(),
                    Name = request.NewAuthor.Name
                };

                _fakeDatabase.authors.Add(newAuthor);

                return await Task.FromResult(newAuthor); 
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding a new author.", ex);
            }
        }
    }
}
