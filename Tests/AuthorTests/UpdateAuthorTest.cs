using Application.AuthorCommands.UpdateAuthorCommand;
using Domain;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.AuthorTests
{
    public class UpdateAuthorTest
    {
        [Fact]
        public async Task UpdateAuthorCommandHandler_Should_UpdateAuthorDetailsInFakeDatabase()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var author = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Old Name"
            };

            fakeDatabase.authors.Add(author);

            var handler = new UpdateAuthorCommandHandler(fakeDatabase);
            var command = new UpdateAuthorCommand(author.Id, "New Name");

            // Act
            var updatedAuthor = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(updatedAuthor);
            Assert.Equal("New Name", updatedAuthor.Name);
        }
    }
}
