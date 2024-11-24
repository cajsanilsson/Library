using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.AuthorQueries.GetAllAuthors;
using Application.BookQueries.GetBookById;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Tests.BookTests
{
    public class GetBookByIdTest
    {
        [Fact]
        public async Task GetBookByIdQueryHandler_Should_ReturnCorrectBook_WhenBookExists()
        {
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<GetBookByIdQueryHandler>();
            var fakeDatabase = new FakeDatabase();
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Test Book",
                Description = "Test Description"
            };

            fakeDatabase.books.Add(book);

            var handler = new GetBookByIdQueryHandler(fakeDatabase, logger);
            var query = new GetBookByIdQuery(book.Id);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result); 
            Assert.Equal(book.Id, result.Id);
            Assert.Equal(book.Title, result.Title);
            Assert.Equal(book.Description, result.Description);
        }

        [Fact]
        public async Task GetBookByIdQueryHandler_Should_ReturnNull_WhenBookDoesNotExist()
        {
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<GetBookByIdQueryHandler>();
            var fakeDatabase = new FakeDatabase();

            var handler = new GetBookByIdQueryHandler(fakeDatabase, logger);
            var query = new GetBookByIdQuery(Guid.NewGuid()); 

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
