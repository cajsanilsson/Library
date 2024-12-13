using Domain.Models;
using Infrastructure.Database;
using Application.BookQueries.GetAllBooks;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Tests.BookTests
{
    public class GetAllBooksTest
    {
        private LibraryDatabase CreateInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<LibraryDatabase>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new LibraryDatabase(options);
        }

        [Fact]
        public async Task GetAllBooksQueryHandler_Should_ReturnAllBooksFromFakeDatabase()
        {
            using var database = CreateInMemoryDatabase();
            var bookRepository = new BookRepository(database);
            var handler = new GetAllBooksQueryHandler(bookRepository);

            database.Books.Add(new Book { Id = Guid.NewGuid(), Title = "Test Book1", Description = "Test Description1"});
            database.Books.Add(new Book { Id = Guid.NewGuid(), Title = "Test Book2", Description = "Test Description2" });
            await database.SaveChangesAsync();

            var getAllBooksQuery = new GetAllBooksQuery();

            var result = await handler.Handle(getAllBooksQuery, CancellationToken.None);

            Assert.True(result.Success);
            Assert.Equal(2, result.Data.Count);
            Assert.Equal("Operation successful", result.Message);
        }
    }
}
