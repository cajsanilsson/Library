using Application.AuthorQueries.GetAllAuthors;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.AuthorTests
{
    public class GetAllAuthorsTest
    {
        private LibraryDatabase CreateInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<LibraryDatabase>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new LibraryDatabase(options);
        }

        [Fact]
        public async Task GetAllAuthorsQueryHandler_Should_ReturnAllAuthorsFromFakeDatabase()
        {
            using var database = CreateInMemoryDatabase();
            var authorRepository = new AuthorRepository(database);
            var handler = new GetAllAuthorsQueryHandler(authorRepository);

            database.Authors.Add(new Author { Id = Guid.NewGuid(), Name = "Test Author1" });
            database.Authors.Add(new Author { Id = Guid.NewGuid(), Name = "Test Author2" });
            await database.SaveChangesAsync();

            var getAllAuthorsQuery = new GetAllAuthorsQuery();

            var result = await handler.Handle(getAllAuthorsQuery, CancellationToken.None);

            Assert.True(result.Success);
            Assert.Equal(2, result.Data.Count);
            Assert.Equal("Operation successful", result.Message);
        }
    }
}
