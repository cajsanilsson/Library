using Application.AuthorQueries.GetAllAuthors;
using Application.BookCommands.UpdateBookCommand;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.Extensions.Logging;

namespace Tests.BookTests
{
    public class UpdateBookTest
    {
        [Fact]
        public async Task UpdateBookCommandHandler_Should_UpdateBookDetailsInFakeDatabase()
        {
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<UpdateBookCommandHandler>();
            var fakeDatabase = new FakeDatabase();
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Old Title",
                Description = "Old Description"
            };

            fakeDatabase.books.Add(book);

            var handler = new UpdateBookCommandHandler(fakeDatabase, logger);
            var command = new UpdateBookCommand(book.Id, "New Title", "New Description");

            var updatedBook = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(updatedBook);
            Assert.Equal("New Title", updatedBook.Title);
            Assert.Equal("New Description", updatedBook.Description);
        }
    }
}
