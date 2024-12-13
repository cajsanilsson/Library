using Application.AuthorCommands.UpdateAuthorCommand;
using Application.AuthorQueries.GetAllAuthors;
using Application.BookCommands.UpdateBookCommand;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Tests.BookTests
{
    public class UpdateBookTest
    {
        private LibraryDatabase CreateInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<LibraryDatabase>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new LibraryDatabase(options);
        }

        [Fact]
        public async Task UpdateBookCommandHandler_Should_UpdateBookDetailsInFakeDatabase()
        {
            using var database = CreateInMemoryDatabase();
            var bookRepository = new BookRepository(database);
            var handler = new UpdateBookCommandHandler(bookRepository);

            var bookId = Guid.NewGuid();
            var existingBook = new Book { Id = bookId, Title = "Old Title", Description = "Old Description" };
            database.Books.Add(existingBook);
            await database.SaveChangesAsync();

            var updatedBook = new Book { Id = bookId, Title = "Updated Title",Description = "Updated Description" };

            var updateBookCommand = new UpdateBookCommand(bookId, updatedBook.Title, updatedBook.Description);

            var result = await handler.Handle(updateBookCommand, CancellationToken.None);

            var updatedDbBook = database.Books.FirstOrDefault(a => a.Id == bookId);
            Assert.NotNull(updatedDbBook);
            Assert.Equal("Updated Title", updatedDbBook.Title);
            Assert.Equal("Updated Description", updatedDbBook.Description);
            Assert.True(result.Success);
            Assert.Equal("Updated Title", result.Data.Title);
        }
    }
}
