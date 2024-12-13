
using Application.BookCommands.AddBookCommand;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Tests.BookTests
{
   public class AddNewBookTest
    {
        private LibraryDatabase CreateInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<LibraryDatabase>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new LibraryDatabase(options);
        }

        [Fact]
        public async Task Handle_ShouldAddBook_WhenValidRequest()
        {
            using var database = CreateInMemoryDatabase();
            var bookRepository = new BookRepository(database);
            var handler = new AddBookCommandHandler(bookRepository);

            var newBook = new Book { Title = "Test Book", Description = "Test Description" };
            var addBookCommand = new AddBookCommand(newBook);

            var result = await handler.Handle(addBookCommand, CancellationToken.None);

            var addedBook = database.Books.FirstOrDefault(b => b.Title == "Test Book");
            Assert.NotNull(addedBook); 
            Assert.Equal("Test Book", addedBook.Title); 
            Assert.Equal("Test Description", addedBook.Description);  
            Assert.True(result.Success);  
            Assert.Equal("Operation successful", result.Message);
        }
    }

        
}
