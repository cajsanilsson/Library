using Application.AuthorQueries.GetAllAuthors;
using Application.BookCommands.DeleteBookCommand;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.BookTests
{
    public class DeleteBookTest
    {
        [Fact]
        public async Task DeleteBookCommandHandler_Should_RemoveBookFromFakeDatabase()
        {
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<DeleteBookCommandHandler>();
            var fakeDatabase = new FakeDatabase();
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Book to Delete",
                Description = "Description"
            };

            fakeDatabase.books.Add(book);

            var handler = new DeleteBookCommandHandler(fakeDatabase, logger);
            var command = new DeleteBookCommand(book.Id);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.DoesNotContain(book, fakeDatabase.books); 
        }
    }
}
