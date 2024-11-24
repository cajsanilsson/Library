using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.BookCommands.AddBookCommand;
using Domain;
using Infrastructure.Database;
using MediatR;


namespace Tests.BookTests
{
   public class AddNewBookTest
    {
        [Fact]
        public async Task AddBookToFakeDatabase()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            var handler = new AddBookCommandHandler(fakeDatabase);

            var newBook = new Book
            {
                Title = "Test Title",
                Description = "Test Description"
            };

            var command = new AddBookCommand(newBook);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result); 
            Assert.Equal("Test Title", result.Title); 
            Assert.Equal("Test Description", result.Description); 
            Assert.Contains(result, fakeDatabase.books); 
        }

        [Fact]
        public void FakeDatabase_Should_StartEmpty()
        {
            // Arrange
            var fakeDatabase = new FakeDatabase();

            // Assert
            Assert.Empty(fakeDatabase.books);
        }
    }

        
}
