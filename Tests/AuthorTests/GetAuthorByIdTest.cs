using Application.AuthorQueries.GetAuthorById;
using Domain;
using Infrastructure.Database;
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
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var author = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Test Author"
            };

            fakeDatabase.authors.Add(author);

            var handler = new GetAuthorByIdQueryHandler(fakeDatabase);
            var query = new GetAuthorByIdQuery(author.Id);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(author.Id, result.Id);
            Assert.Equal(author.Name, result.Name);
        }

        [Fact]
        public async Task GetAuthorByIdQueryHandler_Should_ReturnNull_WhenAuthorDoesNotExist()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            var handler = new GetAuthorByIdQueryHandler(fakeDatabase);
            var query = new GetAuthorByIdQuery(Guid.NewGuid());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
