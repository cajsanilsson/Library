using Application.AuthorQueries.GetAllAuthors;
using Domain;
using Infrastructure.Database;
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
            // Arrange
            var fakeDatabase = new FakeDatabase();
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

            var handler = new GetAllAuthorsQueryHandler(fakeDatabase);
            var query = new GetAllAuthorsQuery();

            // Act
            var authors = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(authors);
            Assert.Equal(2, authors.Count); 
        }
    }
}
