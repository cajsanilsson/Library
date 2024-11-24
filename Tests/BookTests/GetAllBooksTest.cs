using Domain;
using Infrastructure.Database;
using Application.BookQueries.GetAllBooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.BookTests
{
    public class GetAllBooksTest
    {
        [Fact]
        public async Task GetBooksQueryHandler_Should_ReturnAllBooksFromFakeDatabase()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();
            fakeDatabase.books.Add(new Book
            {
                Id = Guid.NewGuid(),
                Title = "Book 1",
                Description = "Description 1"
            });
            fakeDatabase.books.Add(new Book
            {
                Id = Guid.NewGuid(),
                Title = "Book 2",
                Description = "Description 2"
            });

            var handler = new GetAllBooksQueryHandler(fakeDatabase);
            var query = new GetAllBooksQuery();

            // Act
            var books = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(books);
            Assert.Equal(2, books.Count); 
        }
    }
}
