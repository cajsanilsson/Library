
using Application.BookCommands.DeleteBookCommand;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.BookTests
{
    public class DeleteBookTest
    {
        private LibraryDatabase CreateInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<LibraryDatabase>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new LibraryDatabase(options);
        }

        [Fact]
        public async Task DeleteBookCommandHandler_Should_RemoveBookFromFakeDatabase()
        {
            using var database = CreateInMemoryDatabase();
            var bookRepository = new BookRepository(database);
            var handler = new DeleteBookCommandHandler(bookRepository);

            var bookId = Guid.NewGuid();
            var existingBook = new Book { Id = bookId, Title = "Test Book", Description = "Test Description" };
            database.Books.Add(existingBook);
            await database.SaveChangesAsync();

            var deleteBookCommand = new DeleteBookCommand(bookId);

            var result = await handler.Handle(deleteBookCommand, CancellationToken.None);

            var deletedBook = database.Books.FirstOrDefault(a => a.Id == bookId);
            Assert.Null(deletedBook);
            Assert.True(result.Success);
            Assert.Equal("Operation successful", result.Message);
        }
    }
}
