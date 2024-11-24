using Application.AuthorCommands.DeleteAuthorCommand;
using Domain;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.AuthorTests
{
    public class DeleteAuthorTest
    {
        [Fact]
        public async Task DeleteAuthorCommandHandler_Should_RemoveAuthorFromFakeDatabase()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var author = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Author to Delete"
            };

            fakeDatabase.authors.Add(author);

            var handler = new DeleteAuthorCommandHandler(fakeDatabase);
            var command = new DeleteAuthorCommand(author.Id);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.DoesNotContain(author, fakeDatabase.authors); 
        }
    }
}
