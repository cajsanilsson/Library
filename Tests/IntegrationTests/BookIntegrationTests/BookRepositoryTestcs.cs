using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using FakeItEasy;
using Infrastructure.Database;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xunit;


namespace Tests.IntegrationTests.BookIntegrationTests
{
    public class BookRepositoryTest : IDisposable
    {
        private readonly LibraryDatabase _database;
        private readonly IBookRepository _bookRepository;

        public BookRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<LibraryDatabase>()
                .UseInMemoryDatabase("LibraryTestDb_Book")
                .Options;

            _database = new LibraryDatabase(options);
            _bookRepository = new BookRepository(_database);
        }

        public void Dispose()
        {
            _database.Database.EnsureDeleted();
            _database.Dispose();
        }

        [Fact]
        public async Task AddBook_ShouldAddBookToDatabase()
        {
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Test Book",
                Description = "Test Description"
            };

            var addedBook = await _bookRepository.AddBook(book);

            var bookFromDb = await _database.Books.FindAsync(addedBook.Id);

            Assert.NotNull(bookFromDb);
            Assert.Equal(book.Title, bookFromDb.Title);
            Assert.Equal(book.Description, bookFromDb.Description);
        }

        [Fact]
        public async Task GetAllBooks_ShouldReturnBooks()
        {
            var book1 = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Book 1",
                Description = "Description 1"
            };
            var book2 = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Book 2",
                Description = "Description 2"
            };

            await _bookRepository.AddBook(book1);
            await _bookRepository.AddBook(book2);

            var books = await _bookRepository.GetAllBooks();

            Assert.Equal(2, books.Count);
            Assert.Contains(books, b => b.Title == book1.Title);
            Assert.Contains(books, b => b.Title == book2.Title);
        }

        [Fact]
        public async Task GetBookById_ShouldReturnCorrectBook()
        {
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Test Book",
                Description = "Test Description"
            };

            var addedBook = await _bookRepository.AddBook(book);
            var bookFromDb = await _bookRepository.GetBookById(addedBook.Id);

            Assert.NotNull(bookFromDb);
            Assert.Equal(addedBook.Id, bookFromDb.Id);
            Assert.Equal(addedBook.Title, bookFromDb.Title);
        }

        [Fact]
        public async Task UpdateBook_ShouldUpdateBookInDatabase()
        {
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Original Title",
                Description = "Original Description"
            };

            var addedBook = await _bookRepository.AddBook(book);

            var updatedBook = new Book
            {
                Title = "Updated Title",
                Description = "Updated Description"
            };

            var bookFromDb = await _bookRepository.UpdateBook(addedBook.Id, updatedBook);

            Assert.NotNull(bookFromDb);
            Assert.Equal(updatedBook.Title, bookFromDb.Title);
            Assert.Equal(updatedBook.Description, bookFromDb.Description);
        }

        [Fact]
        public async Task DeleteBook_ShouldRemoveBookFromDatabase()
        {
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Test Book",
                Description = "Test Description"
            };

            var addedBook = await _bookRepository.AddBook(book);

            var deletedBook = await _bookRepository.DeleteBook(addedBook.Id);

            Assert.NotNull(deletedBook);
            Assert.Equal(addedBook.Id, deletedBook.Id);

            var bookFromDb = await _database.Books.FindAsync(addedBook.Id);
            Assert.Null(bookFromDb);
        }
    }
}
