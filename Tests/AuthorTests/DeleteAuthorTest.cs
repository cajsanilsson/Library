using Application.AuthorCommands.DeleteAuthorCommand;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.AuthorTests
{
    public class DeleteAuthorTest
    {
        private LibraryDatabase CreateInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<LibraryDatabase>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new LibraryDatabase(options);
        }

        [Fact]
        public async Task DeleteAuthorCommandHandler_Should_RemoveAuthorFromFakeDatabase()
        {
            using var database = CreateInMemoryDatabase();
            var authorRepository = new AuthorRepository(database);
            var handler = new DeleteAuthorCommandHandler(authorRepository);

            var authorId = Guid.NewGuid();
            var existingAuthor = new Author { Id = authorId, Name = "Test Author" };
            database.Authors.Add(existingAuthor);
            await database.SaveChangesAsync();

            var deleteAuthorCommand = new DeleteAuthorCommand(authorId);

            var result = await handler.Handle(deleteAuthorCommand, CancellationToken.None);

            var deletedAuthor = database.Authors.FirstOrDefault(a => a.Id == authorId);
            Assert.Null(deletedAuthor);
            Assert.True(result.Success);
            Assert.Equal("Operation successful", result.Message);
        }
    }
}
