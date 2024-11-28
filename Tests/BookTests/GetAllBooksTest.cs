using Domain.Models;
using Infrastructure.Database;
using Application.BookQueries.GetAllBooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.AuthorQueries.GetAllAuthors;
using Microsoft.Extensions.Logging;

namespace Tests.BookTests
{
    public class GetAllBooksTest
    {
        [Fact]
        public async Task GetBooksQueryHandler_Should_ReturnAllBooksFromFakeDatabase()
        {
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<GetAllBooksQueryHandler>();
            var fakeDatabase = new FakeDatabase();
            fakeDatabase.books.Clear();
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

            var handler = new GetAllBooksQueryHandler(fakeDatabase, logger);
            var query = new GetAllBooksQuery();

            var books = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(books);
            Assert.Equal(2, books.Count);
        }
    }
}
