using Application.AuthorCommands.AddAuthorCommand;
using Domain;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.AuthorTests
{
    public class AddNewAuthorTest
    {
        [Fact]
        public async Task AddAuthorCommandHandler_Should_AddAuthorToFakeDatabase()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var handler = new AddAuthorCommandHandler(fakeDatabase);

            var newAuthor = new Author
            {
                Name = "Test Author"
            };

            var command = new AddAuthorCommand(newAuthor);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Author", result.Name);
            Assert.Contains(result, fakeDatabase.authors); 
        }
    }
}
