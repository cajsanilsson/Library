using Application.AuthorQueries.GetAllAuthors;
using Application.AuthorQueries.GetAuthorById;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.AuthorTests
{
    public class GetAuthorByIdTest
    {
        [Fact]
        public async Task GetAuthorByIdQueryHandler_Should_ReturnCorrectAuthor_WhenAuthorExists()
        {
            var fakeDatabase = new FakeDatabase();
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<GetAuthorByIdQueryHandler>();
            var author = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Test Author"
            };

            fakeDatabase.authors.Add(author);

            var handler = new GetAuthorByIdQueryHandler(fakeDatabase, logger);
            var query = new GetAuthorByIdQuery(author.Id);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(author.Id, result.Id);
            Assert.Equal(author.Name, result.Name);
        }

        [Fact]
        public async Task GetAuthorByIdQueryHandler_Should_ThrowKeyNotFoundException_WhenAuthorDoesNotExist()
        {
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<GetAuthorByIdQueryHandler>();
            var fakeDatabase = new FakeDatabase();

            var handler = new GetAuthorByIdQueryHandler(fakeDatabase, logger);
            var query = new GetAuthorByIdQuery(Guid.NewGuid());

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(query, CancellationToken.None));

            Assert.Equal($"Author with ID {query.Id} not found.", exception.Message);
        }
    }
}
