using Application.BookCommands.UpdateBookCommand;
using Domain;
using Infrastructure.Database;

namespace Tests.BookTests
{
    public class UpdateBookTest
    {
        [Fact]
        public async Task UpdateBookCommandHandler_Should_UpdateBookDetailsInFakeDatabase()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Old Title",
                Description = "Old Description"
            };

            fakeDatabase.books.Add(book);

            var handler = new UpdateBookCommandHandler(fakeDatabase);
            var command = new UpdateBookCommand(book.Id, "New Title", "New Description");

            // Act
            var updatedBook = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(updatedBook);
            Assert.Equal("New Title", updatedBook.Title);
            Assert.Equal("New Description", updatedBook.Description);
        }
    }
}
