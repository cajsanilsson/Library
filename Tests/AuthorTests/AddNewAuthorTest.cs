using Application.AuthorCommands.AddAuthorCommand;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.AuthorTests
{
    public class AddNewAuthorTest
    {
        private LibraryDatabase CreateInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<LibraryDatabase>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new LibraryDatabase(options);
        }

        [Fact]
        public async Task Handle_ShouldAddAuthor_WhenValidRequest()
        {
            using var database = CreateInMemoryDatabase();
            var authorRepository = new AuthorRepository(database);
            var handler = new AddAuthorCommandHandler(authorRepository);

            var newAuthor = new Author { Name = "Test Author" };
            var addAuthorCommand = new AddAuthorCommand(newAuthor);

            
            var result = await handler.Handle(addAuthorCommand, CancellationToken.None);

            
            var addedAuthor = database.Authors.FirstOrDefault(a => a.Name == "Test Author");
            Assert.NotNull(addedAuthor);
            Assert.Equal("Test Author", addedAuthor.Name);
            Assert.True(result.Success);
            Assert.Equal("Operation successful", result.Message);
        }
    }
}