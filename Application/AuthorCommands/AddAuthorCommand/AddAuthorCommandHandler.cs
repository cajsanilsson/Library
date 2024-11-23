using Application.BookCommands.AddBookCommand;
using Domain;
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

        public Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {

            Author newAuthor = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewAuthor.Name
            };


            _fakeDatabase.authors.Add(newAuthor);


            return Task.FromResult(newAuthor);
        }



    }
}
