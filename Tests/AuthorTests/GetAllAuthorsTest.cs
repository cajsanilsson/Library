using Application.AuthorQueries.GetAllAuthors;
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
    public class GetAllAuthorsTest
    {
        [Fact]
        public async Task GetAllAuthorsQueryHandler_Should_ReturnAllAuthorsFromFakeDatabase()
        {
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<GetAllAuthorsQueryHandler>();
            var fakeDatabase = new FakeDatabase();
            fakeDatabase.authors.Clear();
            fakeDatabase.authors.Add(new Author
            {
                Id = Guid.NewGuid(),
                Name = "Author 1"
            });
            fakeDatabase.authors.Add(new Author
            {
                Id = Guid.NewGuid(),
                Name = "Author 2"
            });

            var handler = new GetAllAuthorsQueryHandler(fakeDatabase, logger);
            var query = new GetAllAuthorsQuery();

            var authors = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(authors);
            Assert.Equal(2, authors.Count);
        }
    }
}
