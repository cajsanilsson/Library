using Application.BookCommands.AddBookCommand;
using Application.Interface.RepositoryInterfaces;
using Domain.Models;

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
        private readonly IAuthorRepository _authorRepository;

        public AddAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.NewAuthor.Name))
            {
                throw new ArgumentException("Author name cannot be empty.", nameof(request.NewAuthor.Name));
            }

            var existingAuthor = _authorRepository.AddAuthor(request.NewAuthor)
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

                _authorRepository.AddAuthor(request.NewAuthor);

                return await Task.FromResult(newAuthor); 
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding a new author.", ex);
            }
        }
    }
}
