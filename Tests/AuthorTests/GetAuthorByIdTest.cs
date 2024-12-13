
using Application.AuthorQueries.GetAuthorById;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.AuthorTests
{
    public class GetAuthorByIdTest
    {
        private LibraryDatabase CreateInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<LibraryDatabase>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new LibraryDatabase(options);
        }

        [Fact]
        public async Task GetAuthorByIdQueryHandler_Should_ReturnCorrectAuthor_WhenAuthorExists()
        {
            using var database = CreateInMemoryDatabase();
            var authorRepository = new AuthorRepository(database);
            var handler = new GetAuthorByIdQueryHandler(authorRepository);

            var authorId = Guid.NewGuid();
            var author = new Author { Id = authorId, Name = "Test Author" };
            database.Authors.Add(author);
            await database.SaveChangesAsync();

            var getAuthorsByIdQuery = new GetAuthorByIdQuery(authorId);

            var result = await handler.Handle(getAuthorsByIdQuery, CancellationToken.None);

            Assert.True(result.Success);
            Assert.Equal("Test Author", result.Data.Name);
            Assert.Equal("Operation successful", result.Message);
        }
    }
}
