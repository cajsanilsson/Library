
using Application.BookQueries.GetBookById;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Tests.BookTests
{
    public class GetBookByIdTest
    {
        private LibraryDatabase CreateInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<LibraryDatabase>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new LibraryDatabase(options);
        }

        [Fact]
        public async Task GetBookByIdQueryHandler_Should_ReturnCorrectBook_WhenBookExists()
        {
            using var database = CreateInMemoryDatabase();
            var bookRepository = new BookRepository(database);
            var handler = new GetBookByIdQueryHandler(bookRepository);

            var bookId = Guid.NewGuid();
            var book = new Book { Id = bookId, Title = "Test Book", Description = "Test Description" };
            database.Books.Add(book);
            await database.SaveChangesAsync();

            var getBooksByIdQuery = new GetBookByIdQuery(bookId);

            var result = await handler.Handle(getBooksByIdQuery, CancellationToken.None);

            Assert.True(result.Success);
            Assert.Equal("Test Book", result.Data.Title);
            Assert.Equal("Operation successful", result.Message);
        }
    }
}
