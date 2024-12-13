using Application.AuthorCommands.UpdateAuthorCommand;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.AuthorTests
{
    public class UpdateAuthorTest
    {
        private LibraryDatabase CreateInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<LibraryDatabase>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new LibraryDatabase(options);
        }

        [Fact]
        public async Task UpdateAuthorCommandHandler_Should_UpdateAuthorDetailsInFakeDatabase()
        {
            using var database = CreateInMemoryDatabase();
            var authorRepository = new AuthorRepository(database);
            var handler = new UpdateAuthorCommandHandler(authorRepository);

            var authorId = Guid.NewGuid();
            var existingAuthor = new Author { Id = authorId, Name = "Old Name" };
            database.Authors.Add(existingAuthor);
            await database.SaveChangesAsync();

            var updatedAuthor = new Author { Id = authorId, Name = "Updated Name" };
            
            var updateAuthorCommand = new UpdateAuthorCommand(authorId, updatedAuthor.Name);

            var result = await handler.Handle(updateAuthorCommand, CancellationToken.None);

            var updatedDbAuthor = database.Authors.FirstOrDefault(a => a.Id == authorId);
            Assert.NotNull(updatedDbAuthor);
            Assert.Equal("Updated Name", updatedDbAuthor.Name);
            Assert.True(result.Success);
            Assert.Equal("Updated Name", result.Data.Name);
        }
    }
}
